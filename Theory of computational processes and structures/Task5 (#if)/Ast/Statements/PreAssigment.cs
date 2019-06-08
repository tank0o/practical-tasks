using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Ast.Statements
{
    sealed class PreAssignment : IStatement
    {
        public readonly string Variable;
        public readonly bool Expr;
        //public string FormattedString => $"{Variable} = {Expr};\n";
        public string FormattedString => Expr ? $"#define {Variable}\n" : $"#undef {Variable}\n";

        public PreAssignment(string variable, bool expr)
        {
            Variable = variable;
            Expr = expr;
        }
        public void Accept(IStatementVisitor visitor) => visitor.VisitPreAssignment(this);
        public T Accept<T>(IStatementVisitor<T> visitor) => visitor.VisitPreAssignment(this);
    }
}
