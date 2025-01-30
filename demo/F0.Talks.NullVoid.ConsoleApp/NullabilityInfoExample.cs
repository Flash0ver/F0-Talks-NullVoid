using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Reflection;
// ReSharper disable All

namespace F0.Talks.NullVoid.ConsoleApp;

internal static class NullabilityInfoExample
{
	internal static async Task InvokeAsync<T>()
	{
		Type type = typeof(T);

		await using IndentedTextWriter metadata = new(new StringWriter());

		metadata.Write(type.MemberType);
		metadata.Write(' ');
		metadata.WriteLine(type.Name);
		metadata.WriteLine('{');

		const BindingFlags Lookup =
			BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly;

		metadata.Indent++;
		NullabilityInfoContext context = new();
		foreach (MethodInfo method in type.GetMethods(Lookup))
		{
			WriteNullableMetadata(metadata, method, context);
		}
		metadata.Indent--;

		metadata.WriteLine('}');

		Console.WriteLine(metadata.InnerWriter.ToString());
	}

	private static void WriteNullableMetadata(IndentedTextWriter metadata, MethodInfo method, NullabilityInfoContext context)
	{
		metadata.Write(method.Name);
		metadata.WriteLine('(');

		metadata.Indent++;
		ParameterInfo[] parameters = method.GetParameters();
		for (int i = 0; i < parameters.Length; i++)
		{
			ParameterInfo parameter = parameters[i];
			WriteNullableMetadata(metadata, parameter, context);

			if (i + 1 < parameters.Length)
			{
				metadata.WriteLine(",");
			}
		}
		metadata.Indent--;

		metadata.WriteLine();
		metadata.Write(')');
		metadata.WriteLine(';');
	}

	private static void WriteNullableMetadata(IndentedTextWriter metadata, ParameterInfo parameter, NullabilityInfoContext context)
	{
		metadata.Write(parameter.Name);
		metadata.Write(": ");

		NullabilityInfo info = context.Create(parameter);
		WriteNullableMetadata(metadata, parameter.ParameterType, info);
	}

	private static void WriteNullableMetadata(IndentedTextWriter metadata, Type type, NullabilityInfo info)
	{
		if (type.IsArray)
		{
			Type? encompassedType = type.GetElementType();
			Debug.Assert(encompassedType is not null);

			Debug.Assert(info.ReadState == info.WriteState);
			Debug.Assert(info.ElementType is not null);
			Debug.Assert(info.ElementType.ReadState == info.ElementType.WriteState);

			metadata.Write(info.ReadState);
			metadata.Write(" Array of ");
			metadata.Write(info.ElementType.ReadState);
			metadata.Write(' ');
			metadata.Write(encompassedType.Name);
		}
		else // is not array
		{
			Debug.Assert(info.ReadState == info.WriteState);
			Debug.Assert(info.ElementType is null);

			metadata.Write(info.ReadState);
			metadata.Write(' ');
			metadata.Write(type.Name);
		}
	}
}
