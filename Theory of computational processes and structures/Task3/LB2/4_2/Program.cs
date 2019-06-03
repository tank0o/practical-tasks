using System;
using System.Collections.Generic;
using System.IO;

namespace _4_2
{
    public interface INode
    {
        string ToFormattedString();
        void DebugPrint(TextWriter o, int depth);
        string Accept(INodeVisitor<string> v);
        void Accept(INodeVisitor v);

    }
    public interface IExpression : INode { }

    public class SelectStmt : INode
    {
        public IReadOnlyList<string> columns;
        public readonly string fromTable;
        public IReadOnlyList<DescAsc> orderByColumns;
        public SelectStmt(IReadOnlyList<string> columns, string fromTable, IReadOnlyList<DescAsc> orderByColumns)
        {
            this.columns = columns;
            this.fromTable = fromTable;
            this.orderByColumns = orderByColumns;
        }

        public string Accept(INodeVisitor<string> v) => v.VisitSelectStmt(this);
        public void Accept(INodeVisitor v) => v.VisitSelectStmt(this);
        public string ToFormattedString()
        {
            string columnsString = columns[0];
            for (int i = 1; i < columns.Count; i++)
                columnsString += ", " + columns[i];

            string orderByColumnsString = orderByColumns[0].ToFormattedString();
            for (int i = 1; i < orderByColumns.Count; i++)
                orderByColumnsString += ", " + orderByColumns[i].ToFormattedString();

            return
                $"SELECT {columnsString} FROM {fromTable} ORDER BY {orderByColumnsString}";
        }
        public void DebugPrint(TextWriter o, int depth)
        {
            o.WriteIndent(depth);
            o.Write("new SelectStmt(\n");
            o.WriteIndent(depth + 1);
            o.Write("new string[] {\n");
            for (int i = 0; i < columns.Count; i++)
            {
                if (i > 0)
                {
                    o.Write(",\n");
                }
                o.WriteIndent(depth + 2);
                o.Write("\"" + columns[i] + "\"");
            }
            o.Write("\n");
            o.WriteIndent(depth + 1);
            o.Write("},\n");
            o.WriteIndent(depth + 1);
            o.Write("\"" + fromTable + "\"");
            o.Write(",\n");
            o.WriteIndent(depth + 1);
            o.Write("new DescAsc[] {\n");
            for (int i = 0; i < orderByColumns.Count; i++)
            {
                if (i > 0)
                {
                    o.Write(",\n");
                }
                orderByColumns[i].DebugPrint(o, depth + 2);
            }
            o.Write("\n");
            o.WriteIndent(depth + 1);
            o.Write("}\n");
            o.WriteIndent(depth);
            o.Write(")");
        }
    }

    public enum EDescAsc
    {
        desc,
        asc,
        NULL
    }

    sealed public class DescAsc : INode
    {
        public readonly IExpression expression;
        public readonly EDescAsc descAsc;

        public DescAsc(IExpression expression, EDescAsc descAsc)
        {
            this.expression = expression;
            this.descAsc = descAsc;
        }

        public string Accept(INodeVisitor<string> v) => v.VisitDescAsc(this);

        public void Accept(INodeVisitor v) => v.VisitDescAsc(this);

        public string ToFormattedString()
        {
            string descAscStr = "";
            if (descAsc == EDescAsc.asc)
                descAscStr = "asc";
            else if (descAsc == EDescAsc.desc)
                descAscStr = "desc";
            return
                $"{expression.ToFormattedString()} {descAscStr }";
        }
        public void DebugPrint(TextWriter o, int depth)
        {
            string descAscStr = "";
            if (descAsc == EDescAsc.asc)
                descAscStr = "EDescAsc.asc";
            else if (descAsc == EDescAsc.desc)
                descAscStr = "EDescAsc.desc";
            else descAscStr = "EDescAsc.NULL";

            o.WriteIndent(depth);
            o.Write("new DescAsc(\n");
            expression.DebugPrint(o, depth + 1);
            o.Write(",\n");
            o.WriteIndent(depth + 1);
            o.Write(descAscStr);
            o.Write("\n");
            o.WriteIndent(depth);
            o.Write(")");
        }
    }

