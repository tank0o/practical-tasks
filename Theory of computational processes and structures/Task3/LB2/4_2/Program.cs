using System;
using System.Collections.Generic;
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
    interface IExpression : INode { }

    sealed class SelectStmt : INode
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
                o.Write("\""+columns[i]+"\"");
            }
            o.Write("\n");
            o.WriteIndent(depth + 1);
            o.Write("},\n");
            o.WriteIndent(depth + 1);
            o.Write("\""+fromTable+ "\"");
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

    sealed class DescAsc : INode
    {
        public readonly IExpression expression;
        public readonly string descAsc;

        public DescAsc(IExpression expression, string descAsc)
        {
            this.expression = expression;
            this.descAsc = descAsc;
        }

        public string ToFormattedString()
        {
            return
                $"{expression.ToFormattedString()} {descAsc}";
        }
        public void DebugPrint(TextWriter o, int depth)
        {
            o.WriteIndent(depth);
            o.Write("new DescAsc(\n");
            expression.DebugPrint(o, depth + 1);
            o.Write(",\n");
            o.WriteIndent(depth + 1);
            o.Write($"\"{descAsc}\"");
            o.Write("\n");
            o.WriteIndent(depth);
            o.Write(")");
        }
    }

    sealed class Binary : IExpression
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
        public string ToFormattedString()
        {
            return number;
        }
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
                new string[]
                {
                    "ab",
                    "dfg"
                },
                "bla",
                new DescAsc[]
                {
                    new DescAsc(
                        new Identifier("ab"),""),
                    new DescAsc(
                        new Binary(
                            new Identifier("dfg"), "+",
                            new Binary(new Number("2"),"*",
                                new Number("6"))),"desc"),
                    new DescAsc(new Identifier("df"),"asc")
                });
            Console.WriteLine(tree.ToFormattedString());
            // Отладочная строка
            Console.WriteLine(tree.ToDebugString());
        }
    }
}