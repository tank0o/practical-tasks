using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace LB2
{
    static class Program
    {
        static void Main()
        {
            string r =
              @"(?<Punctuation><<=|>>=|<<|>>|[+-/!=<>&|^%\*]=|=>|\+\+|--|\|\||&&|\?\?|[\[\]\?{}/!,.%&*()+=;<>|~^?-])|" +
              @"(?<Identifier>[_A-Za-z][_A-Za-z0-9]*)|" +
              @"(?<Commentary>//[^\n\r]*\r?\n)|" +
              @"(?<Integer>\d+)|" +
              @"(?<Real>\d+\.\d+)|" +
              @"(?<String>@""([^""]|"""")*""|""[^\\""\n]*"")|" +
              @"(?<Newline>[\r\n]+)|" +
              @"(?<Whitespaces>[ \t\f]+)";
            Regex regex = new Regex(r, RegexOptions.ExplicitCapture);
            var source = File.ReadAllText("../../Program.cs");
            var tokens = Lexer.GetTokens(regex, source);
            var transformedTokens = Lexer.Transform(tokens);
            var transformedSource = string.Join(" ", transformedTokens.Select(x => x.Lexeme));
            File.WriteAllText("../../../LB3Out/Program.cs", transformedSource);
            int a = 3;

        }
    }
    enum TokenType
    {
        Identifier,
        Commentary,
        Punctuation,
        Integer,
        Real,
        String,
        Whitespaces,
        Newline
    }
    sealed class Token
    {
        public TokenType Type { get; }
        public string Lexeme { get; }
        public Token(TokenType type, string lexeme)
        {
            Type = type;
            Lexeme = lexeme;
        }
        public override string ToString()
        {
            return string.Format("{1} - '{0}'", Lexeme, Type);
        }
    }
    static class Lexer
    {
        public static IEnumerable<Token> GetTokens(Regex regex, string s)
        {
            int lastPos = 0;
            for (var m = regex.Match(s); m.Success; m = m.NextMatch())
            {
                if (lastPos != m.Index)
                {
                    throw new Exception("Кривая строка");
                }
                lastPos = m.Index + m.Length;
                if (m.Groups["Identifier"].Success)
                {
                    yield return new Token(TokenType.Identifier, m.Groups["Identifier"].Value);
                }
                else if (m.Groups["Commentary"].Success)
                {
                    yield return new Token(TokenType.Commentary, m.Groups["Commentary"].Value);
                }
                else if (m.Groups["Punctuation"].Success)
                {
                    yield return new Token(TokenType.Punctuation, m.Groups["Punctuation"].Value);
                }
                else if (m.Groups["Integer"].Success)
                {
                    yield return new Token(TokenType.Integer, m.Groups["Integer"].Value);
                }
                else if (m.Groups["Real"].Success)
                {
                    yield return new Token(TokenType.Real, m.Groups["Real"].Value);
                }
                else if (m.Groups["String"].Success)
                {
                    yield return new Token(TokenType.String, m.Groups["String"].Value);
                }
                else if (m.Groups["Whitespaces"].Success)
                {
                    yield return new Token(TokenType.Whitespaces, m.Groups["Whitespaces"].Value);
                }
                else if (m.Groups["Newline"].Success)
                {
                    yield return new Token(TokenType.Newline, m.Groups["Newline"].Value);
                }
                else
                {
                    throw new Exception("Кривая регулярка");
                }
                Console.Write(m.Value);
            }
            if (lastPos != s.Length)
            {

                throw new Exception("Кривая строка");
            }
        }

        public static IEnumerable<Token> Transform(IEnumerable<Token> tokens)
        {
            Token previous = null, current = null, next = null;
            foreach (var item in tokens)
            {
                previous = current;
                current = next;
                next = item;
                if (current == null) continue;

                if (previous != null && previous.Type == TokenType.Punctuation)
                {
                    if (current.Type != TokenType.Whitespaces)
                    {
                        yield return new Token(TokenType.Whitespaces, " ");
                        yield return current;
                    }

                    else if (current.Type == TokenType.Whitespaces)
                    {
                        yield return new Token(TokenType.Whitespaces, " ");
                    }
                }
                else
                    yield return current;
            }
            yield return next;
        }
        static void gg()
        {
            int a = 1 + 2 * 3 - 4 / 8;
            if (!false == true && 32 <= 5 || 3 % 123 != 6 && 3 == 5 || 3 > 2 | 3 < 2 & 2 >= 3)
                a += 2;
            a = 3 / 2;
            a = 3 * 2;
            a = 3 - 2;
            a = 3 + 2;
            a = 3 % 2;
            a = 3 ^ 2;
            int b = ~a;
            uint c = 0000;
            uint d;
            d = c >> 2;
            d = c << 2;
            int? x = null;
            int y = x ?? -1;
            c >>= 2;
            a -= 3;
            a--;
            a++;
            a *= 3;
            a /= 3;
            a %= 3;
            a &= 3;
            a |= 3;
            a ^= 3;
            a <<= 3;
            Action odsda = () => { };
        }
    }

}