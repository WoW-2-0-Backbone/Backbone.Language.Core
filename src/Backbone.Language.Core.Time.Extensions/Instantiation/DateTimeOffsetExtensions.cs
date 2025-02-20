namespace Backbone.Language.Core.Time.Extensions.Instantiation;

/// <summary>
/// Contains extensions for <see cref="DateTimeOffset" /> type
/// </summary>
public static class DateTimeOffsetExtensions
{
    /// <summary>
    /// Converts a <see cref="DateTimeOffset"/> to a <see cref="DateOnly"/> object.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTimeOffset"/> to convert.</param>
    /// <returns>A <see cref="DateOnly"/> instance representing the date portion of the input.</returns>
    public static DateOnly ToDateOnly(this DateTimeOffset dateTime)
    {
        return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
    }
}

/// <summary>
/// Contains extensions for <see cref="DateTime"/> type
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Converts a <see cref="DateTime"/> to a <see cref="DateOnly"/> object.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to convert.</param>
    /// <returns>A <see cref="DateOnly"/> instance representing the date portion of the input.</returns>
    public static DateOnly ToDateOnly(this DateTime dateTime)
    {
        return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
    }
}