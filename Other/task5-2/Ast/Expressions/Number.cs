namespace Lab5.Ast.Expressions {
	sealed class Number : IExpression {
		public int Position { get; }
		public readonly string Lexeme;
		public Number(int position, string lexeme) {
			Position = position;
			Lexeme = lexeme;
		}
		public string FormattedString => Lexeme;
		public void Accept(IExpressionVisitor visitor) => visitor.VisitNumber(this);
		public T Accept<T>(IExpressionVisitor<T> visitor) => visitor.VisitNumber(this);
	}
}
