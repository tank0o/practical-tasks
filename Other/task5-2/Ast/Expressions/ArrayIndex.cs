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
		public readonly ArrayIndex array;
		public readonly IExpression r;
		public readonly IExpression l;

		public ArrayIndex(string variable, IExpression r, IExpression l)
		{
			this.variable = variable;
			this.r = r;
			this.l = l;
		}

		public ArrayIndex(ArrayIndex array, IExpression r, IExpression l)
		{
			this.array = array;
			this.r = r;
			this.l = l;
		}

		public string FormattedString => $"{variable}[{r} {(l != r ? (":") : (""))} {l}];";

		public int Position => 0;

		public void Accept(IExpressionVisitor visitor) => visitor.VisitArrayIndex(this);
		public T Accept<T>(IExpressionVisitor<T> visitor) => visitor.VisitArrayIndex(this);
	}
}
