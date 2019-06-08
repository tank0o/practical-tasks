namespace Lab5.Ast.Expressions {
	sealed class Identifier : IExpression {
		public int Position { get; }
		public readonly string Name;
		public Identifier(int position, string name) {
			Position = position;
			Name = name;
		}
		public string FormattedString => Name;
		public void Accept(IExpressionVisitor visitor) => visitor.VisitIdentifier(this);
		public T Accept<T>(IExpressionVisitor<T> visitor) => visitor.VisitIdentifier(this);
	}
}
