using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_2
{
    public interface INodeVisitor
    {
        void VisitSelectStmt(SelectStmt2 obj);
        void VisitDescAsc(DescAsc obj);
        void VisitBinary(Binary obj);
        void VisitNumber(Number number);
        void VisitIdentifier(Identifier identifier);
        void VisitParentheses(Parentheses parentheses);
    }
    public interface IAcceptFormatted
    {
        void Accept(INodeVisitor v);
    }
    public static class TextWriterExtensions
    {
        public static void WriteIndent(this TextWriter o, int depth)
        {
            o.Write(new string(' ', depth * 2));
        }
    }
    class DebugPrint : INodeVisitor
    {
        TextWriter o;
        int depth;
        public string result { get; set; }
        public DebugPrint(INode node)
        {
            using (o = new StringWriter())
            {
                depth = 0;
                Accept(node);
                o.Write("\n");
                result = o.ToString();
            }
        }
        void Accept(INode node)
        {
            node.Accept(this);
        }
        public void VisitSelectStmt(SelectStmt2 obj)
        {
            o.WriteIndent(depth);
            o.Write($"new {nameof(SelectStmt2)}(\n");
            o.WriteIndent(depth + 1);
            o.Write("new string[] {\n");
            for (int i = 0; i < obj.columns.Count; i++)
            {
                if (i > 0)
                {
                    o.Write(",\n");
                }
                o.WriteIndent(depth + 2);
                o.Write($"\"{ obj.columns[i]}\"");
            }
            o.Write("\n");
            o.WriteIndent(depth + 1);
            o.Write("},\n");
            o.WriteIndent(depth + 1);
            o.Write($"\"{ obj.fromTable}\"");
            o.Write(",\n");
            o.WriteIndent(depth + 1);
            o.Write($"new { nameof(DescAsc) }[]{{ \n");
            for (int i = 0; i < obj.orderByColumns.Count; i++)
            {
                if (i > 0)
                {
                    o.Write(",\n");
                }
                depth++;
                obj.orderByColumns[i].Accept(this);
                depth--;
            }
            o.Write("\n");
            o.WriteIndent(depth + 1);
            o.Write("}\n");
            o.WriteIndent(depth);
            o.Write(")");
        }
        public void VisitDescAsc(DescAsc obj)
        {
            string descAscStr = nameof(EDescAsc) + ".";
            if (obj.descAsc == EDescAsc.asc)
                descAscStr += nameof(EDescAsc.asc);
            else if (obj.descAsc == EDescAsc.desc)
                descAscStr += nameof(EDescAsc.desc);
            else descAscStr += nameof(EDescAsc.NULL);


            o.WriteIndent(depth);
            o.Write($"new { nameof(DescAsc)}(\n");
            depth++;
            obj.expression.Accept(this);
            depth--;
            o.Write(",\n");
            o.WriteIndent(depth + 1);
            o.Write(descAscStr);
            o.Write("\n");
            o.WriteIndent(depth);
            o.Write(")");
        }
        public void VisitBinary(Binary obj)
        {
            o.WriteIndent(depth);
            o.Write($"new { nameof(Binary)}(\n");
            depth++;
            Accept(obj.right);
            o.Write(",\n");
            o.WriteIndent(depth);
            o.Write($"\"{obj.operation}\",\n");
            Accept(obj.left);
            depth--;
            o.Write("\n");
            o.WriteIndent(depth);
            o.Write(")");
        }
        public void VisitNumber(Number number)
        {
            o.WriteIndent(depth);
            o.Write($"new { nameof(Number)}(\"{number.number}\")");
        }
        public void VisitIdentifier(Identifier identifier)
        {
            o.WriteIndent(depth);
            o.Write($"new { nameof(Identifier)}(\"{identifier.Name}\")");
        }

        public void VisitParentheses(Parentheses parentheses)
        {
            o.WriteIndent(depth);
            o.Write($"new { nameof(Parentheses)}(\n");
            Accept(parentheses.child);
            o.Write("\n");
            o.WriteIndent(depth);
            o.Write(")");
        }
    }
}
