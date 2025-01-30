using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace F0.Talks.NullVoid.Tests.Contracts;

public class MessageTests
{
	[Fact]
	public void Serialize()
	{
		Message message = new()
		{
			Id = Guid.Empty,
			RequiredText = "Null & Void",
			OptionalText = null,
			RequiredNumber = 0x_F0,
			OptionalNumber = null,
		};

		string actual = JsonConvert.SerializeObject(message, Formatting.Indented);

		string expected = """
			{
			  "RequiredText": "Null & Void",
			  "OptionalText": null,
			  "RequiredNumber": 240,
			  "OptionalNumber": null
			}
			""";

		actual.Should().Be(expected);
	}

	[Fact]
	public void Deserialize()
	{
		string json = """
			{
			  "RequiredText": "Null & Void",
			  "RequiredNumber": 240
			}
			""";

		Message? actual = JsonConvert.DeserializeObject<Message>(json);

		Message expected = new()
		{
			Id = Guid.Empty,
			RequiredText = "Null & Void",
			OptionalText = null,
			RequiredNumber = 0x_F0,
			OptionalNumber = null,
		};

		actual.Should().Be(expected);
	}
}
