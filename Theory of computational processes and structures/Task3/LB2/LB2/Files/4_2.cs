using System;
using System.IO;

static class TextWriterExtensions
{
    public static void WriteIndent(this TextWriter o, int depth)
    {
        o.Write(new string(' ', depth * 2));
    }
}
interface INode
{
    string ToFormattedString();
    void DebugPrint(TextWriter o, int depth);
}
interface ITableOrSubquery : INode { }
sealed class Table : ITableOrSubquery
{
    public readonly string SchemaName;
    public readonly string TableName;
    public Table(string schemaName, string tableName)
    {
        SchemaName = schemaName;
        TableName = tableName;
    }
    public string ToFormattedString() =>
        $"{(SchemaName == null ? "" : SchemaName + ".")}{TableName}";
    public void DebugPrint(TextWriter o, int depth)
    {
        o.WriteIndent(depth);
        o.Write($"new Table(");
        if (SchemaName == null)
        {
            o.Write("null");
        }
        else
        {
            o.Write('"' + SchemaName + '"');
        }
        o.Write($", \"{TableName}\")");
    }
}
sealed class Subquery : ITableOrSubquery
{
    public readonly SelectStmt SelectStmt;
    public Subquery(SelectStmt selectStmt)
    {
        SelectStmt = selectStmt;
    }
    public string ToFormattedString() =>
        $"({SelectStmt.ToFormattedString()})";
    public void DebugPrint(TextWriter o, int depth)
    {
        o.WriteIndent(depth);
        o.Write("new Subquery(\n");
        SelectStmt.DebugPrint(o, depth + 1);
        o.Write("\n");
        o.WriteIndent(depth);
        o.Write(")");
    }
}
sealed class SelectStmt : INode
{
    public readonly int Column;
    public readonly ITableOrSubquery From;
    public SelectStmt(int column, ITableOrSubquery from)
    {
        Column = column;
        From = from;
    }
    public string ToFormattedString() =>
        $"SELECT {Column} FROM {From.ToFormattedString()}";
    public void DebugPrint(TextWriter o, int depth)
    {
        o.WriteIndent(depth);
        o.Write("new SelectStmt(\n");
        o.WriteIndent(depth + 1);
        o.Write($"{Column},\n");
        From.DebugPrint(o, depth + 1);
        o.Write("\n");
        o.WriteIndent(depth);
        o.Write(")");
    }
}
static class NodeExtensions
{
    public static string ToDebugString(this INode node)
    {
        using (var o = new StringWriter())
        {
            node.DebugPrint(o, 0);
            o.Write("\n");
            return o.ToString();
        }
    }
}
static class Program
{
    static void Main()
    {
        // SELECT 1 FROM (SELECT 2 FROM (SELECT 3 FROM t1.t2))
        var tree = new SelectStmt(
            1,
            new Subquery(
                new SelectStmt(
                    2,
                    new Subquery(
                        new SelectStmt(
                            3,
                            new Table("t1", "t2")
                            )))));
        // Отформатированная строка
        Console.WriteLine(tree.ToFormattedString());
        // Отладочная строка
        Console.WriteLine(tree.ToDebugString());
    }
}
