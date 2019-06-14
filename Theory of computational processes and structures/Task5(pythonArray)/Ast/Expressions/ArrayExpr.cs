using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Ast.Expressions
{
    class ArrayExpr : IExpression
    {
        public IReadOnlyList<IExpression> Expr;

        public ArrayExpr(List<IExpression> expr)
        {
            Expr = expr;
        }

        public string FormattedString => $"[{string.Join(",", Expr.Select(x => x.FormattedString))}]\n";

        public int Position => 0;

        public void Accept(IExpressionVisitor visitor) => visitor.VisitArrayExpr(this);
        public T Accept<T>(IExpressionVisitor<T> visitor) => visitor.VisitArrayExpr(this);
    }
}