    public class Binary : IExpression
    {
        public readonly IExpression left;
        public readonly string operation;
        public readonly IExpression right;
        public Binary(IExpression left, string operation, IExpression right)
        {
            this.left = left;
            this.operation = operation;
            this.right = right;
        }

        public string Accept(INodeVisitor<string> v) => v.VisitBinary(this);
        public void Accept(INodeVisitor v) => v.VisitBinary(this);

        public string ToFormattedString()
        {
            return
                $"{left.ToFormattedString()}{operation}{right.ToFormattedString()}";
        }
        public void DebugPrint(TextWriter o, int depth)
        {
            o.WriteIndent(depth);
            o.Write("new Binary(\n");
            left.DebugPrint(o, depth + 1);
            o.Write(",\n");
            o.WriteIndent(depth + 1);
            o.Write($"\"{operation}\",\n");
            right.DebugPrint(o, depth + 1);
            o.Write("\n");
            o.WriteIndent(depth);
            o.Write(")");
        }
    }

    sealed public class Number : IExpression
    {
        public readonly string number;
        public Number(string number)
        {
            this.number = number;
        }

        public string Accept(INodeVisitor<string> v) => v.VisitNumber(this);
        public void Accept(INodeVisitor v) => v.VisitNumber(this);
        public string ToFormattedString() => number;
        public void DebugPrint(TextWriter o, int depth)
        {
            o.WriteIndent(depth);
            o.Write($"new Number(\"{number}\")");
        }
    }
    sealed public class Identifier : IExpression
    {
        public readonly string Name;
        public Identifier(string Name)
        {
            this.Name = Name;
        }

        public string Accept(INodeVisitor<string> v) => v.VisitIdentifier(this);

        public void Accept(INodeVisitor v) => v.VisitIdentifier(this);

        public string ToFormattedString()
        {
            return Name;
        }
        public void DebugPrint(TextWriter o, int depth)
        {
            o.WriteIndent(depth);
            o.Write($"new Identifier(\"{Name}\")");
        }
    }
    public class Parentheses : IExpression
    {
        public readonly IExpression child;
        public Parentheses(IExpression child)
        {
            this.child = child;
        }
        public string ToFormattedString()
        {
            return $"({child.ToFormattedString()})";
        }
        public void DebugPrint(TextWriter o, int depth)
        {
            o.WriteIndent(depth);
            o.Write("new Parentheses(\n");
            child.DebugPrint(o, depth + 1);
            o.Write("\n");
            o.WriteIndent(depth);
            o.Write(")");
        }
        public string Accept(INodeVisitor<string> v) => v.VisitParentheses(this);
        public void Accept(INodeVisitor v) => v.VisitParentheses(this);
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
            string s = "SELECT ab, dfg FROM bla ORDER BY ab, dfg + 2 * 6 desc, df asc";
            s = "SELECT ab, dfg FROM bla ORDER BY 1+2+3 asc";
            //var tree = new SelectStmt(
            //    new string[]
            //    {
            //        "ab",
            //        "dfg"
            //    },
            //    "bla",
            //    new DescAsc[]
            //    {
            //        new DescAsc(
            //            new Identifier("ab"),""),
            //        new DescAsc(
            //            new Binary(
            //                new Identifier("dfg"), "+",
            //                new Binary(new Number("2"),"*",
            //                    new Number("6"))),"desc"),
            //        new DescAsc(new Identifier("df"),"asc")
            //    });
            //Console.WriteLine(tree.ToFormattedString());
            //// Отладочная строка
            //Console.WriteLine(tree.ToDebugString());

            var tree = new Parser(s).SelectStmt();
            ToFormattedString interpreter = new ToFormattedString();
            Console.WriteLine(tree.Accept(interpreter));
            DebugPrint debugPrint = new DebugPrint(tree);
            Console.WriteLine(debugPrint.result);
        }
    }
}