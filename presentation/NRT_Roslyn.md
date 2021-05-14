# .NET Compiler Platform

### Project

```cs
namespace Microsoft.CodeAnalysis
{
    [Flags]
    public enum NullableContextOptions
    {
        Disable = 0,
        Warnings = 1,
        Annotations = 2,
        Enable = Annotations | Warnings, // 3
    }
}
```

### Source file location

```cs
namespace Microsoft.CodeAnalysis
{
    [Flags]
    public enum NullableContext
    {
        Disabled = 0,
        WarningsEnabled = 1,
        AnnotationsEnabled = 2,
        Enabled = AnnotationsEnabled | WarningsEnabled, // 3
        WarningsContextInherited = 4,
        AnnotationsContextInherited = 8,
        ContextInherited = AnnotationsContextInherited | WarningsContextInherited, // 12
    }
}
```

### Nullability

```cs
namespace Microsoft.CodeAnalysis
{
    public enum NullableAnnotation : byte
    {
        None = 0,
        NotAnnotated = 1,
        Annotated = 2,
    }
}
```

### SyntaxNode

```cs
namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
    public sealed class NullableTypeSyntax : TypeSyntax
    {
        // Microsoft.CodeAnalysis.CSharp.SyntaxKind.NullableType
    }
}
```

---
#### Demos
- `F0.Talks.NullVoid.Tests/Contracts/Message.cs`
- `F0.Talks.NullVoid.Analyzers/NullableAwareContractsAnalyzer.cs`
- `F0.Talks.NullVoid.Analyzers/NullableAwareContractsFixer.cs`

#### [TOC](./TOC.md)
