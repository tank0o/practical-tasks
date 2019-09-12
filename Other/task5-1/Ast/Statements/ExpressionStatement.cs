namespace Lab5.Ast.Statements {
	sealed class ExpressionStatement : IStatement {
		public readonly IExpression Expr;
		public ExpressionStatement(IExpression expr) {
			Expr = expr;
		}
		public string FormattedString => $"{Expr.FormattedString};\n";
		public void Accept(IStatementVisitor visitor) => visitor.VisitExpressionStatement(this);
		public T Accept<T>(IStatementVisitor<T> visitor) => visitor.VisitExpressionStatement(this);
	}
}
