using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnalisadorLexicoForms
{
    public class AnalisadorLexico
    {
        // Tokens definidos
        private static readonly Dictionary<string, string> PalavrasChaveDict = new Dictionary<string, string>
    {
        { "if", "CM_IF" },
        { "else", "CM_ELSE" },
        { "while", "CM_WHILE" },
        { "var", "CM_VAR" },
        { "int", "TYPE_INT" },
        { "real", "TYPE_REAL" },
        { "=", "CM_ATRIB" }
    };

        private static readonly Dictionary<string, string> OperadoresDict = new Dictionary<string, string>
    {
        { "+", "OP_ADD" },
        { "-", "OP_SUB" },
        { "*", "OP_MUL" },
        { "/", "OP_DIV" },
        { "^", "OP_EXP" },
        { ">", "OP_GT" },
        { "<", "OP_LT" },
        { ">=", "OP_GTE" },
        { "<=", "OP_LTE" },
        { "==", "OP_EQ" },
        { "!=", "OP_NEQ" }
    };

        private static readonly HashSet<string> DelimitadoresDict = new HashSet<string>
    {
        "(", ")", ".", ",", "{", "}", ";"
    };

        public static List<LexemaXToken> Analisar(string input)
        {
            var tokens = new List<LexemaXToken>();
            string pattern = @"([A-Za-z_][A-Za-z0-9_]*)|([0-9]*\.?[0-9]+)|([+\-*/^><=!]=?)|([(){};,])|(\s+)";
            var matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                string lexema = match.Value;

                if (string.IsNullOrWhiteSpace(lexema)) continue; // Ignora espaços em branco, tabs e novas linhas

                if (PalavrasChaveDict.ContainsKey(lexema))
                {
                    tokens.Add(new LexemaXToken(lexema, PalavrasChaveDict[lexema]));
                }
                else if (OperadoresDict.ContainsKey(lexema))
                {
                    tokens.Add(new LexemaXToken(lexema, OperadoresDict[lexema]));
                }
                else if (DelimitadoresDict.Contains(lexema))
                {
                    tokens.Add(new LexemaXToken(lexema, "DELIM"));
                }
                else if (Regex.IsMatch(lexema, @"^[A-Za-z_][A-Za-z0-9_]*$"))
                {
                    tokens.Add(new LexemaXToken(lexema, "ID"));
                }
                else if (Regex.IsMatch(lexema, @"^-?[0-9]*\.?[0-9]+$"))
                {
                    tokens.Add(new LexemaXToken(lexema, "NUM"));
                }
                else
                {
                    throw new ApplicationException($"Lexical error: Unrecognized token '{lexema}'");
                }
            }

            return tokens;
        }
    }
}
