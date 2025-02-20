# WoW2.Backbone.Language.Core.Extensions.Enums

Provides extension methods for enum types.

## Installation

```bash
dotnet add package WoW2.Backbone.Language.Core.Extensions.Enums
```

## Features

- Retrieving custom attribute value or collection of custom attributes
- Retrieving enum description or attempting to retrieve
- Parsing enum by description or attempting to parse

## Performance

| Method                        |        Mean |     Error |    StdDev |   Gen0 | Allocated |
| ----------------------------- | ----------: | --------: | --------: | -----: | --------: |
| GetDescription_Existing       |   355.82 ns |  7.075 ns |  6.271 ns | 0.0324 |     272 B |
| GetDescriptionOrValue_Missing |    56.26 ns |  0.477 ns |  0.423 ns | 0.0029 |      24 B |
| TryParseByDescription_Valid   | 1,566.55 ns | 15.991 ns | 14.176 ns | 0.1736 |    1464 B |
| TryParseByDescription_Invalid | 1,777.18 ns | 16.346 ns | 15.290 ns | 0.1717 |    1440 B |

## Structure

- `EnumAttributeExtensions` -
- `EnumDescriptionExtensions` -
-

## Usage

```bash
dotnet add package WoW2.Backbone.Language.Core.Extensions.Enums
```

```csharp
using WoW2.Backbone.Language.Core.Extensions.Enums;

public enum Status
{
    [Description("Active Status")]
    Active,
    Inactive
}

// Get description
var description = Status.Active.GetDescription();

// Parse safely
var status = EnumDescriptionExtensions.ParseByDescription<Status>("Active");

// Validate
var isValid = EnumDescriptionExtensions.TryParseByDescription<Status>("Active", out var result);

## Requirements

- .NET 9.0 or higher

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

#
```
