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
		public readonly IExpression r;
		public readonly IExpression l;

		public ArrayIndex(IExpression variable, IExpression l)
		{
			this.variable = variable;
			this.l = l;
		}

		public ArrayIndex(IExpression variable, IExpression l, IExpression r)
		{
			this.variable = variable;
			this.r = r;
			this.l = l;
		}

		public string FormattedString => $"{variable.FormattedString}[{l.FormattedString}{(r != null && l != r  ? (":"+ (r.FormattedString)) : (""))}]";

		public int Position => 0;

		public void Accept(IExpressionVisitor visitor) => visitor.VisitArrayIndex(this);
		public T Accept<T>(IExpressionVisitor<T> visitor) => visitor.VisitArrayIndex(this);
	}
}
