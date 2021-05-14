# Adopt nullable reference types

### Strategy 1
- Project: `<Nullable>enable</Nullable>`
- Source files: `#nullable disable`
- File by file: remove `#nullable disable`, annotate and fix warnings

### Strategy 2
- Project: keep `<Nullable>disable</Nullable>` (default)
- File by file: add `#nullable enable`, annotate and fix warnings
- Project: `<Nullable>enable</Nullable>`
- Source files: remove `#nullable enable`
- Caveat: explicitly add `#nullable enable` to new files

### .NET Framework & .NET Standard 2.0
- Language Version is C# 7.3
- _C# 8.0_ for nullable annotations
- define _Nullable Attributes_
- Multitargeting for annotated BCL
- no _nullable_ warnings for oblivious TFMs

### Argument validation
- validate even though in nullable context with a non-nullable parameter
  - e.g. via `standalone discard`
- consumer may not have _Nullable Reference Types_ (warnings) enabled
- in all _externally visible_ methods
- we can omit validation in non-public API
- consider `<WarningsAsErrors>nullable</WarningsAsErrors>` for `Configuration == Release`

---
#### Demos
- `F0.Talks.NullVoid.Oblivious/`

#### References
- [C# language versioning](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/configure-language-version)
- [How to use Nullable Reference Types in .NET Standard 2.0 and .NET Framework](https://www.meziantou.net/how-to-use-nullable-reference-types-in-dotnet-standard-2-0-and-dotnet-.htm)
- [C# 8.0 and .NET Standard 2.0 - Doing Unsupported Things](https://stu.dev/csharp8-doing-unsupported-things/)

#### [TOC](./TOC.md)
