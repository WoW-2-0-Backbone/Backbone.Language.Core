using Backbone.Language.Core.Extensions.Enums.Correlation.Attributes;
using Backbone.Language.Core.Extensions.Enums.Extensions;

namespace Backbone.Language.Core.Extensions.Enums.Correlation.Extensions;

/// <summary>
/// Contains enum correlation extensions for linked or related enum values
/// </summary>
public static class EnumCorrelationExtensions
{
    /// <summary>
    /// Gets correlated value from given enum value.
    /// </summary>
    /// <param name="enumValue">An enum value to get correlated value for.</param>
    /// <typeparam name="TEnum">The type of correlated enum.</typeparam>
    /// <returns>The correlated enum value</returns>
    /// <exception cref="ArgumentException">If given enum value does not have <see cref="EnumCorrelationAttribute{TEnum}"/> value</exception>
    public static TEnum GetCorrelatedEnum<TEnum>(this Enum enumValue) where TEnum : struct, Enum
    {
        var enumCorrelationAttribute = enumValue.GetCustomAttributeValue<EnumCorrelationAttribute<TEnum>>();
        if (enumCorrelationAttribute == null)
            throw new ArgumentException($"Correlation attribute not found for {enumValue}.", nameof(enumValue));

        return enumCorrelationAttribute.CorrelatedValue;
    }

    /// <summary>
    /// Gets the correlated enum value for the given enum value using the non generic correlate attribute.
    /// </summary>
    /// <param name="enumValue">An enum value to get the correlated value for.</param>
    /// <returns>The correlated enum value.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided enum does not have a corresponding <see cref="EnumCorrelationAttribute"/>.
    /// </exception>
    public static Enum GetCorrelatedValue(this Enum enumValue)
    {
        var enumCorrelationAttribute = enumValue.GetCustomAttributeValue<EnumCorrelationAttribute>();
        if (enumCorrelationAttribute == null)
            throw new ArgumentException($"Correlation attribute not found for {enumValue}.", nameof(enumValue));

        return enumCorrelationAttribute.CorrelatedValue;
    }

    /// <summary>
    /// Tries to get correlated value from the given enum value.
    /// </summary>
    /// <param name="enumValue">An enum value to get the correlated value for.</param>
    /// <param name="correlatedValue">When this method returns, contains the correlated enum value if found; otherwise, the default value of <typeparamref name="TEnum"/>.</param>
    /// <typeparam name="TEnum">The type of correlated enum.</typeparam>
    /// <returns>True if the correlated value was found; otherwise, false.</returns>
    public static bool TryGetCorrelatedValue<TEnum>(this Enum enumValue, out TEnum correlatedValue)
        where TEnum : struct, Enum
    {
        correlatedValue = default;

        var enumCorrelationAttribute = enumValue.GetCustomAttributeValue<EnumCorrelationAttribute<TEnum>>();
        if (enumCorrelationAttribute is null) return false;

        correlatedValue = enumCorrelationAttribute.CorrelatedValue;
        return true;
    }

    /// <summary>
    /// Tries to get the correlated enum value for the given enum value using the non-generic correlation attribute.
    /// </summary>
    /// <param name="enumValue">An enum value to get the correlated value for.</param>
    /// <param name="correlatedValue">When this method returns, contains the correlated enum value if found; otherwise, null.</param>
    /// <returns>True if the correlated value was found; otherwise, false.</returns>
    public static bool TryGetCorrelatedValue(this Enum enumValue, out Enum correlatedValue)
    {
        correlatedValue = null!;

        var enumCorrelationAttribute = enumValue.GetCustomAttributeValue<EnumCorrelationAttribute>();
        if (enumCorrelationAttribute is null) return false;

        correlatedValue = enumCorrelationAttribute.CorrelatedValue;
        return true;
    }
}