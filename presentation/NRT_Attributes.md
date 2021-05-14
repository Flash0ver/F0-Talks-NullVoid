# Nullable Attributes

### .NET Core 3.0 & .NET Standard 2.1
- [AllowNull](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.allownullattribute) - A non-nullable argument may be null
- [DisallowNull](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.disallownullattribute) - A nullable argument should never be null
- [MaybeNull](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.maybenullattribute) - A non-nullable return value may be null
- [NotNull](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.notnullattribute) - A nullable return value will never be null
- [MaybeNullWhen](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.maybenullwhenattribute) - A non-nullable argument may be null when the method returns the specified bool value
- [NotNullWhen](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.notnullwhenattribute) - A nullable argument won't be null when the method returns the specified bool value
- [NotNullIfNotNull](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.notnullifnotnullattribute) - A return value isn't null if the argument for the specified parameter isn't null
- [DoesNotReturn](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.doesnotreturnattribute) - A method never returns. In other words, it always throws an exception
- [DoesNotReturnIf](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.doesnotreturnifattribute) - This method never returns if the associated bool parameter has the specified value.

### .NET 5.0
- [MemberNotNull](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.membernotnullattribute) - The listed member won't be null when the method returns
- [MemberNotNullWhen](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.codeanalysis.membernotnullwhenattribute) - The listed member won't be null when the method returns the specified bool value

---
#### Demos
- `F0.Talks.NullVoid.Library/NullableAttributes.cs`

#### References
- [Reserved attributes contribute to the compiler's null state static analysis](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/attributes/nullable-analysis)
- [Try out Nullable Reference Types](https://devblogs.microsoft.com/dotnet/try-out-nullable-reference-types/)

#### [TOC](./TOC.md)
