namespace Lab5.Ast.Expressions {
	sealed class Parentheses : IExpression {
		public int Position { get; }
		public readonly IExpression Expr;
		public Parentheses(int position, IExpression expr) {
			Position = position;
			Expr = expr;
		}
		public string FormattedString => $"({Expr.FormattedString})";
		public void Accept(IExpressionVisitor visitor) => visitor.VisitParentheses(this);
		public T Accept<T>(IExpressionVisitor<T> visitor) => visitor.VisitParentheses(this);
	}
}
