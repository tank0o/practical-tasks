using System.Collections.Generic;
namespace Lab5.Ast.Expressions {
	sealed class Binary : IExpression {
		public int Position { get; }
		public readonly IExpression Left;
		public readonly BinaryOperator Operator;
		public readonly IExpression Right;
		public Binary(int position, IExpression left, BinaryOperator op, IExpression right) {
			Position = position;
			Left = left;
			Operator = op;
			Right = right;
		}
		static readonly IReadOnlyDictionary<BinaryOperator, string> operators = new Dictionary<BinaryOperator, string>{
			{ BinaryOperator.Addition, "+" },
			{ BinaryOperator.Subtraction, "-" },
			{ BinaryOperator.Multiplication, "*" },
			{ BinaryOperator.Division, "/" },
			{ BinaryOperator.Remainder, "%" },
			{ BinaryOperator.Equal, "==" },
			{ BinaryOperator.Less, "<" },
		};
		public string OperatorString => operators[Operator];
		public string FormattedString => $"{Left.FormattedString} {OperatorString} {Right.FormattedString}";
		public void Accept(IExpressionVisitor visitor) => visitor.VisitBinary(this);
		public T Accept<T>(IExpressionVisitor<T> visitor) => visitor.VisitBinary(this);
	}
}
