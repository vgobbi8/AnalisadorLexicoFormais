using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisadorLexicoForms
{
    public class LexemaXToken
    {
        public LexemaXToken(string lex, string token)
        {
            this.Lexema = lex;
            this.Token = token;
        }
        public string Lexema { get; set; }
        public string Token { get; set; }
    }
}
