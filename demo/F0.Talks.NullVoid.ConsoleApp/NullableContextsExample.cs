#nullable enable

// ReSharper disable All

namespace F0.Talks.NullVoid.ConsoleApp;

public static class NullableContextsExample
{
	public static void Run() => Run(new NullableContexts());

	public static void Run(NullableContexts instance)
	{
		Console.WriteLine(instance.RequiredText.Length);
		Console.WriteLine(instance.OptionalText.Length);
	}
}
