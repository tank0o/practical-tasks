namespace Lab5.Ast.Expressions {
	sealed class MemberAccess : IExpression {
		public int Position { get; }
		public readonly IExpression Obj;
		public readonly string Member;
		public MemberAccess(int position, IExpression obj, string member) {
			Position = position;
			Obj = obj;
			Member = member;
		}
		public string FormattedString => $"{Obj.FormattedString}.{Member}";
		public void Accept(IExpressionVisitor visitor) => visitor.VisitMemberAccess(this);
		public T Accept<T>(IExpressionVisitor<T> visitor) => visitor.VisitMemberAccess(this);
	}
}
