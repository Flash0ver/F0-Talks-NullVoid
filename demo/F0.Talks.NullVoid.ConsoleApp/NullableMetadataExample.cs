using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
// ReSharper disable All

namespace F0.Talks.NullVoid.ConsoleApp;

internal static class NullableMetadataExample
{
	private const string NullableAttribute =
		"System.Runtime.CompilerServices.NullableAttribute";
	private const string NullableContextAttribute =
		"System.Runtime.CompilerServices.NullableContextAttribute";

	private const byte Oblivious = 0;
	private const byte NonNullable = 1;
	private const byte Nullable = 2;

	private static readonly IReadOnlyDictionary<byte, string> map =
		new Dictionary<byte, string>(3)
	{
		{ Oblivious, nameof(Oblivious)},
		{ NonNullable, nameof(NonNullable)},
		{ Nullable, nameof(Nullable)},
	};

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
		byte? nullableContextFlag = GetNullableContext(type);
		foreach (MethodInfo method in type.GetMethods(Lookup))
		{
			WriteNullableMetadata(metadata, method, nullableContextFlag);
		}
		metadata.Indent--;

		metadata.WriteLine('}');

		Console.WriteLine(metadata.InnerWriter.ToString());
	}

	private static void WriteNullableMetadata(IndentedTextWriter metadata, MethodInfo method, byte? nullableContextFlag)
	{
		nullableContextFlag = GetNullableContext(method) ?? nullableContextFlag;

		metadata.Write(method.Name);
		metadata.WriteLine('(');

		metadata.Indent++;
		ParameterInfo[] parameters = method.GetParameters();
		for (int i = 0; i < parameters.Length; i++)
		{
			ParameterInfo parameter = parameters[i];
			WriteNullableMetadata(metadata, parameter, nullableContextFlag);

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

	private static void WriteNullableMetadata(IndentedTextWriter metadata, ParameterInfo parameter, byte? nullableContextFlag)
	{
		metadata.Write(parameter.Name);
		metadata.Write(": ");

		byte[]? nullableFlags = GetNullable(parameter);
		WriteNullableMetadata(metadata, parameter.ParameterType, nullableContextFlag, nullableFlags);
	}

	private static void WriteNullableMetadata(IndentedTextWriter metadata, Type type, byte? nullableContextFlag, byte[]? nullableFlags)
	{
		if (type.IsArray)
		{
			if (nullableFlags is not null)
			{
				if (nullableFlags.Length is 2)
				{
					WriteArray(metadata, nullableFlags[0], nullableFlags[1], type);
				}
				else
				{
					Debug.Assert(nullableFlags.Length is 1);

					WriteArray(metadata, nullableFlags[0], nullableFlags[0], type);
				}
			}
			else
			{
				Debug.Assert(nullableContextFlag is not null);

				WriteArray(metadata, nullableContextFlag.Value, nullableContextFlag.Value, type);
			}
		}
		else // is not array
		{
			if (nullableFlags is not null)
			{
				metadata.Write(map[nullableFlags.Single()]);
			}
			else
			{
				Debug.Assert(nullableContextFlag is not null);
				metadata.Write(map[nullableContextFlag.Value]);
			}

			metadata.Write(' ');
			metadata.Write(type.Name);
		}

		static void WriteArray(IndentedTextWriter metadata, byte array, byte element, Type type)
		{
			Type? encompassedType = type.GetElementType();
			Debug.Assert(encompassedType is not null);

			metadata.Write(map[array]);
			metadata.Write(" Array of ");
			metadata.Write(map[element]);
			metadata.Write(' ');
			metadata.Write(encompassedType.Name);
		}
	}

	private static byte? GetNullableContext(MemberInfo member)
	{
		CustomAttributeData? nullableContext =
			member.CustomAttributes.FirstOrDefault(attribute =>
				attribute.AttributeType.FullName is not null
				&& attribute.AttributeType.FullName.Equals(NullableContextAttribute, StringComparison.Ordinal));

		if (nullableContext is not null)
		{
			CustomAttributeTypedArgument flag = nullableContext.ConstructorArguments.Single();
			Debug.Assert(flag.ArgumentType == typeof(byte));

			Debug.Assert(flag.Value is not null);
			return (byte)flag.Value;
		}

		return null;
	}

	private static byte[]? GetNullable(ParameterInfo parameter)
	{
		CustomAttributeData? nullable =
			parameter.CustomAttributes.FirstOrDefault(attribute =>
				attribute.AttributeType.FullName is not null
				&& attribute.AttributeType.FullName.Equals(NullableAttribute, StringComparison.Ordinal));

		if (nullable is not null)
		{
			CustomAttributeTypedArgument nullableFlags = nullable.ConstructorArguments.Single();
			Debug.Assert(nullableFlags.Value is not null);

			if (nullableFlags.ArgumentType == typeof(byte[]))
			{
				return ((ReadOnlyCollection<CustomAttributeTypedArgument>)nullableFlags.Value)
					.Select(argument =>
					{
						Debug.Assert(argument.Value is not null);
						return (byte)argument.Value;
					})
					.ToArray();
			}
			else
			{
				Debug.Assert(nullableFlags.ArgumentType == typeof(byte));

				return new byte[] { (byte)nullableFlags.Value };
			}
		}

		return null;
	}
}
