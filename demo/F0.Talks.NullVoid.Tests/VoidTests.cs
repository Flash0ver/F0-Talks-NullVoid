using System;
using System.Diagnostics;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace F0.Talks.NullVoid.Tests
{
	public class VoidTests
	{
		[Fact]
		public void Reflection_ReturnType()
		{
			Type type = typeof(Record);

			MethodInfo? method = type.GetMethod(nameof(Record.Deconstruct));

			method.Should().NotBeNull();
			Debug.Assert(method is not null);

			Type returnType = method.ReturnType;

			returnType.Should().Be(typeof(void));
		}

		private record Record(int Number, string Text);
	}
}
