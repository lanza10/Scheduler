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

        public static readonly Dictionary<MonthlyDateOrder, string> OrderMap = new()
        {
            {
                MonthlyDateOrder.First, "First"
            },
            {
                MonthlyDateOrder.Second, "Second"
            },
            {
               MonthlyDateOrder.Third, "Third"
            },
            {
                MonthlyDateOrder.Fourth, "Fourth"
            },
            {
                MonthlyDateOrder.Last, "Last"
            },
        };
        public static readonly Dictionary<MonthlyDateDay, string> DayOfMonthMap = new()
        {
            {
                MonthlyDateDay.Sunday, "Sunday"
            },
            {
                MonthlyDateDay.Monday, "Monday"
            },
            {
                MonthlyDateDay.Tuesday, "Tuesday"
            },
            {
                MonthlyDateDay.Wednesday, "Wednesday"
            },
            {
                MonthlyDateDay.Thursday, "Thursday"
            },
            {
                MonthlyDateDay.Friday, "Friday"
            },
            {
                MonthlyDateDay.Saturday, "Saturday"
            },
            {
                MonthlyDateDay.Day,  "Day"
            },
            {
                MonthlyDateDay.Weekday, "Weekday"

            },
            {
                MonthlyDateDay.WeekendDay, "Weekend day"
            }
        };

        public static string GetOrderDayQuote(MonthlyDateOrder order, MonthlyDateDay day)
        {
            if (!OrderMap.TryGetValue(order, out var orderQuote))
            {
                throw new KeyNotFoundException();
            }

            DayOfMonthMap.TryGetValue(day, out var dayQuote);

            return $"{Rm.GetString(orderQuote, CultureInfo.CurrentCulture)} {Rm.GetString(dayQuote!, CultureInfo.CurrentCulture)}";
        }
    }
}
