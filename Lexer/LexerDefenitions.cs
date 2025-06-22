using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer {
    public static class LexerDefenitions {

        public static readonly HashSet<string> Keywords = new HashSet<string>{
            "if", "else", "while", "for", "return", "int", "float",
            "void", "char", "string", "break", "continue"
        };

        public static readonly Dictionary<string, TokenType> OperatorsAndPunctuations = new Dictionary<string, TokenType>
{
            { "==", TokenType.OPERATOR },
            { "!=", TokenType.OPERATOR },
            { ">=", TokenType.OPERATOR },
            { "<=", TokenType.OPERATOR },
            { "=", TokenType.OPERATOR },
            { "+", TokenType.OPERATOR },
            { "-", TokenType.OPERATOR },
            { "*", TokenType.OPERATOR },
            { "/", TokenType.OPERATOR },
            { ">", TokenType.OPERATOR },
            { "<", TokenType.OPERATOR },
            { "(", TokenType.PUNCTUATION },
            { ")", TokenType.PUNCTUATION },
            { "{", TokenType.PUNCTUATION },
            { "}", TokenType.PUNCTUATION },
            { "[", TokenType.PUNCTUATION },
            { "]", TokenType.PUNCTUATION },
            { ";", TokenType.PUNCTUATION },
            { ",", TokenType.PUNCTUATION },
            { "+=", TokenType.OPERATOR },
            { "-=", TokenType.OPERATOR },
            { "*=", TokenType.OPERATOR },
            { "/=", TokenType.OPERATOR },
            { "++", TokenType.OPERATOR },
            { "--", TokenType.OPERATOR }
        };
    }
}
