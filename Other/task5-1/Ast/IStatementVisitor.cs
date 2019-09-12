using Lab5.Ast.Statements;
namespace Lab5.Ast {
	interface IStatementVisitor {
		void VisitIf(IfStatement ifStatement);
		void VisitWhile(WhileStatement whiteStatement);
		void VisitExpressionStatement(ExpressionStatement expressionStatement);
		void VisitAssignment(Assignment assignment);
	}
	interface IStatementVisitor<T> {
		T VisitIf(IfStatement ifStatement);
		T VisitWhile(WhileStatement whiteStatement);
		T VisitExpressionStatement(ExpressionStatement expressionStatement);
		T VisitAssignment(Assignment assignment);
	}
}
