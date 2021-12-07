# NullCheck Benchmarks

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1348 (21H1/May2021Update)
Intel Core i7-9700K CPU 3.60GHz (Coffee Lake), 1 CPU, 8 logical and 8 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
```

### [NullCheckBenchmarks.cs](../demo/F0.Talks.NullVoid.Benchmarks/NullCheckBenchmarks.cs)

|                                                    Method |      Mean |     Error |    StdDev |    Median |       Min |       Max |
|---------------------------------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|
|                                          &#39;record == null&#39; | 0.2281 ns | 0.0041 ns | 0.0038 ns | 0.2295 ns | 0.2205 ns | 0.2323 ns |
|                                       record.Equals(null) | 0.0082 ns | 0.0038 ns | 0.0035 ns | 0.0089 ns | 0.0016 ns | 0.0126 ns |
|                             &#39;Object.Equals(record, null)&#39; | 0.2097 ns | 0.0039 ns | 0.0035 ns | 0.2102 ns | 0.2027 ns | 0.2169 ns |
|                    &#39;Object.ReferenceEquals(record, null)&#39; | 0.0043 ns | 0.0049 ns | 0.0046 ns | 0.0042 ns | 0.0000 ns | 0.0120 ns |
|   &#39;EqualityComparer&lt;Record&gt;.Default.Equals(record, null)&#39; | 0.9490 ns | 0.0073 ns | 0.0068 ns | 0.9521 ns | 0.9286 ns | 0.9548 ns |
| &#39;ReferenceEqualityComparer.Instance.Equals(record, null)&#39; | 0.0077 ns | 0.0031 ns | 0.0029 ns | 0.0075 ns | 0.0022 ns | 0.0127 ns |
|                                  &#39;(object)record == null&#39; | 0.0076 ns | 0.0072 ns | 0.0064 ns | 0.0068 ns | 0.0000 ns | 0.0218 ns |
|                                          &#39;record is null&#39; | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |

---

### [NotNullCheckBenchmarks.cs](../demo/F0.Talks.NullVoid.Benchmarks/NotNullCheckBenchmarks.cs)

|                                                     Method |      Mean |     Error |    StdDev |    Median |       Min |       Max |
|----------------------------------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|
|                                           &#39;record != null&#39; | 0.2256 ns | 0.0018 ns | 0.0016 ns | 0.2258 ns | 0.2216 ns | 0.2278 ns |
|                                       !record.Equals(null) | 0.0066 ns | 0.0044 ns | 0.0042 ns | 0.0068 ns | 0.0000 ns | 0.0140 ns |
|                             &#39;!Object.Equals(record, null)&#39; | 0.2096 ns | 0.0037 ns | 0.0034 ns | 0.2106 ns | 0.2035 ns | 0.2155 ns |
|                    &#39;!Object.ReferenceEquals(record, null)&#39; | 0.0031 ns | 0.0058 ns | 0.0054 ns | 0.0000 ns | 0.0000 ns | 0.0193 ns |
|   &#39;!EqualityComparer&lt;Record&gt;.Default.Equals(record, null)&#39; | 1.3883 ns | 0.0124 ns | 0.0116 ns | 1.3915 ns | 1.3686 ns | 1.4120 ns |
| &#39;!ReferenceEqualityComparer.Instance.Equals(record, null)&#39; | 0.0080 ns | 0.0023 ns | 0.0022 ns | 0.0085 ns | 0.0039 ns | 0.0104 ns |
|                                   &#39;(object)record != null&#39; | 0.0190 ns | 0.0033 ns | 0.0030 ns | 0.0199 ns | 0.0126 ns | 0.0244 ns |
|                                         &#39;record is object&#39; | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|                                         &#39;record is Record&#39; | 0.0058 ns | 0.0028 ns | 0.0026 ns | 0.0062 ns | 0.0017 ns | 0.0118 ns |
|                                            &#39;record is { }&#39; | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |
|                                       &#39;record is not null&#39; | 0.0067 ns | 0.0048 ns | 0.0045 ns | 0.0077 ns | 0.0000 ns | 0.0135 ns |
