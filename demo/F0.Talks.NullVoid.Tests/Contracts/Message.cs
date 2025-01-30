using System.Runtime.Serialization;
// ReSharper disable All

#nullable enable annotations
#nullable disable warnings

namespace F0.Talks.NullVoid.Tests.Contracts;

[DataContract]
public record class Message
{
	public Guid Id { get; init; }

	[DataMember(IsRequired = true)]
	public string RequiredText { get; init; }

	[DataMember(IsRequired = false)]
	public string? OptionalText { get; init; }

	[DataMember(IsRequired = true)]
	public int RequiredNumber { get; init; }

	[DataMember(IsRequired = false)]
	public int? OptionalNumber { get; init; }
}
