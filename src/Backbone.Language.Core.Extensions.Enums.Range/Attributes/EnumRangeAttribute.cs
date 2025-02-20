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

    // /// <summary>
    // /// Validates and retrieves the <see cref="EnumRangeAttribute"/> applied to the specified enum type.
    // /// Throws an exception if the attribute is not found.
    // /// </summary>
    // /// <typeparam name="TEnum">The enum type to validate.</typeparam>
    // /// <param name="enumRange">The retrieved <see cref="EnumRangeAttribute"/>.</param>
    // /// <exception cref="InvalidOperationException">Thrown when the enum does not have the <see cref="EnumRangeAttribute"/> applied.</exception>
    // public static void ValidateAndThrow<TEnum>(out EnumRangeAttribute enumRange) where TEnum : Enum => 
    //     ValidateAndThrow(typeof(TEnum), out enumRange);
    //
    // /// <summary>
    // /// Validates and retrieves the <see cref="EnumRangeAttribute"/> applied to the specified enum type.
    // /// Throws an exception if the attribute is not found.
    // /// </summary>
    // /// <param name="enumType">The enum type to validate.</param>
    // /// <param name="enumRange">The retrieved <see cref="EnumRangeAttribute"/>.</param>
    // /// <exception cref="InvalidOperationException">Thrown when the enum does not have the <see cref="EnumRangeAttribute"/> applied.</exception>
    // public static void ValidateAndThrow(Type enumType, out EnumRangeAttribute enumRange)
    // {
    //     if (!enumType.IsEnum)
    //         throw new ArgumentException($"Type {enumType.Name} must be an enum");
    //     
    //     if (enumType.GetCustomAttributes(typeof(EnumRangeAttribute), false).FirstOrDefault() is not EnumRangeAttribute rangeValue)
    //         throw new InvalidOperationException($"Enum {enumType.Name} must be range enum");
    //
    //     enumRange = rangeValue;
    // }
}