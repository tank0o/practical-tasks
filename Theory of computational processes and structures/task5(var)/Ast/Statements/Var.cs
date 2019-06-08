using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Ast.Statements
{
    class Var : IStatement
    {
        public readonly Assignment Assignment;

        public Var(Assignment assignment)
        {
            this.Assignment = assignment;
        }

        public string FormattedString => $"var {Assignment.FormattedString}\n";

        
        public void Accept(IStatementVisitor visitor) => visitor.VisitVar(this);
        public T Accept<T>(IStatementVisitor<T> visitor) => visitor.VisitVar(this);
    }
}
