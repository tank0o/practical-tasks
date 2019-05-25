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
        void Visit(SelectStmt obj);
        void Visit(DescAsc obj);
        void Visit(Binary obj);
        void Visit(Number number);
        void Visit(Identifier identifier);
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
        public void Visit(SelectStmt obj)
        {
            o.WriteIndent(depth);
            o.Write("new SelectStmt(\n");
            o.WriteIndent(depth + 1);
            o.Write("new string[] {\n");
            for (int i = 0; i < obj.columns.Count; i++)
            {
                if (i > 0)
                {
                    o.Write(",\n");
                }
                o.WriteIndent(depth + 2);
                o.Write("\"" + obj.columns[i] + "\"");
            }
            o.Write("\n");
            o.WriteIndent(depth + 1);
            o.Write("},\n");
            o.WriteIndent(depth + 1);
            o.Write("\"" + obj.fromTable + "\"");
            o.Write(",\n");
            o.WriteIndent(depth + 1);
            o.Write("new DescAsc[] {\n");
            for (int i = 0; i < obj.orderByColumns.Count; i++)
            {
                if (i > 0)
                {
                    o.Write(",\n");
                }
                obj.orderByColumns[i].DebugPrint(o, depth + 2);
            }
            o.Write("\n");
            o.WriteIndent(depth + 1);
            o.Write("}\n");
            o.WriteIndent(depth);
            o.Write(")");
        }
        public void Visit(DescAsc obj)
        {
            string descAscStr = "";
            if (obj.descAsc == EDescAsc.asc)
                descAscStr = "EDescAsc.asc";
            else if (obj.descAsc == EDescAsc.desc)
                descAscStr = "EDescAsc.desc";
            else descAscStr = "EDescAsc.NULL";


            o.WriteIndent(depth);
            o.Write("new DescAsc(\n");
            obj.expression.DebugPrint(o, depth + 1);
            o.Write(",\n");
            o.WriteIndent(depth + 1);
            o.Write(descAscStr);
            o.Write("\n");
            o.WriteIndent(depth);
            o.Write(")");
        }
        public void Visit(Binary obj)
        {
            o.WriteIndent(depth);
            o.Write("new Binary(\n");
            Accept(obj.right);
            o.Write(",\n");
            o.WriteIndent(depth + 1);
            o.Write($"\"{obj.operation}\",\n");
            Accept(obj.left);
            o.Write("\n");
            o.WriteIndent(depth);
            o.Write(")");
        }
        public void Visit(Number number)
        {
            o.WriteIndent(depth);
            o.Write($"new Number(\"{number.number}\")");
        }
        public void Visit(Identifier identifier)
        {
            o.WriteIndent(depth);
            o.Write($"new Identifier(\"{identifier.Name}\")");
        }
    }
}
