using System;
using System.Collections.Generic;
namespace Lab5.Parsing {
	sealed class TupleList<T1, T2> : List<Tuple<T1, T2>> {
		public void Add(T1 item1, T2 item2) {
			Add(Tuple.Create(item1, item2));
		}
	}
}
