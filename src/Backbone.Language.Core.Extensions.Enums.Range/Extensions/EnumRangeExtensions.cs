// namespace Backbone.Language.Core.Extensions.Enums.Range.Extensions;
//
// /// <summary>
// /// Contains enum range extensions.
// /// </summary>
// public static class EnumRangeExtensions
// {
//     #region Definition Validation
//
//     /// <summary>
//     /// Validates if the enum type is a proper range enum based on the <see cref="RangeEnumAttribute"/> applied to it.
//     /// </summary>
//     /// <typeparam name="TRangeEnum">The enum type to be validated.</typeparam>
//     /// <returns>True if the enum is a valid range enum, otherwise false.</returns>
//     /// <exception cref="InvalidOperationException">Thrown if the enum does not have the <see cref="RangeEnumAttribute"/> or if it fails validation.</exception>
//     public static bool IsValidRangeEnum<TRangeEnum>() where TRangeEnum : Enum
//     {
//         RangeEnumAttribute.ValidateAndThrow<TRangeEnum>(out var rangeValue);
//
//         var values = Enum.GetValues(typeof(TRangeEnum))
//             .Cast<TRangeEnum>()
//             .Select(value => Convert.ToUInt16(value))
//             .OrderBy(value => value)
//             .ToList();
//
//         return values.Zip(values.Skip(1), (prev, next) => next - prev)
//             .All(diff => diff == rangeValue.Range);
//     }
//
//     public static bool IsValidCorrelatedEnum<TType, TCategory>()
//         where TType : Enum
//         where TCategory : Enum
//     {
//         RangeEnumAttribute.ValidateAndThrow<TCategory>(out var rangeValue);
//
//         // Get category enum values
//         var categoryValues = Enum.GetValues(typeof(TCategory))
//             .Cast<TCategory>()
//             .Select(value => Convert.ToUInt16(value))
//             .OrderBy(value => value)
//             .ToList();
//
//         // Get category enum limits
//         var minTypeValue = categoryValues.First();
//         var maxTypeValue = categoryValues.Last() + rangeValue.Range;
//
//         // Get type enum values
//         var typeValues = Enum.GetValues(typeof(TType))
//             .Cast<TType>()
//             .Select(value => Convert.ToUInt16(value))
//             .OrderBy(value => value)
//             .ToList();
//
//         return typeValues.All(value => value >= minTypeValue && value < maxTypeValue);
//     }
//
//     #endregion
//
//     #region Value Validation
//
//     public static bool IsValidTypeForCategory<TType, TCategory>(this TType type, TCategory category)
//         where TType : Enum
//         where TCategory : Enum
//     {
//         RangeEnumAttribute.ValidateAndThrow<TCategory>(out var rangeValue);
//
//         var currentCategoryValue = Convert.ToUInt16(category);
//         var maxValue = currentCategoryValue + rangeValue.Range;
//         var typeValue = Convert.ToUInt16(type);
//
//         return typeValue >= currentCategoryValue && typeValue < maxValue;
//     }
//
//     public static TCategory GetCategory<TType, TCategory>(this TType type)
//         where TType : Enum
//         where TCategory : Enum
//     {
//         RangeEnumAttribute.ValidateAndThrow<TCategory>(out var rangeValue);
//
//         var typeValue = Convert.ToUInt16(type);
//         var categories = Enum.GetValues(typeof(TCategory)).Cast<TCategory>();
//
//         var category = categories
//             .FirstOrDefault(category =>
//             {
//                 var categoryValue = Convert.ToUInt16(category);
//                 var maxValue = categoryValue + rangeValue.Range;
//                 return typeValue >= categoryValue && typeValue < maxValue;
//             }) ?? throw new ArgumentException(
//             $"Type {type} doesn't belong to any category in {typeof(TCategory).Name}");
//
//         return category;
//     }
//
//     #endregion
// }