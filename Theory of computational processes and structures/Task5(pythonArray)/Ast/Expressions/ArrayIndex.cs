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
        public readonly IExpression right;
        public readonly bool range;
        public readonly IExpression left;

        public ArrayIndex(string variable, IExpression right, bool range, IExpression left)
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
