# WoW2.Backbone.Language.Core.Extensions.Enums

Provides extension methods for enum types.

## Installation

```bash
dotnet add package WoW2.Backbone.Language.Core.Extensions.Enums
```

## Features

-

- Type-safe enum parsing
- Enum value validation
- Description attribute support
- Enum to string conversions
- Enum collections utilities

## Usage

```csharp
using WoW2.Backbone.Language.Core.Extensions.Enums;

public enum Status
{
    [Description("Active Status")]
    Active,
    Inactive
}

// Get description
string description = Status.Active.GetDescription(); // Returns "Active Status"

// Parse safely
Status status = "Active".ToEnum<Status>(); // Returns Status.Active

// Validate
bool isValid = "Invalid".IsValidEnum<Status>(); // Returns false
```

## Requirements

- .NET 9.0 or higher

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

# CHANGELOG.md

## [9.0.0-alpha.0] - 2025-02-19

### Added

- Initial release
- Enum extension methods
- Type-safe parsing utilities
- Description attribute support
- Collection helpers for enums

### Changed

- Initial version - no changes

### Deprecated

- None

### Removed

- None

### Fixed

- None

### Security

- None
