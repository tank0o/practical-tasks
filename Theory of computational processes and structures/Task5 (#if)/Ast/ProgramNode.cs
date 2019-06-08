using System.Collections.Generic;
using System.Linq;
namespace Lab5.Ast {
	sealed class ProgramNode : INode {
		public readonly IReadOnlyList<IStatement> Statements;
		public ProgramNode(IReadOnlyList<IStatement> statements) {
			Statements = statements;
		}
		public string FormattedString => string.Join("", Statements.Select(x => x.FormattedString));
	}
}
