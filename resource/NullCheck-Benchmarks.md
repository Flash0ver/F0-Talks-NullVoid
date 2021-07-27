# NullCheck Benchmarks

``` ini
BenchmarkDotNet=v0.13.0, OS=Windows 10.0.19043.1110 (21H1/May2021Update)
Intel Core i7-9700K CPU 3.60GHz (Coffee Lake), 1 CPU, 8 logical and 8 physical cores
.NET SDK=5.0.302
  [Host]     : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
  DefaultJob : .NET 5.0.8 (5.0.821.31504), X64 RyuJIT
```

### [NullCheckBenchmarks.cs](../demo/F0.Talks.NullVoid.Benchmarks/NullCheckBenchmarks.cs)

|                                                    Method |      Mean |     Error |    StdDev |    Median |       Min |       Max |
|---------------------------------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|
|                                          &#39;record == null&#39; | 0.6462 ns | 0.0052 ns | 0.0046 ns | 0.6469 ns | 0.6355 ns | 0.6533 ns |
|                                       record.Equals(null) | 0.6627 ns | 0.0062 ns | 0.0058 ns | 0.6626 ns | 0.6505 ns | 0.6724 ns |
|                             &#39;Object.Equals(record, null)&#39; | 0.7595 ns | 0.0078 ns | 0.0073 ns | 0.7582 ns | 0.7509 ns | 0.7715 ns |
|                    &#39;Object.ReferenceEquals(record, null)&#39; | 0.0105 ns | 0.0025 ns | 0.0023 ns | 0.0100 ns | 0.0081 ns | 0.0160 ns |
|   &#39;EqualityComparer&lt;Record&gt;.Default.Equals(record, null)&#39; | 0.9310 ns | 0.0062 ns | 0.0055 ns | 0.9302 ns | 0.9250 ns | 0.9442 ns |
| &#39;ReferenceEqualityComparer.Instance.Equals(record, null)&#39; | 0.2829 ns | 0.0051 ns | 0.0046 ns | 0.2839 ns | 0.2727 ns | 0.2898 ns |
|                                  &#39;(object)record == null&#39; | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|                                          &#39;record is null&#39; | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |

---

### [NotNullCheckBenchmarks.cs](../demo/F0.Talks.NullVoid.Benchmarks/NotNullCheckBenchmarks.cs)

|                                                     Method |      Mean |     Error |    StdDev |    Median |       Min |       Max |
|----------------------------------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|
|                                           &#39;record != null&#39; | 1.3263 ns | 0.0092 ns | 0.0077 ns | 1.3271 ns | 1.3171 ns | 1.3443 ns |
|                                       !record.Equals(null) | 1.0547 ns | 0.0023 ns | 0.0022 ns | 1.0545 ns | 1.0513 ns | 1.0580 ns |
|                             &#39;!Object.Equals(record, null)&#39; | 1.4029 ns | 0.0031 ns | 0.0028 ns | 1.4024 ns | 1.4000 ns | 1.4101 ns |
|                    &#39;!Object.ReferenceEquals(record, null)&#39; | 0.0089 ns | 0.0021 ns | 0.0018 ns | 0.0089 ns | 0.0045 ns | 0.0118 ns |
|   &#39;!EqualityComparer&lt;Record&gt;.Default.Equals(record, null)&#39; | 1.3396 ns | 0.0076 ns | 0.0071 ns | 1.3406 ns | 1.3208 ns | 1.3496 ns |
| &#39;!ReferenceEqualityComparer.Instance.Equals(record, null)&#39; | 0.2784 ns | 0.0065 ns | 0.0061 ns | 0.2778 ns | 0.2690 ns | 0.2890 ns |
|                                   &#39;(object)record != null&#39; | 0.0294 ns | 0.0023 ns | 0.0020 ns | 0.0299 ns | 0.0258 ns | 0.0327 ns |
|                                         &#39;record is object&#39; | 0.0073 ns | 0.0023 ns | 0.0020 ns | 0.0075 ns | 0.0032 ns | 0.0101 ns |
|                                            &#39;record is { }&#39; | 0.0075 ns | 0.0028 ns | 0.0024 ns | 0.0066 ns | 0.0050 ns | 0.0116 ns |
|                                       &#39;record is not null&#39; | 0.0101 ns | 0.0046 ns | 0.0043 ns | 0.0117 ns | 0.0012 ns | 0.0147 ns |
