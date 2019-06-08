namespace Lab5.Ast.Statements {
	sealed class WhileStatement : IStatement {
		public readonly IExpression Condition;
		public readonly Block Body;
		public string FormattedString => $"while ({Condition.FormattedString}) {Body.FormattedString}";
		public WhileStatement(IExpression condition, Block body) {
			Condition = condition;
			Body = body;
		}
		public void Accept(IStatementVisitor visitor) => visitor.VisitWhile(this);
		public T Accept<T>(IStatementVisitor<T> visitor) => visitor.VisitWhile(this);
	}
}
