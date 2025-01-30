using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;
// ReSharper disable All

namespace F0.Talks.NullVoid.Library;

public class NullableAttributes
{
}

// .NET Standard 2.1
// .NET Core 3.0

// A non-nullable argument may be null
public class Precondition_AllowNull
{
	private string _text = String.Empty;

	[AllowNull]
	public string Text
	{
		get => _text;
		set => _text = value ?? String.Empty;
	}

	public void Example1()
	{
		Text = null;
		_ = Text.Length;
	}

	public void Example2()
	{
		using HttpResponseMessage message = new();

		//[AllowNull]
		message.Content = null;
	}
}

// A nullable argument should never be null
public class Precondition_DisallowNull
{
	private string? _text;

	[DisallowNull]
	public string? Text
	{
		get => _text;
		set => _text = value ?? throw new ArgumentNullException(nameof(value));
	}

	public void Example1()
	{
		Text = null;
		_ = Text.Length;
	}

	public void Example2()
	{
		Resource? resource = null;

		//public abstract int GetHashCode([DisallowNull] T obj);
		_ = EqualityComparer<Resource?>.Default.GetHashCode(resource);
	}
}

// A non-nullable return value may be null
public class Postconditions_MaybeNull
{
	[return: MaybeNull]
	public static T Find<T>(IEnumerable<T> source, Func<T, bool> predicate)
	{
		foreach (T item in source)
		{
			if (predicate(item))
			{
				return item;
			}
		}

		return default;
	}

	public void Example1()
	{
		string? result = Find(Enumerable.Empty<string>(), text => text == "dotnet");

		_ = result.Length;
	}
}

// A nullable return value will never be null
public class Postconditions_NotNull
{
	public static void ThrowIfNull([NotNull] object? value, string? paramName)
	{
		_ = value ?? throw new ArgumentNullException(paramName);
	}

	public void Example1(string? message)
	{
		ThrowIfNull(message, nameof(message));

		_ = message.Length;
	}

	public static void InitializeIfNull<T>([NotNull] ref T[]? array)
	{
		array ??= Array.Empty<T>();
	}

	public void Example2(string[]? array)
	{
		InitializeIfNull(ref array);

		_ = array.Length;
	}
}

// A nullable argument won't be null when the method returns the specified bool value
public class ConditionalPostConditions_NotNullWhen
{
	public void Example1(string? message)
	{
		//public static bool IsNullOrEmpty([NotNullWhen(false)] String? value);
		if (String.IsNullOrEmpty(message))
		{
			_ = message.Length;
		}
		else
		{
			_ = message.Length;
		}
	}

	public void Example2(string input)
	{
		//public static bool TryParse([NotNullWhen(true)] string? input, [NotNullWhen(true)] out Version? result);
		if (Version.TryParse(input, out Version? version))
		{
			_ = version.Major;
		}
		else
		{
			_ = version.Major;
		}
	}
}

// A non-nullable argument may be null when the method returns the specified bool value
public class ConditionalPostConditions_MaybeNullWhen
{
	public void Example1(Dictionary<string, string> dictionary)
	{
		//public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value);
		if (dictionary.TryGetValue("text", out string? value))
		{
			_ = value.Length;
		}
		else
		{
			_ = value.Length;
		}
	}

	public void Example2(Queue<string> queue)
	{
		//public bool TryDequeue([MaybeNullWhen(false)] out T result);
		if (queue.TryDequeue(out string? value))
		{
			_ = value.Length;
		}
		else
		{
			_ = value.Length;
		}
	}
}

// A return value isn't null if the argument for the specified parameter isn't null
public class ConditionalPostConditions_NotNullIfNotNull
{
	public void Example(string path)
	{
		//[return: NotNullIfNotNull("path")]
		string? fileName = Path.GetFileName(path);

		_ = fileName.Length;
	}
}

// A method never returns. In other words, it always throws an exception
public class FlowAttributes_DoesNotReturn
{
	public void Example(string? message, Exception exception)
	{
		//[DoesNotReturn]
		ExceptionDispatchInfo.Capture(exception).Throw();

		_ = message.Length;
	}
}

// This method never returns if the associated bool parameter has the specified value
public class FlowAttributes_DoesNotReturnIf
{
	public void Example(string? message)
	{
		//public static void Assert([DoesNotReturnIf(false)] bool condition);
		Debug.Assert(message is not null);

		_ = message.Length;
	}
}

// .NET 5

// The listed member won't be null when the method returns
public class ConstructorHelpers_MemberNotNull
{
	private string _value;

	public ConstructorHelpers_MemberNotNull(string message)
	{
		Initialize(message);
	}

	[MemberNotNull(nameof(_value))]
	private void Initialize(string value)
	{
		_value = value;
	}
}

// The listed member won't be null when the method returns the specified bool value
public class ConstructorHelpers_MemberNotNullWhen
{
	[MemberNotNullWhen(true, nameof(Value))]
	public bool HasValue { get; }

	public string? Value { get; }

	public void Example()
	{
		if (HasValue)
		{
			_ = Value.Length;
		}
	}
}
