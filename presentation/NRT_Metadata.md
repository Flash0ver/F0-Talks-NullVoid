# Nullable Metadata

synthesized type declarations

```cs
// namespace System.Runtime.CompilerServices
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
internal sealed class NullableAttribute : Attribute
{
    public readonly byte[] NullableFlags;

    public NullableAttribute(byte flag)
    {
        NullableFlags = new byte[] { flag };
    }

    public NullableAttribute(byte[] flags)
    {
        NullableFlags = flags;
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
internal sealed class NullableContextAttribute : Attribute
{
    public readonly byte Flag;

    public NullableContextAttribute(byte flag)
    {
        Flag = flag;
    }
}
```

both `NullableAttribute` and `NullableContextAttribute` is for compiler use only - it is not permitted in source code

`byte`
- 0 => oblivious
- 1 => not annotated
- 2 => annotated

Optimizations
- if `byte[]` is empty, then `NullableAttribute` is skipped
- if all values in `byte[]` are the same, then `NullableAttribute` is collapsed to single `byte` value
- if no `NullableAttribute`, then nearest `NullableContextAttribute` is used
- if no `NullableContextAttribute` in hierarchy, missing `NullableAttribute` treated as `NullableAttribute(0)`

Planned API for _.NET 6.0_

---
#### Demos
- [sharplab.io](https://sharplab.io/)
- `F0.Talks.NullVoid.ConsoleApp/NullableMetadataExample.cs`

#### References
- [Nullable Metadata](https://github.com/dotnet/roslyn/blob/main/docs/features/nullable-metadata.md)
- [Entity Framework Core](https://github.com/dotnet/efcore/blob/main/src/EFCore/Metadata/Conventions/NonNullableConventionBase.cs)
- [NullableAttribute and C# 8](https://codeblog.jonskeet.uk/2019/02/10/nullableattribute-and-c-8/)
- [Easy unit testing of null argument validation (C# 8 edition)](https://thomaslevesque.com/2019/11/19/easy-unit-testing-of-null-argument-validation-c-8-edition/)
- [Expose top-level nullability information from reflection](https://github.com/dotnet/runtime/issues/29723)

#### [TOC](./TOC.md)
