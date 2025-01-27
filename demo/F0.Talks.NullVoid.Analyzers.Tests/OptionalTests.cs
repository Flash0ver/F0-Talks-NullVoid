using FluentAssertions;
using Microsoft.CodeAnalysis;
using Xunit;

#nullable disable

namespace F0.Talks.NullVoid.Analyzers.Tests;

public class OptionalTests
{
	[Fact]
	public void Optional_NoValue()
	{
		Optional<string> optional = default;

		optional.HasValue.Should().BeFalse();
		optional.Value.Should().BeNull();
	}

	[Fact]
	public void Optional_WithValue()
	{
		Optional<string> optional = "Text";

		optional.HasValue.Should().BeTrue();
		optional.Value.Should().Be("Text");
	}
}
