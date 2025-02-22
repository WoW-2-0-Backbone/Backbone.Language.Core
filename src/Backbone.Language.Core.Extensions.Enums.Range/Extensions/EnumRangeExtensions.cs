using System.Reflection;
using Backbone.Language.Core.Extensions.Enums.Range.Attributes;

namespace Backbone.Language.Core.Extensions.Enums.Range.Extensions;

/// <summary>
/// Contains enum range extensions.
/// </summary>
public static class EnumRangeExtensions
{
    #region Attribute Validation

    /// <summary>
    /// Validates and retrieves the <see cref="EnumRangeAttribute"/> applied to the specified enum type.
    /// </summary>
    /// <typeparam name="TEnum">The enum type to retrieve enum range value from.</typeparam>
    /// <exception cref="InvalidOperationException">Thrown when the enum does not have the <see cref="EnumRangeAttribute"/> applied.</exception>
    public static EnumRangeAttribute GetEnumRangeValue<TEnum>() where TEnum : Enum
    {
        return GetEnumRangeValue(typeof(TEnum));
    }

    /// <summary>
    /// Validates and retrieves the <see cref="EnumRangeAttribute"/> applied to the specified enum type.
    /// </summary>
    /// <param name="enumType">The enum type to retrieve enum range value from.</param>
    /// <exception cref="ArgumentException">Thrown when the provided type is not an enum.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the enum does not have the <see cref="EnumRangeAttribute"/> applied.</exception>
    public static EnumRangeAttribute GetEnumRangeValue(Type enumType)
    {
        if (!enumType.IsEnum)
            throw new ArgumentException($"Type {enumType.Name} must be an enum", nameof(enumType));

        if (enumType.GetCustomAttribute<EnumRangeAttribute>(false) is not { } rangeValue)
            throw new InvalidOperationException($"Enum {enumType.Name} must be a range enum");

        return rangeValue;
    }

    #endregion

    #region Definition Validation

    /// <summary>
    /// Validates if the enum type is a proper range enum based on the <see cref="EnumRangeAttribute"/> applied to it.
    /// </summary>
    /// <typeparam name="TRangeEnum">The enum type to be validated.</typeparam>
    /// <returns>True if the enum is a valid range enum, otherwise false.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the enum does not have the <see cref="EnumRangeAttribute"/> or if it fails validation.</exception>
    public static bool IsValidRangeEnum<TRangeEnum>() where TRangeEnum : Enum
    {
        var rangeValue = GetEnumRangeValue<TRangeEnum>();

        var values = Enum.GetValues(typeof(TRangeEnum))
            .Cast<TRangeEnum>()
            .Select(value => Convert.ToInt64(value))
            .ToList();

        return values.Zip(values.Skip(1), (prev, next) => next - prev)
            .All(diff => diff == rangeValue.Range);
    }

    #endregion
}