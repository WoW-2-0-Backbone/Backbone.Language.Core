namespace Backbone.Language.Core.Time.Extensions.Calculation;

/// <summary>
/// Contains extensions to calculate day.
/// </summary>
public static class DayCalculationExtensions
{
    /// <summary>
    /// Calculates available day of month for given duration of months and returns the correct date.
    /// </summary>
    /// <param name="date">The starting date.</param>
    /// <param name="months">The number of months to consider for calculation.</param>
    /// <returns>The calculated date with correct available day of month.</returns>
    public static DateOnly GetConsistentAvailableLastDay(this DateOnly date, ushort months)
    {
        var minDaysInMonth = 28;
        if (date.DayNumber < minDaysInMonth)
            return date;

        minDaysInMonth = Enumerable.Range(0, months + 1)
            .Select(index => DateTime.DaysInMonth(
                date.Year + (date.Month + index - 1) / 12,
                (date.Month + index - 1) % 12 + 1))
            .Min();

        var targetDay = Math.Min(date.Day, minDaysInMonth);
        return new DateOnly(date.Year, date.Month, targetDay);
    }
}