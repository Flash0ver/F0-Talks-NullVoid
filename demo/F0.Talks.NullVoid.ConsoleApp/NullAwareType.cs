#nullable enable

namespace F0.Talks.NullVoid.ConsoleApp;

internal sealed class NullAwareType
{
	public NullAwareType()
	{
	}

	public NullAwareType(int number, string text)
	{
		Number = number;
		Text = text;
	}

	public int Number { get; set; }
	public string? Text { get; set; }
}
