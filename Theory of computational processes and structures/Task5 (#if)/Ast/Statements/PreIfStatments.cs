using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Ast.Statements
{
    sealed class PreIfStatements : IStatement
    {
        public IReadOnlyList<Tuple<string, PreBlock>> preIfStatements;

        public PreIfStatements(IReadOnlyList<Tuple<string, PreBlock>> preIfStatements)
        {
            this.preIfStatements = preIfStatements;
        }

        public string FormattedString
        {
            get
            {
                string text = "\n#if " + preIfStatements[0].Item1;
                text += preIfStatements[0].Item2.FormattedString;
                for (int i = 1; i < preIfStatements.Count; i++)
                {
                    if (preIfStatements[i].Item1 != null)
                    {
                        text += "\n#elseif " + preIfStatements[i].Item1;
                        text += preIfStatements[i].Item2.FormattedString;
                    }
                    else
                    {
                        text += "\n#else ";
                        text += preIfStatements[i].Item2.FormattedString;
                    }
                }
                text += "\n#endif";
                return text;
            }
        }
        //public string FormattedString => $"if";

        // => "\n" + string.Join("", preIfStatements.Select(x => x.Item2.FormattedString)) + "\n";
        public void Accept(IStatementVisitor visitor) => visitor.VisitPreIf(this);
        public T Accept<T>(IStatementVisitor<T> visitor) => visitor.VisitPreIf(this);
    }
}
