// ReSharper disable All

namespace F0.Talks.NullVoid.Generic;

public class Unconstrained<T>
{
	public void Method(T value) { }
}

public class NotNull<T>
	where T : notnull
{
	public void Method(T value) { }
}

public class NotNullClass<T>
	where T : class
{
	public void Method(T value) { }
}

public class MaybeNullClass<T>
	where T : class?
{
	public void Method(T value) { }
}

public class NotNullStruct<T>
	where T : struct
{
	public void Method(T value) { }
}

internal class GenericExample
{
	public void Example_Unconstrained()
	{
		var unconstrained = new Unconstrained<int?>();
		unconstrained.Method(null);
	}

	public void Example_NotNull()
	{
		var notnull = new NotNull<int>();
		notnull.Method(0x_F0);
	}

	public void Example_NotNullClass()
	{
		var notnullClass = new NotNullClass<string>();
		notnullClass.Method(String.Empty);
	}

	public void Example_MaybeNullClass()
	{
		var maybeNullClass = new MaybeNullClass<string?>();
		maybeNullClass.Method(null);
	}

	public void Example_NotNullStruct()
	{
		var notNullStruct = new NotNullStruct<int>();
		notNullStruct.Method(0x_F0);
	}

	public void Example_Dictionary()
	{
		var dictionary = new Dictionary<int, string?>();
		var value = dictionary.Values.FirstOrDefault();
	}
}
