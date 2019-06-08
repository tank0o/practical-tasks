using System.Collections.Generic;
using System.Linq;
namespace Lab5.Ast {
	sealed class Block : INode {
		public readonly IReadOnlyList<IStatement> Statements;
		public Block(IReadOnlyList<IStatement> statements) {
			Statements = statements;
		}
		public string FormattedString => "{\n" + string.Join("", Statements.Select(x => x.FormattedString)) + "}\n";
	}

    sealed class PreBlock : INode
    {
        public readonly IReadOnlyList<IStatement> Statements;
        public PreBlock(IReadOnlyList<IStatement> statements)
        {
            Statements = statements;
        }
        public string FormattedString => "\n" + string.Join("", Statements.Select(x => x.FormattedString)) + "\n";
    }
}
