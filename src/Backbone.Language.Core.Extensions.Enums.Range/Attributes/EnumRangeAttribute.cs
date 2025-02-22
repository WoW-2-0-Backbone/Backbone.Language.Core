namespace Backbone.Language.Core.Extensions.Enums.Range.Attributes;

/// <summary>
/// Represents an attribute used to specify a range for enum values.
/// </summary>
[AttributeUsage(AttributeTargets.Enum)]
public sealed class EnumRangeAttribute(ushort range) : Attribute
{
    /// <summary>
    /// Gets the range value specified for the enum.
    /// </summary>
    public ushort Range { get; } = range;
}