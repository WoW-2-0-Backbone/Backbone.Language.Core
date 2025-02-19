using System.ComponentModel;
using System.Reflection;
using Backbone.Language.Core.Extensions.Enums.Constants;

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

/// <summary>
/// Contains description extensions for enums.
/// </summary>
public static class EnumDescriptionExtensions
{
    #region Parsing

    /// <summary>
    /// Retrieves the description of the enum value.
    /// </summary>
    /// <param name="value">The enum value to retrieve the description for.</param>
    /// <returns>The description if available.</returns>
    /// <exception cref="ArgumentException">Thrown if the enum value does not have a description.</exception>
    public static string GetDescription(this Enum value)
    {
        var descriptionAttribute = value.GetCustomAttributeValue<DescriptionAttribute>();
        return descriptionAttribute?.Description
               ?? throw new ArgumentException(string.Format(ErrorMessages.MissingDescription, value));
    }

    /// <summary>
    /// Retrieves the description of the enum value if set, otherwise returns enum value.
    /// </summary>
    /// <param name="value">The enum value to retrieve the description for.</param>
    /// <returns>The description if available, otherwise the name of the enum value.</returns>
    public static string GetDescriptionOrValue(this Enum value)
    {
        var descriptionAttribute = value.GetCustomAttributeValue<DescriptionAttribute>();
        return descriptionAttribute?.Description ?? value.ToString();
    }

    /// <summary>
    /// Attempts to retrieve the description of a specified enum value.
    /// </summary>
    /// <param name="value">The enum value to retrieve the description for.</param>
    /// <param name="description">The retrieved enum description if present.</param>
    /// <returns>The attempt result.</returns>
    public static bool TryGetDescription(this Enum value, out string description)
    {
        var descriptionAttribute = value.GetCustomAttributeValue<DescriptionAttribute>();
        if (descriptionAttribute != null)
        {
            description = descriptionAttribute.Description;
            return true;
        }

        description = value.ToString();
        return false;
    }

    /// <summary>
    /// Parses an enum value based on its Description attribute.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="description">The description of the enum value.</param>
    /// <returns>The parsed enum value.</returns>
    /// <exception cref="ArgumentException">Thrown if the description is invalid.</exception>
    public static T ParseByDescription<T>(string description) where T : struct, Enum
    {
        var matchedEnum = GetAllAvailableValuesAndDescriptions<T>()
            .FirstOrDefault(kv => kv.Value.Equals(description, StringComparison.OrdinalIgnoreCase));

        if (matchedEnum.Value is null || !Enum.IsDefined(matchedEnum.Key))
            throw new ArgumentException(string.Format(ErrorMessages.NoMatchingEnumValue, description, typeof(T).Name));

        return matchedEnum.Key;
    }

    /// <summary>
    /// Tries to parse an enum value based on its Description attribute.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="description">The description of the enum value.</param>
    /// <param name="result">The parsed enum value if successful, or default if not.</param>
    /// <returns>True if parsing is successful, otherwise false.</returns>
    public static bool TryParseByDescription<T>(string description, out T result) where T : struct, Enum
    {
        var matchedEnum = GetAllAvailableValuesAndDescriptions<T>()
            .FirstOrDefault(kv => kv.Value.Equals(description, StringComparison.OrdinalIgnoreCase));

        result = matchedEnum.Key;
        return matchedEnum.Value is not null && Enum.IsDefined(typeof(T), matchedEnum.Key); 
    }

    /// <summary>
    /// Tries to parse an enum value based on its Description attribute.
    /// </summary>
    /// <param name="enumType">The enum type</param>
    /// <param name="description">The description of the enum value.</param>
    /// <param name="result">The parsed enum value if successful, or default if not.</param>
    /// <returns>True if parsing is successful, otherwise false.</returns>
    public static bool TryParseByDescription(Type enumType, string description, out Enum result)
    {
        var matchedEnum = GetAllAvailableValuesAndDescriptions(enumType)
            .FirstOrDefault(kv => kv.Value.Equals(description, StringComparison.OrdinalIgnoreCase));
        
        if (matchedEnum.Value is not null)
        {
            result = matchedEnum.Key;
            return Enum.IsDefined(enumType, result);
        }

        result = (Enum)Activator.CreateInstance(enumType)!;
        return false;
    }

    #endregion

    #region other

    /// <summary>
    /// Gets all values of the enum and description as a list of key-value pairs.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <returns>A key-value pair list of enum value and description.</returns>
    public static List<KeyValuePair<T, string>> GetAllValuesAndDescriptions<T>() where T : struct, Enum
    {
        return Enum.GetValues<T>()
            .Select(e => new KeyValuePair<T, string>(e, e.GetDescription()))
            .ToList();
    }
    
    /// <summary>
    /// Gets all values of the enum and description as a list of key-value pairs.
    /// </summary>
    /// <param name="enumType">The enum type.</param>
    /// <returns>A key-value pair list of enum value and description.</returns>
    public static List<KeyValuePair<Enum, string>> GetAllValuesAndDescriptions(Type enumType) 
    {
        return Enum.GetValues(enumType)
            .Cast<Enum>()
            .Select(e => new KeyValuePair<Enum, string>(e, e.GetDescription()))
            .ToList();
    }

    /// <summary>
    /// Gets all values of the enum and description as a list of key-value pairs.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <returns>A key-value pair list of enum value and description.</returns>
    public static List<KeyValuePair<T, string>> GetAllAvailableValuesAndDescriptions<T>() where T : struct, Enum
    {
        return Enum.GetValues<T>()
            .Where(e => e.TryGetDescription(out _))
            .Select(e => new KeyValuePair<T, string>(e, e.GetDescription()))
            .ToList();
    }

    /// <summary>
    /// Gets all values of the enum and description as a list of key-value pairs.
    /// </summary>
    /// <param name="enumType">The enum type.</param>
    /// <returns>A key-value pair list of enum value and description.</returns>
    public static List<KeyValuePair<Enum, string>> GetAllAvailableValuesAndDescriptions(Type enumType)
    {
        return Enum.GetValues(enumType)
            .Cast<Enum>()
            .Where(e => e.TryGetDescription(out _))
            .Select(e => new KeyValuePair<Enum, string>(e, e.GetDescription()))
            .ToList();
    }

    #endregion
}