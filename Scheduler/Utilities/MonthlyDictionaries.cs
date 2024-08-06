using Scheduler.Enums;
using System.Globalization;
using System.Resources;

namespace Scheduler.Utilities
{
    public class MonthlyDictionaries
    {
        private static readonly ResourceManager Rm = new("Scheduler.StringCultures.MonthlyStrings.monthlyStrings", typeof(MonthlyDictionaries).Assembly);
        public static readonly Dictionary<MonthlyDateDay, List<DayOfWeek>> WeekDaysMap = new()
        {
            {
                MonthlyDateDay.Sunday, [DayOfWeek.Sunday]
            },
            {
                MonthlyDateDay.Monday, [DayOfWeek.Monday]
            },
            {
                MonthlyDateDay.Tuesday, [DayOfWeek.Tuesday]
            },
            {
                MonthlyDateDay.Wednesday, [DayOfWeek.Wednesday]
            },
            {
                MonthlyDateDay.Thursday, [DayOfWeek.Thursday]
            },
            {
                MonthlyDateDay.Friday, [DayOfWeek.Friday]
            },
            {
                MonthlyDateDay.Saturday, [DayOfWeek.Saturday]
            },
            {
                MonthlyDateDay.Day,  [DayOfWeek.Monday, DayOfWeek.Tuesday,
                                        DayOfWeek.Wednesday, DayOfWeek.Thursday,
                                        DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday]
            },
            {
                MonthlyDateDay.Weekday, [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                                            DayOfWeek.Thursday, DayOfWeek.Friday]

            },
            {
                MonthlyDateDay.WeekendDay, [DayOfWeek.Saturday, DayOfWeek.Sunday]
            }
        };

        public static string GetOrderDayQuote(MonthlyDateOrder order, MonthlyDateDay day)
        {
            var orderSt = order.ToString("G");
            var daySt = day.ToString("G");
            return $"{Rm.GetString(orderSt, CultureInfo.CurrentCulture)} " +
                   $"{Rm.GetString(daySt, CultureInfo.CurrentCulture)}";
        }
    }
}
