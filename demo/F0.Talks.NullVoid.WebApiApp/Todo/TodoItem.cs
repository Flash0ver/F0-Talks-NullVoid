using System.Diagnostics.CodeAnalysis;

namespace F0.Talks.NullVoid.WebApiApp.Todo;

public sealed record class TodoItem
{
	[SetsRequiredMembers]
	public TodoItem(long Id, string Title, string? Message, bool IsComplete)
	{
		this.Id = Id;
		this.Title = Title;
		this.Message = Message;
		this.IsComplete = IsComplete;
	}

	public long Id { get; init; }
	public required string Title { get; init; }
	public string? Message { get; init; }
	public bool IsComplete { get; init; }
}
