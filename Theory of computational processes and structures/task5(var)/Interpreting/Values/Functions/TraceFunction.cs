using System;
namespace Lab5.Interpreting.Values.Functions {
	sealed class TraceFunction : ICallable, IDumpable, IReferenceEquatable {
		public object Call(object[] args) {
			if (args.Length != 1) {
				throw new Exception($"Нужен 1 аргумент, а не {args.Length}: {string.Join(", ", args)}");
			}
			Console.WriteLine($">> {DumpFunction.ValueToString(args[0])}");
			return args[0];
		}
		public string GetDumpString() {
			return "trace";
		}
	}
}
