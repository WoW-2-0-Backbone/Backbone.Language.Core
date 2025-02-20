namespace Backbone.Language.Core.Extensions.Enums.Correlation.Attributes;

/// <summary>
/// Represents correlation between enums.
/// </summary>
/// <param name="enumType">The correlated enum type.</param>
/// <param name="correlatedValue">The correlated enu value.</param>
[AttributeUsage(AttributeTargets.Field)]
public class CorrelatedEnumAttribute(Type enumType, Enum correlatedValue)
    : Attribute
{
    /// <summary>
    /// Gets the type of the correlated enum.
    /// </summary>
    public Type EnumType { get; } = enumType;

    /// <summary>
    /// Gets the correlated enum value.
    /// </summary>
    public Enum CorrelatedValue { get; } = correlatedValue;
}

/// <summary>
/// Represents correlation between enums.
/// </summary>
/// <typeparam name="TEnum">The correlated enum type.</typeparam>
/// <param name="correlatedValue">The correlated enu value.</param>
[AttributeUsage(AttributeTargets.Field)]
public class CorrelatedEnumAttribute<TEnum>(TEnum correlatedValue)
    : CorrelatedEnumAttribute(typeof(TEnum), correlatedValue)
    where TEnum : struct, Enum
{
    /// <summary>
    /// Gets the correlated enum value.
    /// </summary>
    public new TEnum CorrelatedValue => (TEnum)base.CorrelatedValue;
}