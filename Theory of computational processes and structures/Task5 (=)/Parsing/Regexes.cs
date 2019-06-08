using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Lab5.Parsing {
	sealed class Regexes {
		readonly IReadOnlyList<Tuple<TokenType, string>> tokenPatterns = new TupleList<TokenType, string> {
			{ TokenType.Whitespaces, @"[ \t\r\n]+" },
			{ TokenType.SingleLineComment, @"//[^\r\n]*" },
			{ TokenType.MultiLineComment, @"/\*.*?\*/" },
			{ TokenType.Identifier, @"[a-zA-Z_][a-zA-Z_0-9]*" },
			{ TokenType.NumberLiteral, @"[0-9]+" },
			{ TokenType.OperatorOrPunctuator, @":=|==|[-+*/%.<,=;(){}[\]]" },
		};
		public readonly Regex CombinedRegex;
		public readonly IEnumerable<Tuple<TokenType, string>> TokenGroupNames;
		public readonly IReadOnlyDictionary<TokenType, Regex> TokenRegexes;
		Regexes() {
			var tokenRegexes = new Dictionary<TokenType, Regex>();
			foreach (var tp in tokenPatterns) {
				var tokenType = tp.Item1;
				var pattern = tp.Item2;
				var regex = RegexUtils.CreateRegex(@"\A(" + pattern + @")\z");
				tokenRegexes.Add(tokenType, regex);
			}
			TokenRegexes = tokenRegexes;
			var combinedPattern = string.Join("\n|\n", tokenPatterns.Select(x => $"(?<{x.Item1}>{x.Item2})"));
			CombinedRegex = RegexUtils.CreateRegex(combinedPattern);
			TokenGroupNames = tokenPatterns.Select(x => Tuple.Create(x.Item1, x.Item1.ToString())).ToArray();
		}
		static Regexes instance;
		public static Regexes Instance => instance ?? (instance = new Regexes());
	}
}
