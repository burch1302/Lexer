using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer {
    internal class Program {
        static void Main(string[] args) {

            string code = " string i = \"Hi";

            var lexer = new Lexer(code);
            List<Token> tokens = new List<Token>();
            Token token;
            do {
                token = lexer.NextToken();
                tokens.Add(token);
            }
            while (token.Type != TokenType.ENDOFLINE);

            foreach (var t in tokens) {
                Console.WriteLine(t);
            }

            Console.WriteLine("Лексер закончил работу.");

        }
    }
}
