// ReSharper disable All

namespace F0.Talks.NullVoid.ConsoleApp;

public class Class
{
	public void Method1(string? first) { }

	public void Method2(string first,
		string? second) { }

	public void Method3(string[] first,
		string[]? second,
		string?[] third,
		string?[]? fourth) { }
}
