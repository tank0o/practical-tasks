using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace _4_2
{
    enum TokenType
    {
        IntegerLiteral,
        Whitespaces,
        Identifier,
        Punctuation,
        Eof
    }
    sealed class Token
    {
        public TokenType Type { get; }
        public string Lexeme { get; }
        public Token(TokenType type, string lexeme = "")
        {
            Type = type;
            Lexeme = lexeme;
        }
        public override string ToString()
        {
            return String.Format("{0}\t{1}", Type, Lexeme);
        }
    }
    static class Lexer
    {
        public static IEnumerable<Token> GetTokens(string s)
        {
            List<Tuple<string, TokenType>> Type = new Dictionary<string, TokenType>
                {{ "IntegerLiteral", TokenType.IntegerLiteral },
                { "Whitespaces", TokenType.Whitespaces },
                { "Identifier", TokenType.Identifier },
                { "Punctuation", TokenType.Punctuation } }.Select(x => Tuple.Create<string, TokenType>(x.Key, x.Value)).ToList();
            Regex lexemeRx = new Regex(@"(?<IntegerLiteral>\d+) |   " +
                                            @"(?<Whitespaces>[ \t\r\n]+)   |   " +
                                            @"(?<Identifier>[a-zA-Z]+\d*)     |  " +
                                            @"(?<Punctuation>(\|\|)|(=>)|(==)|(--)|(\+=)|(\+\+)|(!=)|(&&)|[\+\-\*\\={}!<>;,\[\]()|/.])",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.ExplicitCapture);
            int lastPos = 0;
            string[] namesG = lexemeRx.GetGroupNames();
            for (var m = lexemeRx.Match(s); m.Success; m = m.NextMatch())
            {
                if (m.Index != lastPos)
                {
                    string error = s.Substring(lastPos, m.Index - lastPos);
                    throw new Exception(String.Format("Ошибка в символе: {0}", error));
                }
                lastPos = m.Index + m.Length;
                Token answer = null;
                foreach (var i in Type)
                {
                    if (m.Groups[i.Item1].Success)
                    {
                        if (answer != null) new Exception(String.Format("Ошибка в регулярном выражении"));
                        answer = new Token(i.Item2, m.Groups[i.Item1].Value);
                    }
                }
                if (answer == null) new Exception(String.Format("Неправильная входная строка"));
                yield return answer;
            }
        }
    }
}
