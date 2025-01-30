// ReSharper disable All

namespace F0.Talks.NullVoid.Library;

internal class Resource : IDisposable
{
	public Resource()
	{
	}

	public Resource(string value)
	{
		Property = value;
	}

	public string? Property { get; }

	public void Dispose()
	{
	}
}
