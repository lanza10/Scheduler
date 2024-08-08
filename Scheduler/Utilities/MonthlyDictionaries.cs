using Microsoft.Extensions.Localization;
using Scheduler.Enums;
using Scheduler.Services;
using System.Globalization;
using System.Resources;

namespace Scheduler.Utilities
{
    public class MonthlyDictionaries
    {
        private static readonly LocalizationManager Localizer = new();
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
            return $"{Localizer[orderSt]} " +
                   $"{Localizer[daySt]}";
        }
    }
}
