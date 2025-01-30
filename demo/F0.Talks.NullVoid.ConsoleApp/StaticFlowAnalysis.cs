#nullable enable

namespace F0.Talks.NullVoid.ConsoleApp;

public static class StaticFlowAnalysis
{
	public static void Flow()
		=> Flow(new MyType());

	public static void Flow(MyType instance)
	{
		Console.WriteLine(instance.Text.Length);
	}

	public static void Set(MyType instance)
	{
		instance.Text = null!;
	}
}

public sealed class MyType
{
	public string Text { get; set; }

	public MyType() { }
	public MyType(string text) => Text = text;
}
