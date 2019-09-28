using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Ast.Statements
{
    class ArrayIndex : IExpression
    {
        public readonly IExpression variable;
        public readonly IExpression right;
        public readonly IExpression left;
        public readonly bool range = false;

		public ArrayIndex(IExpression variable, IExpression left)
		{
			range = false;
			this.variable = variable;
			this.left = left;
		}

		public ArrayIndex(IExpression variable, IExpression left, IExpression right)
		{
			this.range = true;
			this.variable = variable;
			this.right = right;
			this.left = left;
		}

		public string FormattedString => $"{variable.FormattedString}[{(left!=null?(left.FormattedString):(""))} {(range?(":"):(""))} {(right!=null?(right.FormattedString):(""))}]";


		public int Position => 0;

        public void Accept(IExpressionVisitor visitor) => visitor.VisitArrayIndex(this);
        public T Accept<T>(IExpressionVisitor<T> visitor) => visitor.VisitArrayIndex(this);
    }
}
