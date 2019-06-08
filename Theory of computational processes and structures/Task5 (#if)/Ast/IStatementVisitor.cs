using Lab5.Ast.Statements;
namespace Lab5.Ast {
	interface IStatementVisitor {
		void VisitIf(IfStatement ifStatement);
		void VisitWhile(WhileStatement whiteStatement);
		void VisitExpressionStatement(ExpressionStatement expressionStatement);
        void VisitAssignment(Assignment assignment);
        void VisitPreAssignment(PreAssignment preAssignment);
        void VisitPreIf(PreIfStatements preIfStatements);
	}
	interface IStatementVisitor<T> {
		T VisitIf(IfStatement ifStatement);
		T VisitWhile(WhileStatement whiteStatement);
		T VisitExpressionStatement(ExpressionStatement expressionStatement);
		T VisitAssignment(Assignment assignment);
        T VisitPreIf(PreIfStatements preIfStatements);
        T VisitPreAssignment(PreAssignment preAssignment);
    }
}
