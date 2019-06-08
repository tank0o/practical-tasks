using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_2
{
    public interface INodeVisitor<T>
    {
        T VisitSelectStmt(SelectStmt2 obj);
        T VisitDescAsc(DescAsc obj);
        T VisitBinary(Binary obj);
        T VisitNumber(Number number);
        T VisitIdentifier(Identifier identifier);
        T VisitParentheses(Parentheses parentheses);
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
        public string VisitSelectStmt(SelectStmt2 obj)
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
        public string VisitDescAsc(DescAsc obj)
        {
            string descAscStr = "";
            if (obj.descAsc == EDescAsc.asc)
                descAscStr = "asc";
            else if (obj.descAsc == EDescAsc.desc)
                descAscStr = "desc";
            return
                $"{obj.expression.ToFormattedString()} {descAscStr }";
        }
        public string VisitBinary(Binary obj)
        {
            return
                $"{Accept(obj.right)}{obj.operation}{Accept(obj.left)}";
        }
        public string VisitNumber(Number number)
        {
            return number.number;
        }
        public string VisitIdentifier(Identifier identifier)
        {
            return identifier.Name;
        }

        public string VisitParentheses(Parentheses parentheses)
        {
            return $"({Accept(parentheses.child)})";
        }
    }
}
