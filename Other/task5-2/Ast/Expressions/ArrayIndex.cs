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

		public string FormattedString => MyFormattedString();
		public int Position => 0;

		string MyFormattedString()
		{
			string text = variable.FormattedString + '[' + l.FormattedString;
			if(r!=null && l != r)
				text += ":" + (r.FormattedString);
			text += ']';
			return text;
		}

		public void Accept(IExpressionVisitor visitor) => visitor.VisitArrayIndex(this);
		public T Accept<T>(IExpressionVisitor<T> visitor) => visitor.VisitArrayIndex(this);
	}
}
