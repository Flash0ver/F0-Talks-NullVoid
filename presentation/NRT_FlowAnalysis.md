# Static Flow Analysis & Null Checks

### Static Flow Analysis

- Flow analysis
  - does not skip [ConditionalAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.conditionalattribute), e.g. [Debug.Assert()](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.debug.assert)
- [! (null-forgiving) operator](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving)
  - help the compiler
  - ignore warning for unit tests
- Declaration warnings
  - `class` vs `struct`
- Arrays
- Null safety
- Thread safety

```cs --project ./Snippets/Snippets.csproj --source-file ./Snippets/Code/NRT.cs --region NRT_Flow
```

### Null Checks

Null tests which affect the state of the flow
- comparisons to `null`
  - equality operator: `instance == null` and `null == instance`
  - inequality operator: `instance != null` and `null != instance`
- calls to _well-known equality methods_
  - `System.Object.Equals(Object, Object)`
  - `System.Object.ReferenceEquals(Object, Object)`
  - `object.Equals(Object)` and overrides
  - `System.IEquatable<T>.Equals(T)` and implementations
  - `System.Collections.Generic.IEqualityComparer<T>.Equals(T, T)` and implementations
- `is` operator
  - is expression: `instance is Type`
  - is pattern: `instance is null`
  - declaration pattern: `instance is Type type`
  - recursive pattern: `instance is { }`
  - negation pattern (logical not pattern): `instance is not Type type`, `instance is not null`, `instance is not { }`, `instance is not Type`
- Null-conditional operators `?.` and `?[]`
- Null-coalescing operator `??`
- Null-coalescing assignment operator `??=`

Not a null test
- var pattern: `instance is var variable`
- discard pattern: `instance is var _`

---
#### Demos
- `F0.Talks.NullVoid.Tests/NullChecks.cs`

#### References
- [Nullable Reference Types](https://github.com/dotnet/roslyn/blob/main/docs/features/nullable-reference-types.md)
- [How null checks have changed in C#](https://www.youtube.com/watch?v=lRUfRlp5BXc)

#### [TOC](./TOC.md)
