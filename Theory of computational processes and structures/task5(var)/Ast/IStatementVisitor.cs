using Lab5.Ast.Statements;
namespace Lab5.Ast {
	interface IStatementVisitor {
		void VisitIf(IfStatement ifStatement);
		void VisitWhile(WhileStatement whiteStatement);
		void VisitExpressionStatement(ExpressionStatement expressionStatement);
        void VisitAssignment(Assignment assignment);
        void VisitVar(Var var);
    }
	interface IStatementVisitor<T> {
		T VisitIf(IfStatement ifStatement);
		T VisitWhile(WhileStatement whiteStatement);
		T VisitExpressionStatement(ExpressionStatement expressionStatement);
		T VisitAssignment(Assignment assignment);
        T VisitVar(Var var);
    }
}
