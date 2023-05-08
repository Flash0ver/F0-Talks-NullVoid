using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
// ReSharper disable All

namespace F0.Talks.NullVoid.Tests
{
	internal record class Record(int Number, string Text);

	[System.Diagnostics.CodeAnalysis.SuppressMessage("BestPractice", "F01001:Prefer is pattern to check for null", Justification = "Example")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("BestPractice", "F01002:Prefer is pattern to check for null", Justification = "Example")]
	public class NullChecks
	{
		[Fact]
		public void Equality()
		{
			Record record = new(0x_F0, "Null & Void");

			if (record.Equals(null))
			{
				record.Text.Should().BeNull();
			}
			else
			{
				record.Text.Should().NotBeNull();
			}
		}

		[Fact]
		public void Identity()
		{
			Record record = new(0x_F0, "Null & Void");

			if ((object)record == null)
			{
				record.Text.Should().BeNull();
			}
			else
			{
				record.Text.Should().NotBeNull();
			}
		}

		[Fact]
		public void EqualityComparer()
		{
			Record record = new(0x_F0, "Null & Void");
			IEqualityComparer<Record> comparer = EqualityComparer<Record>.Default;

			if (comparer.Equals(record, null))
			{
				record.Text.Should().BeNull();
			}
			else
			{
				record.Text.Should().NotBeNull();
			}
		}

		[Fact]
		public void NullConditionalOperators()
		{
			Record? record = null;
			Record?[] records = new Record?[1] { null };

			string? member = record?.Text;
			Record? element = records?[0];

			member.Should().BeNull();
			element.Should().BeNull();
		}

		[Fact]
		public void NullCoalescingOperators()
		{
			Record? record = null;

			Record result = record ?? new(0x_F0, "Null & Void");
			result.Should().NotBeNull();

			record.Should().BeNull();
			record ??= new(0x_F0, "Null & Void");
			record.Should().NotBeNull();
		}

		[Fact]
		public void PatternMatching()
		{
			Record record = new(0x_F0, "Null & Void");

			if (record is not null)
			{
				record.Should().NotBeNull();
			}
			else
			{
				record.Should().BeNull();
			}
		}
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0041:Use 'is null' check", Justification = "Example")]
	public class MoreNullChecks
	{
		[Fact]
		public void Equality()
		{
			Class instance = new();
			IEqualityComparer<Class> comparer = EqualityComparer<Class>.Default;

			_ = instance != null;
			_ = instance.Equals(null);
			_ = comparer.Equals(instance, null);
		}

#if HAS_REFERENCE_EQUALITY_COMPARER
		[Fact]
		public void Identity()
		{
			Class instance = new();
			ReferenceEqualityComparer comparer = ReferenceEqualityComparer.Instance;

			_ = (object)instance != null;
			_ = Object.Equals(instance, null);
			_ = comparer.Equals(instance, null);
		}
#endif
	}

	internal class Class : IEquatable<Class>
	{
		public int Number { get; init; }
		public int Text { get; init; }

		public override bool Equals(object? obj)
			=> throw new NotImplementedException("Equals(object)");

		public bool Equals(Class? other)
			=> throw new NotImplementedException("Equals(Class)");

		public override int GetHashCode()
			=> throw new NotImplementedException("GetHashCode");

		public static bool operator ==(Class? left, Class? right)
			=> throw new NotImplementedException("==");

		public static bool operator !=(Class? left, Class? right)
			=> throw new NotImplementedException("!=");
	}
}
