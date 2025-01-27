using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
// ReSharper disable All

namespace F0.Talks.NullVoid.Benchmarks;

[MedianColumn, MinColumn, MaxColumn]
public class NotNullCheckBenchmarks
{
	private readonly Record record = new(0x_F0, "F0");

	[Benchmark(Description = "record != null")]
	public bool op_Inequality()
		=> record != null;

	[Benchmark(Description = "!record.Equals(null)")]
	public bool NotEquals()
		=> !record.Equals(null);

	[Benchmark(Description = "!Object.Equals(record, null)")]
	public bool NotObjectEquals()
		=> !Object.Equals(record, null);

	[Benchmark(Description = "!Object.ReferenceEquals(record, null)")]
	public bool NotReferenceEquals()
		=> !Object.ReferenceEquals(record, null);

	[Benchmark(Description = "!EqualityComparer<Record>.Default.Equals(record, null)")]
	public bool EqualityComparerNotEquals()
		=> !EqualityComparer<Record>.Default.Equals(record, null);

	[Benchmark(Description = "!ReferenceEqualityComparer.Instance.Equals(record, null)")]
	public bool ReferenceEqualityComparerNotEquals()
		=> !ReferenceEqualityComparer.Instance.Equals(record, null);

	[Benchmark(Description = "(object)record != null")]
	public bool NotNullCheck()
		=> (object)record != null;

	[Benchmark(Description = "record is object")]
	public bool IsExpression()
		=> record is object;

	[Benchmark(Description = "record is Record")]
	public bool TypePattern()
		=> record is Record;

	[Benchmark(Description = "record is { }")]
	public bool RecursivePattern()
		=> record is { };

	[Benchmark(Description = "record is not null")]
	public bool NotPattern()
		=> record is not null;
}
