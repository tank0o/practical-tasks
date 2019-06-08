using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_2
{
    class Parser
    {
        int pos;
        Token[] tokens;
        Token currentToken
        {
            get
            {
                return pos < tokens.Length
                ? tokens[pos]
                : new Token(TokenType.Eof);
            }
        }

        public Parser(string sourse)
        {
            pos = 0;
            tokens = DeleteSpases(Lexer.GetTokens(sourse)).ToArray();
        }

        IEnumerable<Token> DeleteSpases(IEnumerable<Token> tokens)
        {
            foreach (var t in tokens)
            {
                if (t.Type != TokenType.Whitespaces)
                    yield return t;
            }
        }

        void ReadNext()
        {
            pos += 1;
        }

        bool SkipIf(string s)
        {
            if (currentToken.Lexeme.ToUpperInvariant() == s.ToUpperInvariant())
            {
                ReadNext();
                return true;
            }
            return false;
        }

        void Expect(string s)
        {
            if (!SkipIf(s))
            {
                throw new Exception($"Ожидали {s}");
            }
        }

        public INode SelectStmt()
        {
            var tree = ParseSelectStmt();
            if (currentToken.Type != TokenType.Eof)
            {
                throw new Exception("Недопарсили до конца");
            }
            return tree;
        }

        SelectStmt2 ParseSelectStmt()
        {
            if (!SkipIf("SELECT"))
                throw new Exception("Ожидалось 'SELECT'");
            List<string> columns = new List<string>();
            columns.Add(ParseIdentifier().Name);
            while (SkipIf(","))
            {
                columns.Add(ParseIdentifier().Name);
            }

            if (!SkipIf("FROM"))
                throw new Exception("Ожидалось 'FROM'");
            var fromTable = ParseIdentifier().Name;

            if (!SkipIf("ORDER"))
                throw new Exception("Ожидалось 'ORDER'");
            if (!SkipIf("BY"))
                throw new Exception("Ожидалось 'BY'");

            List<DescAsc> orderByColumns = new List<DescAsc>();
            orderByColumns.Add(DescAscsParse());
            while (SkipIf(","))
            {
                orderByColumns.Add(DescAscsParse());
            }
            return new SelectStmt2(columns, fromTable, orderByColumns);
        }

        DescAsc DescAscsParse()
        {
            var expression = ParseBinarySum();
            EDescAsc descAsc = EDescAsc.NULL;
            if (SkipIf("asc"))
            {
                descAsc = EDescAsc.asc;
            }
            else if (SkipIf("desc"))
            {
                descAsc = EDescAsc.desc;
            }

            return new DescAsc(expression, descAsc);
        }

        IExpression ParseBinarySum()
        {
            var left = ParseBinaryMult();
            while (true)
            {
                if (SkipIf("+"))
                    left = new Binary(left, "+", ParseBinaryMult());
                else if (SkipIf("-"))
                    left = new Binary(left, "-", ParseBinaryMult());
                else break;
            }
            return left;
        }

        IExpression ParseBinaryMult()
        {
            var left = ParsePrimary();
            while (true)
            {
                if (SkipIf("*"))
                    left = new Binary(left, "*", ParsePrimary());
                else if (SkipIf("/"))
                    left = new Binary(left, "/", ParsePrimary());
                else break;
            }
            return left;
        }

        IExpression ParsePrimary()
        {
            IExpression res;
            if (SkipIf("("))
            {
                var t = new Parentheses(ParseBinarySum());
                Expect(")");
                return t;
            }
            if (currentToken.Type == TokenType.IntegerLiteral)
            {
                res = ParseNumber();
                return res;
            }
            else if (currentToken.Type == TokenType.Identifier)
            {

                res = ParseIdentifier();
                return res;
            }
            
            throw new Exception();
        }
        Identifier ParseIdentifier()
        {
            if (currentToken.Type != TokenType.Identifier)
                throw new Exception("Ожидался идентификатор");
            var t = currentToken;
            ReadNext();
            return new Identifier(t.Lexeme);
        }
        IExpression ParseNumber()
        {
            if (currentToken.Type != TokenType.IntegerLiteral)
                throw new Exception("Ожидалось число");
            var t = currentToken;
            ReadNext();
            return new Number(t.Lexeme);
        }
    }
}
