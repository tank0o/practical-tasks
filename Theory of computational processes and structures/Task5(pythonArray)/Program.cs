using Lab5.Ast;
using Lab5.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
namespace Lab5 {
	sealed class Program {
		static ProgramNode CheckedParse(string code) {
			var programNode = Parser.Parse(code);
			//var code2 = programNode.FormattedString;
			//var programNode2 = Parser.Parse(code2);
			//var code3 = programNode2.FormattedString;
			//if (code2 != code3) {
			//	Console.WriteLine(code2);
			//	Console.WriteLine(code3);
			//	throw new Exception($"Кривой парсер или {nameof(INode.FormattedString)} у узлов");
			//}
			return programNode;
		}
		static void Main(string[] args) {
			string code = File.ReadAllText("../../code.txt");
			var programNode = CheckedParse(code);
			new Interpreting.Interpreter(code, new Dictionary<string, object> {
				{ "x", 2 },
				{ "y", 10 },
				{ "z", 4 },
			}).RunProgram(programNode);
		}
	}
}
