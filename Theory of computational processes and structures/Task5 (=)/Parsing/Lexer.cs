using System;
using System.Collections.Generic;
namespace Lab5.Parsing {
	static class Lexer {
		public static IEnumerable<Token> GetTokens(string text) {
			var regex = Regexes.Instance.CombinedRegex;
			var groupNames = Regexes.Instance.TokenGroupNames;
			int lastPos = 0;
			Func<string, Exception> MakeError = (message) => {
				return new Exception(LexerUtils.MakeErrorMessage(text, lastPos, message));
			};
			for (var m = regex.Match(text); m.Success; m = m.NextMatch()) {
				if (lastPos < m.Index) {
					throw MakeError($"Пропустили '{text.Substring(lastPos, m.Index - lastPos)}'");
				}
				bool found = false;
				foreach (var kv in groupNames) {
					if (m.Groups[kv.Item2].Success) {
						if (found) {
							throw new Exception("Кривая регулярка нашла несколько вхождений");
						}
						found = true;
						yield return new Token(kv.Item1, m.Value, m.Index);
					}
				}
				if (!found) {
					throw new Exception("Кривая регулярка");
				}
				lastPos = m.Index + m.Length;
			}
			if (lastPos < text.Length) {
				throw MakeError($"Пропустили '{text.Substring(lastPos)}'");
			}
		}
	}
}
