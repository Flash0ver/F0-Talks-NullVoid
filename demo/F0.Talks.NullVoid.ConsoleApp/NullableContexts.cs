#nullable enable

// ReSharper disable All

namespace F0.Talks.NullVoid.ConsoleApp;

public sealed class NullableContexts
{
#nullable disable warnings
	public NullableContexts()
	{
	}
#nullable restore warnings

	public NullableContexts(string required)
	{
		RequiredText = required;
	}

	public NullableContexts(string required, string? optional)
	{
		RequiredText = required;
		OptionalText = optional;
	}

	public string RequiredText { get; set; }
	public string? OptionalText { get; set; }
}
