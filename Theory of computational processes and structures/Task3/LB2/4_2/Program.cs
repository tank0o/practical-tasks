using System;
using System.IO;

namespace _4_2
{
    static class TextWriterExtensions
    {
        public static void WriteIndent(this TextWriter o, int depth)
        {
            o.Write(new string(' ', depth * 2));
        }
    }
    interface INode
    {
        string ToFormattedString();
        void DebugPrint(TextWriter o, int depth);
    }
    interface IIdentifierExpr : INode { }
    interface IDescAscExprs : INode { }

    sealed class Subquery : INode
    {
        public readonly SelectStmt SelectStmt;
        public Subquery(SelectStmt selectStmt)
        {
            SelectStmt = selectStmt;
        }

        public void DebugPrint(TextWriter o, int depth)
        {
            throw new NotImplementedException();
        }

        public string ToFormattedString()
        {
            throw new NotImplementedException();
        }
    }
    sealed class SelectStmt : INode
    {
        public readonly IIdentifierExpr Columns;
        public readonly string From;
        public SelectStmt(IdentifierExprs columns, string from)
        {
            Columns = columns;
            From = from;
        }

        public void DebugPrint(TextWriter o, int depth)
        {
            throw new NotImplementedException();
        }

        public string ToFormattedString()
        {
            throw new NotImplementedException();
        }
    }

    sealed class IdentifierExprs : IIdentifierExpr
    {
        public readonly IdentifierExpr Column;
        public readonly IIdentifierExpr AtherColums;

        public IdentifierExprs(IIdentifierExpr atherColums, IdentifierExpr column)
        {
            AtherColums = atherColums;
            Column = column;
        }
        public IdentifierExprs(IdentifierExpr column)
        {
            Column = column;
        }

        public void DebugPrint(TextWriter o, int depth)
        {
            throw new NotImplementedException();
        }

        public string ToFormattedString()
        {
            throw new NotImplementedException();
        }
    }

    sealed class IdentifierExpr : IIdentifierExpr
    {

        public readonly string Table;

        public IdentifierExpr(string table)
        {
            Table = table;
        }

        public void DebugPrint(TextWriter o, int depth)
        {
            throw new NotImplementedException();
        }

        public string ToFormattedString()
        {
            throw new NotImplementedException();
        }
    }

    sealed class OrderBy : INode
    {
        public OrderBy()
        {
        }

        public void DebugPrint(TextWriter o, int depth)
        {
            throw new NotImplementedException();
        }

        public string ToFormattedString()
        {
            throw new NotImplementedException();
        }
    }

    sealed class DescAscExprs : IDescAscExprs
    {
        public readonly DescAscExpr Var1;
        public readonly IDescAscExprs AtherVAr;


        public void DebugPrint(TextWriter o, int depth)
        {
            throw new NotImplementedException();
        }

        public string ToFormattedString()
        {
            throw new NotImplementedException();
        }
    }

    sealed class DescAscExpr : IDescAscExprs
    {
        public readonly SumExpr sumExpr;
        public readonly string descAsc;

        public void DebugPrint(TextWriter o, int depth)
        {
            throw new NotImplementedException();
        }

        public string ToFormattedString()
        {
            throw new NotImplementedException();
        }
    }

    sealed class SumExpr : INode
    {


        public void DebugPrint(TextWriter o, int depth)
        {
            throw new NotImplementedException();
        }

        public string ToFormattedString()
        {
            throw new NotImplementedException();
        }
    }

    sealed class MultExpr : INode
    {
        public void DebugPrint(TextWriter o, int depth)
        {
            throw new NotImplementedException();
        }

        public string ToFormattedString()
        {
            throw new NotImplementedException();
        }
    }

    sealed class Primary : INode
    {
        public void DebugPrint(TextWriter o, int depth)
        {
            throw new NotImplementedException();
        }

        public string ToFormattedString()
        {
            throw new NotImplementedException();
        }
    }

    static class NodeExtensions
    {
        public static string ToDebugString(this INode node)
        {
            using (var o = new StringWriter())
            {
                node.DebugPrint(o, 0);
                o.Write("\n");
                return o.ToString();
            }
        }
    }
    static class Program
    {
        static void Main()
        {
            //SELECT ab, dfg FROM bla ORDER BY ab, dfg + 2 * 6 desc, df asc
            var tree = new SelectStmt(
                new IdentifierExprs(
                    new IdentifierExprs(
                        new IdentifierExpr("ad")), new IdentifierExpr("dfg")), "bla");










            //// SELECT 1 FROM (SELECT 2 FROM (SELECT 3 FROM t1.t2))
            //var tree = new SelectStmt(
            //    1,
            //    new Subquery(
            //        new SelectStmt(
            //            2,
            //            new Subquery(
            //                new SelectStmt(
            //                    3,
            //                    new Table("t1", "t2")
            //                    )))));
            //// Отформатированная строка
            //Console.WriteLine(tree.ToFormattedString());
            //// Отладочная строка
            //Console.WriteLine(tree.ToDebugString());
        }
    }
}
