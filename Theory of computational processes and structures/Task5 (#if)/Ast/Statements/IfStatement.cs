namespace Lab5.Ast.Statements {
	sealed class IfStatement : IStatement {
		public readonly IExpression Condition;
		public readonly Block Body;
		public string FormattedString => $"if ({Condition.FormattedString}) {Body.FormattedString}";
		public IfStatement(IExpression condition, Block body) {
			Condition = condition;
			Body = body;
		}
		public void Accept(IStatementVisitor visitor) => visitor.VisitIf(this);
		public T Accept<T>(IStatementVisitor<T> visitor) => visitor.VisitIf(this);
	}
}
