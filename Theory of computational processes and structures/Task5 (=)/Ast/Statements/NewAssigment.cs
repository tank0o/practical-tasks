using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Ast.Statements
{
    sealed class NewAssignment : IStatement
    {
        public readonly string Variable;
        public readonly IExpression Expr;
        public string FormattedString => $"{Variable} = {Expr.FormattedString};\n";
        public NewAssignment(string variable, IExpression expr)
        {
            Variable = variable;
            Expr = expr;
        }
        public void Accept(IStatementVisitor visitor) => visitor.VisitNewAssignment(this);
        public T Accept<T>(IStatementVisitor<T> visitor) => visitor.VisitNewAssignment(this);
    }
}
