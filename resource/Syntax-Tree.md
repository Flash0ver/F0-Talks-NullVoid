# Syntax Tree

```cs
using System;
#nullable enable
public class Class
{
    public string NonNullable;
    public string? Nullable;
}
```

- Compilation Unit
  - Using Directive
  - Class Declaration
    - Field Declaration
      - Public Keyword
      - Variable Declaration
        - Predefined Type: string
        - Variable Declarator
          - Identifier: NonNullable
    - Field Declaration
      - Public Keyword
      - Variable Declaration
        - Nullable Type
          - Predefined Type: string
        - Variable Declarator
          - Identifier: Nullable
  - End Of File Token
