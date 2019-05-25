using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_2
{
    public interface INodeVisitor<T>
    {
        T Visit(SelectStmt obj);
        T Visit(DescAsc obj);
        T Visit(Binary obj);
        T Visit(Number number);
        T Visit(Identifier identifier);
    }
    public interface IAcceptFormatted<T>
    {
        T Accept(INodeVisitor<T> v);
    }

    public class ToFormattedString : INodeVisitor<string>
    {
        string Accept(INode node)
        {
            return node.Accept(this);
        }
        public string Visit(SelectStmt obj)
        {
            string columnsString = obj.columns[0];
            for (int i = 1; i < obj.columns.Count; i++)
                columnsString += ", " + obj.columns[i];

            string orderByColumnsString = obj.orderByColumns[0].ToFormattedString();
            for (int i = 1; i < obj.orderByColumns.Count; i++)
                orderByColumnsString += ", " + obj.orderByColumns[i].ToFormattedString();

            return
                $"SELECT {columnsString} FROM {obj.fromTable} ORDER BY {orderByColumnsString}";
        }
        public string Visit(DescAsc obj)
        {
            string descAscStr = "";
            if (obj.descAsc == EDescAsc.asc)
                descAscStr = "asc";
            else if (obj.descAsc == EDescAsc.desc)
                descAscStr = "desc";
            return
                $"{obj.expression.ToFormattedString()} {descAscStr }";
        }
        public string Visit(Binary obj)
        {
            return
                $"{Accept(obj.left)}{obj.operation}{Accept(obj.right)}";
        }
        public string Visit(Number number)
        {
            return number.number;
        }
        public string Visit(Identifier identifier)
        {
            return identifier.Name;
        }
    }
}
