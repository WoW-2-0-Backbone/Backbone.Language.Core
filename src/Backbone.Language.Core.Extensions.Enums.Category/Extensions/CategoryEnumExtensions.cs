using Backbone.Language.Core.Extensions.Enums.Range.Attributes;
using Backbone.Language.Core.Extensions.Enums.Range.Extensions;

namespace Backbone.Language.Core.Extensions.Enums.Category.Extensions;

/// <summary>
/// Contains extensions for category enums.
/// </summary>
public static class CategoryEnumExtensions
{
    /// <summary>
    /// Validates if the type enum has values in limit of correlated category enum.
    /// </summary>
    /// <typeparam name="TType">The type enum.</typeparam>
    /// <typeparam name="TCategory">The category enum.</typeparam>
    /// <returns>True if the type enum values are within the specified range of the categories, otherwise false.</returns>
    public static bool IsValidTypeEnumForCategory<TType, TCategory>()
        where TType : Enum
        where TCategory : Enum
    {
        var rangeValue = EnumRangeExtensions.GetEnumRangeValue<TCategory>();

        // Get category enum values
        var categoryValues = Enum.GetValues(typeof(TCategory))
            .Cast<TCategory>()
            .Select(value => Convert.ToInt64(value))
            .OrderBy(value => value)
            .ToList();

        // Get category enum limits
        var minTypeValue = categoryValues.First();
        var maxTypeValue = categoryValues.Last() + rangeValue.Range;

        // Get type enum values
        var typeValues = Enum.GetValues(typeof(TType))
            .Cast<TType>()
            .Select(value => Convert.ToInt64(value))
            .OrderBy(value => value)
            .ToList();

        return typeValues.All(value => value >= minTypeValue && value < maxTypeValue);
    }
    
    /// <summary>
    /// Determines whether the specified type value is valid for the given category, based on the <see cref="EnumRangeAttribute"/>.
    /// </summary>
    /// <typeparam name="TType">The type enum.</typeparam>
    /// <typeparam name="TCategory">The category enum.</typeparam>
    /// <param name="type">The type value.</param>
    /// <param name="category">The category value.</param>
    /// <returns>True if the type value belongs to the category; otherwise, false.</returns>
    public static bool IsValidTypeForCategory<TType, TCategory>(this TType type, TCategory category)
        where TType : Enum
        where TCategory : Enum
    {
        var rangeValue = EnumRangeExtensions.GetEnumRangeValue<TCategory>();

        var currentCategoryValue = Convert.ToInt64(category);
        var maxValue = currentCategoryValue + rangeValue.Range;
        var typeValue = Convert.ToInt64(type);

        return typeValue >= currentCategoryValue && typeValue < maxValue;
    }
    
    /// <summary>
    /// Retrieves the category to which the type belongs, based on the <see cref="EnumRangeAttribute"/>.
    /// </summary>
    /// <typeparam name="TType">The type enum.</typeparam>
    /// <typeparam name="TCategory">The category enum.</typeparam>
    /// <param name="type">The type value.</param>
    /// <returns>The category associated with the type.</returns>
    /// <exception cref="ArgumentException">Thrown when the type doesn't belong to any category.</exception>
    public static TCategory GetCategory<TType, TCategory>(this TType type)
        where TType : Enum
        where TCategory : Enum
    {
        var rangeValue = EnumRangeExtensions.GetEnumRangeValue<TCategory>();

        var typeValue = Convert.ToInt64(type);
        var categories = Enum.GetValues(typeof(TCategory)).Cast<TCategory>();

        var category = categories.FirstOrDefault(category =>
        {
            var categoryValue = Convert.ToInt64(category);
            var maxValue = categoryValue + rangeValue.Range;
            return typeValue >= categoryValue && typeValue < maxValue;
        }) ?? throw new ArgumentException(
            $"Type {type} doesn't belong to any category in {typeof(TCategory).Name}");

        return category;
    }
}