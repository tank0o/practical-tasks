using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Ast.Statements
{
    sealed class AssignmentMas : IStatement
    {
        public readonly string Variable;
        public readonly IExpression[] Expr;
        public string FormattedString => $"{Variable} = {string.Join(",", Expr.Select(x => x.FormattedString))};\n";
        public AssignmentMas(string variable, IExpression[] expr)
        {
            Variable = variable;
            Expr = expr;
        }
        public void Accept(IStatementVisitor visitor) => visitor.VisitAssignmentMas(this);
        public T Accept<T>(IStatementVisitor<T> visitor) => visitor.VisitAssignmentMas(this);
    }
}
