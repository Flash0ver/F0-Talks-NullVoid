using System.Diagnostics.CodeAnalysis;

namespace F0.Talks.NullVoid.WebApiApp.Todo;

public sealed record class TodoData
{
	[SetsRequiredMembers]
	public TodoData(string Title, string? Message, bool IsComplete)
	{
		this.Title = Title;
		this.Message = Message;
		this.IsComplete = IsComplete;
	}

	public required string Title { get; init; }
	public string? Message { get; init; }
	public bool IsComplete { get; init; }
}
