namespace Lab5.Ast {
	interface IExpression : INode {
		int Position { get; }
		void Accept(IExpressionVisitor visitor);
		T Accept<T>(IExpressionVisitor<T> visitor);
	}
}
