using System;
using System.Globalization;
using System.Linq;
namespace Lab5.Interpreting.Values.Functions
{
    sealed class DumpFunction : ICallable, IDumpable, IReferenceEquatable
    {
        public object Call(object[] args)
        {
            Console.WriteLine(string.Join(" ", args.Select(ValueToString)));
            return this;
        }
        public string GetDumpString()
        {
            return "dump";
        }
        public static string ValueToString(object value)
        {
            if (value == null)
            {
                return "null";
            }
            if (value is bool)
            {
                return (bool)value ? "true" : "false";
            }
            if (value is int)
            {
                return ((int)value).ToString(NumberFormatInfo.InvariantInfo);
            }
            if (value is IDumpable)
            {
                return ((IDumpable)value).GetDumpString();
            }
            if (value is object[])
            {
                if (((object[])value).Length == 1)
                    return ((object[])value)[0].ToString();
				if (((object[])value).Length == 0)
                    return "[]";
                string text = "[" + ((object[])value)[0].ToString();

                for (int i = 1; i < ((object[])value).Length; i++)
                {
                    text += "," + ValueToString(((object[])value)[i]);
                }
                text += "]";
                return text;
            }
            throw new Exception($"Неизвестный тип значения {value}");
        }
    }
}
