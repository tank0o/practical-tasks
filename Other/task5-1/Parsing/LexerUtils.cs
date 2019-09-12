using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Lab5.Parsing {
	static class LexerUtils {
		static readonly Regex newLine = RegexUtils.CreateRegex(@"(?<=\r\n|\n)");
		static readonly Regex nonTab = RegexUtils.CreateRegex(@"[^\t]");
		public static IEnumerable<string> FormatLines(
			string source, int offset,
			int linesAround = 1,
			bool inlinePointer = false,
			string pointer = "^",
			int maxLineNumberLength = 5
			) {
			var lines = newLine.Split(source);
			var lineOffset = 0;
			var lineIndex = 0;
			var columnIndex = 0;
			foreach (var line in lines) {
				if (offset < lineOffset + line.Length) {
					columnIndex = offset - lineOffset;
					break;
				}
				lineOffset += line.Length;
				lineIndex += 1;
			}
			for (var i = -linesAround; i <= linesAround; i++) {
				var j = lineIndex + i;
				if (!(0 <= j && j < lines.Length)) {
					continue;
				}
				var line = lines[j].TrimEnd();
				var lineNumber = $"{j + 1}:".PadRight(maxLineNumberLength).Substring(0, maxLineNumberLength);
				var empty = new string(' ', maxLineNumberLength);
				if (i == 0) {
					if (inlinePointer) {
						yield return lineNumber + line.Substring(0, columnIndex) + pointer + line.Substring(columnIndex);
					}
					else {
						yield return lineNumber + line;
						yield return new string(' ', maxLineNumberLength) + nonTab.Replace(lines[lineIndex].Substring(0, columnIndex), " ") + pointer;
					}
				}
				else {
					yield return lineNumber + line;
				}
			}
		}
		public static string MakeErrorMessage(string source, int offset, string message) {
			return message + "\n" + string.Join("\n", FormatLines(source, offset));
		}
	}
}
