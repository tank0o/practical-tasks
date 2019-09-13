using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Interpreting.Values.Functions
{
	class LenFunction : ICallable, IDumpable, IReferenceEquatable
	{
		public object Call(object[] args)
		{
			if (args.Length == 1)
				return ((object[])args[0]).Length;
			else return -1;
		}

		public string GetDumpString()
		{
			return "len";
		}
	}
}
