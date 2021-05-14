# NullCheck Benchmarks

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-9700K CPU 3.60GHz (Coffee Lake), 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=5.0.202
  [Host]     : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT
  DefaultJob : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT
```

### [NullCheckBenchmarks.cs](../demo/F0.Talks.NullVoid.Benchmarks/NullCheckBenchmarks.cs)

|                                                    Method |      Mean |     Error |    StdDev |    Median |       Min |       Max |
|---------------------------------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|
|                                          &#39;record == null&#39; | 0.8167 ns | 0.0103 ns | 0.0086 ns | 0.8105 ns | 0.8089 ns | 0.8357 ns |
|                                       record.Equals(null) | 0.8247 ns | 0.0107 ns | 0.0100 ns | 0.8280 ns | 0.8120 ns | 0.8462 ns |
|                             &#39;Object.Equals(record, null)&#39; | 0.8223 ns | 0.0170 ns | 0.0159 ns | 0.8230 ns | 0.8012 ns | 0.8537 ns |
|                    &#39;Object.ReferenceEquals(record, null)&#39; | 0.0271 ns | 0.0074 ns | 0.0069 ns | 0.0279 ns | 0.0160 ns | 0.0373 ns |
|   &#39;EqualityComparer&lt;Record&gt;.Default.Equals(record, null)&#39; | 0.9074 ns | 0.0118 ns | 0.0110 ns | 0.9082 ns | 0.8948 ns | 0.9295 ns |
| &#39;ReferenceEqualityComparer.Instance.Equals(record, null)&#39; | 0.1020 ns | 0.0081 ns | 0.0068 ns | 0.1000 ns | 0.0914 ns | 0.1130 ns |
|                                  &#39;(object)record == null&#39; | 0.0007 ns | 0.0029 ns | 0.0024 ns | 0.0000 ns | 0.0000 ns | 0.0087 ns |
|                                          &#39;record is null&#39; | 0.0049 ns | 0.0054 ns | 0.0050 ns | 0.0031 ns | 0.0000 ns | 0.0158 ns |

---

### [NotNullCheckBenchmarks.cs](../demo/F0.Talks.NullVoid.Benchmarks/NotNullCheckBenchmarks.cs)

|                                                    Method |      Mean |     Error |    StdDev |    Median |       Min |       Max |
|---------------------------------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|
|                                          &#39;record != null&#39; | 1.3390 ns | 0.0174 ns | 0.0136 ns | 1.3393 ns | 1.3159 ns | 1.3642 ns |
|                                      !record.Equals(null) | 0.9962 ns | 0.0124 ns | 0.0116 ns | 0.9962 ns | 0.9780 ns | 1.0166 ns |
|                            &#39;!Object.Equals(record, null)&#39; | 1.1242 ns | 0.0144 ns | 0.0134 ns | 1.1242 ns | 1.1099 ns | 1.1488 ns |
|                   &#39;!Object.ReferenceEquals(record, null)&#39; | 0.0268 ns | 0.0100 ns | 0.0094 ns | 0.0248 ns | 0.0129 ns | 0.0454 ns |
|  &#39;!EqualityComparer&lt;Record&gt;.Default.Equals(record, null)&#39; | 1.4011 ns | 0.0155 ns | 0.0145 ns | 1.4003 ns | 1.3784 ns | 1.4283 ns |
| &#39;!ReferenceEqualityComparer.Instance.Equals(record, null)&#39; | 0.1034 ns | 0.0072 ns | 0.0067 ns | 0.1023 ns | 0.0954 ns | 0.1149 ns |
|                                  &#39;(object)record != null&#39; | 0.0023 ns | 0.0061 ns | 0.0054 ns | 0.0000 ns | 0.0000 ns | 0.0158 ns |
|                                        &#39;record is object&#39; | 0.0097 ns | 0.0078 ns | 0.0073 ns | 0.0100 ns | 0.0000 ns | 0.0211 ns |
|                                           &#39;record is { }&#39; | 0.0345 ns | 0.0058 ns | 0.0052 ns | 0.0354 ns | 0.0248 ns | 0.0426 ns |
|                                      &#39;record is not null&#39; | 0.0345 ns | 0.0057 ns | 0.0048 ns | 0.0340 ns | 0.0272 ns | 0.0436 ns |
