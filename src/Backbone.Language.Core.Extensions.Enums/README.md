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

| Method                                                    |        Mean |     Error |    StdDev |   Gen0 | Allocated |
| --------------------------------------------------------- | ----------: | --------: | --------: | -----: | --------: |
| Benchmark_GetDescription_Existing                         |   347.53 ns |  6.261 ns |  5.228 ns | 0.0324 |     272 B |
| Benchmark_GetDescriptionOrValue_Existing                  |   344.25 ns |  3.247 ns |  2.879 ns | 0.0324 |     272 B |
| Benchmark_GetDescriptionOrValue_Missing                   |    56.03 ns |  0.795 ns |  0.705 ns | 0.0029 |      24 B |
| Benchmark_TryGetDescription_Existing                      |   348.34 ns |  5.894 ns |  5.225 ns | 0.0324 |     272 B |
| Benchmark_TryGetDescription_Missing                       |    59.25 ns |  1.176 ns |  1.155 ns | 0.0029 |      24 B |
| Benchmark_ParseByDescription_Valid                        | 1,575.74 ns | 13.252 ns | 11.066 ns | 0.1717 |    1440 B |
| Benchmark_TryParseByDescription_Generic_Valid             | 1,560.11 ns |  8.809 ns |  8.240 ns | 0.1736 |    1464 B |
| Benchmark_TryParseByDescription_Generic_Invalid           | 1,789.04 ns | 24.598 ns | 20.541 ns | 0.1717 |    1440 B |
| Benchmark_TryParseByDescription_NonGeneric_Valid          | 1,970.65 ns | 18.489 ns | 15.439 ns | 0.1755 |    1488 B |
| Benchmark_TryParseByDescription_NonGeneric_Invalid        | 1,709.78 ns | 10.634 ns |  9.426 ns | 0.1793 |    1512 B |
| Benchmark_GetAllValuesAndDescriptions_Generic             |          NA |        NA |        NA |     NA |        NA |
| Benchmark_GetAllValuesAndDescriptions_NonGeneric          |          NA |        NA |        NA |     NA |        NA |
| Benchmark_GetAllAvailableValuesAndDescriptions_Generic    | 1,539.13 ns | 15.890 ns | 14.086 ns | 0.1602 |    1352 B |
| Benchmark_GetAllAvailableValuesAndDescriptions_NonGeneric | 1,696.32 ns |  7.668 ns |  5.986 ns | 0.1659 |    1400 B |
| Benchmark_GetCustomAttributeValue_Existing                |   337.99 ns |  0.997 ns |  0.933 ns | 0.0324 |     272 B |
| Benchmark_GetCustomAttributeValue_Missing                 |    53.17 ns |  0.172 ns |  0.143 ns | 0.0029 |      24 B |
| Benchmark_GetAllValuesAndAttributeValues                  |   848.70 ns |  4.843 ns |  4.044 ns | 0.0916 |     768 B |

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
