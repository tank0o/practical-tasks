namespace Lab5.Ast.Statements {
	sealed class Assignment : IStatement {
		public readonly string Variable;
		public readonly IExpression Expr;
		public string FormattedString => $"{Variable} = {Expr.FormattedString};\n";
		public Assignment(string variable, IExpression expr) {
			Variable = variable;
			Expr = expr;
		}
		public void Accept(IStatementVisitor visitor) => visitor.VisitAssignment(this);
		public T Accept<T>(IStatementVisitor<T> visitor) => visitor.VisitAssignment(this);
	}
}
