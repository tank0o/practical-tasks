using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Ast.Statements
{
    class ArrayIndex : IExpression
    {
        public readonly string variable;
        public readonly int right;
        public readonly bool range;
        public readonly int left;

        public ArrayIndex(string variable, int right, bool range, int left)
        {
            this.variable = variable;
            this.right = right;
            this.range = range;
            this.left = left;
        }

        public string FormattedString => $"{variable}[{right} {(range?(":"):(""))} {left}];";

        public int Position => 0;

        public void Accept(IExpressionVisitor visitor) => visitor.VisitArrayIndex(this);
        public T Accept<T>(IExpressionVisitor<T> visitor) => visitor.VisitArrayIndex(this);
    }
}
