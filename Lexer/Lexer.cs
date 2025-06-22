using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer {
    public class Lexer {
        private readonly string _source;
        private int _pos = 0;
        private int _line = 1;
        private int _column = 1;

        public Lexer(string source)
        {
            _source = source;
        }

        private char Current => _pos < _source.Length ? _source[_pos] : '\0';

        private char Peek(int offset = 1) => 
            (_pos + offset < _source.Length) ? _source[_pos + offset] : '\0';

        private void Advance() {
            if(Current == '\n') {
                _line++;
                _column = 1;
            } else {
                _column++;
            }
            _pos++;
        }

        private void SkipWhitespaceAndComments() {
            while (true) {
                if (char.IsWhiteSpace(Current)) {
                    Advance();
                    continue;
                }

                if(Current == '/' && Peek() == '/') {
                    Advance();
                    Advance();
                    while(Current != '\n' && Current != '\0') {
                        Advance();
                    }
                    continue;
                }

                if(Current == '/' && Peek() == '*' ) {
                    Advance();
                    Advance();
                    while (!(Current == '*' && Peek() == '/') && Current != '\0') {
                        Advance();
                    }
                    if(Current == '\0') {
                        Advance();
                        Advance();
                    }
                    continue;
                }
                break;
            }
        }

        private Token ReadIdentifierOrKeyword() {
            int startLine = _line;
            int startColumn = _column;

            var sb = new StringBuilder();

            while (char.IsLetterOrDigit(Current) || Current == '_') {
                sb.Append(Current);
                Advance();
            }

            string lexeme = sb.ToString();

            TokenType type = LexerDefenitions.Keywords.Contains(lexeme)
                ? TokenType.KEYWORD
                : TokenType.IDENTIFIER;

            return new Token(type, lexeme, startLine, startColumn);
        }

        private Token ReadNumber() {
            int startLine = _line;
            int startColumn = _column;
            var sb = new StringBuilder();
            bool seenDot = false;

            while (char.IsDigit(Current) || (!seenDot && Current == '.')) {
                if (Current == '.')
                    seenDot = true;
                sb.Append(Current);
                Advance();
            }

            return new Token(TokenType.NUMBER, sb.ToString(), startLine, startColumn);
        }

        private Token ReadOperatorOrPunctuation() {
            int startLine = _line;
            int startColumn = _column;

            string twoChar = $"{Current}{Peek()}";

            if (LexerDefenitions.OperatorsAndPunctuations.ContainsKey(twoChar)) {
                Advance();
                Advance();
                return new Token(LexerDefenitions.OperatorsAndPunctuations[twoChar], twoChar, startLine, startColumn);
            }

            string oneChar = $"{Current}";
            if (LexerDefenitions.OperatorsAndPunctuations.ContainsKey(oneChar)) {
                Advance();
                return new Token(LexerDefenitions.OperatorsAndPunctuations[oneChar], oneChar, startLine, startColumn);
            }

            char unknown = Current;
            Advance();
            return new Token(TokenType.UNKNOWN, unknown.ToString(), startLine, startColumn);
        }

        private Token ReadString() {
            int startLine = _line;
            int startColumn = _column;

            var sb = new StringBuilder();
            Advance();

            while (Current != '"' && Current != '\0') {
                if (Current == '\\') {
                    Advance();
                    switch (Current) {
                        case 'n': sb.Append('\n'); break;
                        case 't': sb.Append('\t'); break;
                        case '"': sb.Append('"'); break;
                        case '\\': sb.Append('\\'); break;
                        default: sb.Append(Current); break;
                    }
                } else {
                    sb.Append(Current);
                }
                Advance();
            }
            if (Current == '"')
                Advance();
            else
                PrintError($"[Lexical Error] Не закрита строка {startLine}:{startColumn}");

            return new Token(TokenType.STRING, sb.ToString(), startLine, startColumn);

        }

        private void PrintError(string message) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public Token NextToken() {
            SkipWhitespaceAndComments();

            int startLine = _line;
            int startColumn = _column;

            if (Current == '\0')
                return new Token(TokenType.ENDOFLINE, "<EOF>", startLine, startColumn);

            if (char.IsLetter(Current) || Current == '_')
                return ReadIdentifierOrKeyword();

            if (char.IsDigit(Current))
                return ReadNumber();

            string twoChar = $"{Current}{Peek()}";
            string oneChar = $"{Current}";
            if (LexerDefenitions.OperatorsAndPunctuations.ContainsKey(twoChar) ||
                LexerDefenitions.OperatorsAndPunctuations.ContainsKey(oneChar))
                return ReadOperatorOrPunctuation();

            if (Current == '"')
                return ReadString();

            Advance();

            return new Token(TokenType.UNKNOWN, Current.ToString(), startLine, startColumn);
        }
    }
}
