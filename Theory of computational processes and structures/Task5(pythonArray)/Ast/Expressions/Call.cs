using System.Collections.Generic;
using System.Linq;
namespace Lab5.Ast.Expressions {
	sealed class Call : IExpression {
		public int Position { get; }
		public readonly IExpression Function;
		public readonly IReadOnlyList<IExpression> Arguments;
		public string FormattedString => $"{Function.FormattedString}({string.Join(", ", Arguments.Select(x => x.FormattedString))})";
		public Call(int position, IExpression function, IReadOnlyList<IExpression> arguments) {
			Position = position;
			Function = function;
			Arguments = arguments;
		}
		public void Accept(IExpressionVisitor visitor) => visitor.VisitCall(this);
		public T Accept<T>(IExpressionVisitor<T> visitor) => visitor.VisitCall(this);
	}
}
