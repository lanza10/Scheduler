using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Enums;
using Scheduler.Exceptions;

namespace Scheduler.Utilities
{
    public class MonthlyDictionaries
    {
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
                MonthlyDateOrder.First, "first"
            },
            {
                MonthlyDateOrder.Second, "second"
            },
            {
               MonthlyDateOrder.Third, "third"
            },
            {
                MonthlyDateOrder.Fourth, "fourth"
            },
            {
                MonthlyDateOrder.Last, "last"
            },
        }; 
        public static readonly Dictionary<MonthlyDateDay, string> DayOfMonthMap = new()
        {
            {
                MonthlyDateDay.Sunday, "sunday"
            },
            {
                MonthlyDateDay.Monday, "monday"
            },
            {
                MonthlyDateDay.Tuesday, "tuesday"
            },
            {
                MonthlyDateDay.Wednesday, "wednesday"
            },
            {
                MonthlyDateDay.Thursday, "thursday"
            },
            {
                MonthlyDateDay.Friday, "friday"
            },
            {
                MonthlyDateDay.Saturday, "saturday"
            },
            {
                MonthlyDateDay.Day,  "day"
            },
            {
                MonthlyDateDay.Weekday, "weekday"

            },
            {
                MonthlyDateDay.WeekendDay, "weekend day"
            }
        };

        public static string GetOrderDayQuote(MonthlyDateOrder order, MonthlyDateDay day)
        {
            if (!OrderMap.TryGetValue(order, out var orderQuote))
            {
                throw new KeyNotFoundException();
            }

            DayOfMonthMap.TryGetValue(day, out var dayQuote);
            
            return $"{orderQuote} {dayQuote}";
        }
    }
}
