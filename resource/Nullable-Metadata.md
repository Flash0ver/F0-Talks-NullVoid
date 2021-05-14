# Nullable Metadata

### Example 1

```cs
public class Class
{
    public void Method1(string? first) { }

    public void Method2(string first, string? second) { }

    public void Method3(string[] first, string[]? second, string?[] third, string?[]? fourth) { }
}
```

```cs
[System.Runtime.CompilerServices.NullableContext(1)]
[System.Runtime.CompilerServices.Nullable(0)]
public class Class
{
    [System.Runtime.CompilerServices.NullableContext(2)]
    public void Method1(string first)
    {
    }

    public void Method2(string first,
        [System.Runtime.CompilerServices.Nullable(2)] string second)
    {
    }

    public void Method3(string[] first,
        [System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] string[] second,
        [System.Runtime.CompilerServices.Nullable(new byte[] { 1, 2 })] string[] third,
        [System.Runtime.CompilerServices.Nullable(2)] string[] fourth)
    {
    }
}
```

### Example 2

```cs
public class Class
{
    public void Method1(string? first) { }

    public void Method2(string first, string? second) { }

    public void Method3(string[] first, string[]? second, string?[] third, string?[]? fourth) { }

#nullable disable
    public void Method4(string first) { }
#nullable restore
}
```

```cs
public class Class
{
    [System.Runtime.CompilerServices.NullableContext(2)]
    public void Method1(string first)
    {
    }

    [System.Runtime.CompilerServices.NullableContext(1)]
    public void Method2(string first,
        [System.Runtime.CompilerServices.Nullable(2)] string second)
    {
    }

    [System.Runtime.CompilerServices.NullableContext(1)]
    public void Method3(string[] first,
        [System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] string[] second,
        [System.Runtime.CompilerServices.Nullable(new byte[] { 1, 2 })] string[] third,
        [System.Runtime.CompilerServices.Nullable(2)] string[] fourth)
    {
    }

    public void Method4(string first)
    {
    }
}
```
