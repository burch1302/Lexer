using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer {

    public enum TokenType {
        IDENTIFIER,
        NUMBER,
        STRING,
        KEYWORD,
        OPERATOR,
        PUNCTUATION,
        COMMENT,
        WHITESPACE,
        UNKNOWN,
        ENDOFLINE
    }
    public class Token {

        public TokenType Type { get; }
        public string Lexem { get; }
        public int Line { get; }
        public int Column { get; }

        public Token(TokenType type, string lexeme, int line, int column) {
            Type = type;
            Lexem = lexeme;
            Line = line;
            Column = column;
        }

        public override string ToString() =>
            $"{Type} \"{Lexem}\" ({Line}:{Column})";
    }
}
