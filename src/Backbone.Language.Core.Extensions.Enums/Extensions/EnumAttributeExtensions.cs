using System.Reflection;

namespace Backbone.Language.Core.Extensions.Enums.Extensions;

/// <summary>
/// Contains reflection extensions for enums.
/// </summary>
public static class EnumAttributeExtensions
{
    /// <summary>
    /// Gets the value of a specific attribute on an enum value.
    /// </summary>
    /// <typeparam name="TAttribute">The attribute type to retrieve.</typeparam>
    /// <param name="value">The enum value.</param>
    /// <returns>The attribute value if found; otherwise, null.</returns>
    public static TAttribute? GetCustomAttributeValue<TAttribute>(this Enum value) where TAttribute : Attribute
    {
        var field = value.GetType().GetField(value.ToString());
        return field?.GetCustomAttribute<TAttribute>();
    }

    /// <summary>
    /// Gets all values of the enum along with a specified attribute's value as a list of key-value pairs.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <typeparam name="TAttribute">The attribute type.</typeparam>
    /// <returns>A list of key-value pairs where the key is the enum value and the value is the specified attribute's value.</returns>
    public static List<KeyValuePair<TEnum, TAttribute?>> GetAllValuesAndAttributeValues<TEnum, TAttribute>()
        where TEnum : Enum
        where TAttribute : Attribute
    {
        return Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(e => new KeyValuePair<TEnum, TAttribute?>(e, e.GetCustomAttributeValue<TAttribute>()))
            .ToList();
    }
}