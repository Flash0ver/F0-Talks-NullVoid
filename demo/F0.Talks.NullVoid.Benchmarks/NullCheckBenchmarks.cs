using BenchmarkDotNet.Attributes;
// ReSharper disable All

namespace F0.Talks.NullVoid.Benchmarks;

[MedianColumn, MinColumn, MaxColumn]
public class NullCheckBenchmarks
{
	private readonly Record record = new(0x_F0, "F0");

	[Benchmark(Description = "record == null")]
	public bool op_Equality()
		=> record == null;

	[Benchmark(Description = "record.Equals(null)")]
	public bool Equals()
		=> record.Equals(null);

	[Benchmark(Description = "Object.Equals(record, null)")]
	public bool ObjectEquals()
		=> Object.Equals(record, null);

	[Benchmark(Description = "Object.ReferenceEquals(record, null)")]
	public bool ReferenceEquals()
		=> Object.ReferenceEquals(record, null);

	[Benchmark(Description = "EqualityComparer<Record>.Default.Equals(record, null)")]
	public bool EqualityComparerEquals()
		=> EqualityComparer<Record>.Default.Equals(record, null);

	[Benchmark(Description = "ReferenceEqualityComparer.Instance.Equals(record, null)")]
	public bool ReferenceEqualityComparerEquals()
		=> ReferenceEqualityComparer.Instance.Equals(record, null);

	[Benchmark(Description = "(object)record == null")]
	public bool NullCheck()
		=> (object)record == null;

	[Benchmark(Description = "record is null")]
	public bool PatternMatching()
		=> record is null;
}
