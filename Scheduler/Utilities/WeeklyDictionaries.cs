using Scheduler.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Utilities
{
    public class WeeklyDictionaries
    {
        private static readonly Dictionary<DayOfWeek, string> DaysOfWeekMap = new()
        {
            {
               DayOfWeek.Monday, "monday"
            },
            {
                DayOfWeek.Tuesday, "tuesday"
            },
            {
                DayOfWeek.Wednesday, "wednesday"
            },
            {
                DayOfWeek.Thursday, "thursday"
            },
            {
                DayOfWeek.Friday, "friday"
            },
            {
                DayOfWeek.Saturday, "saturday"
            },
            {
                DayOfWeek.Sunday, "sunday"
            }
        };

        public static string GetDaysQuote(List<DayOfWeek> days)
        {
            var result = new StringBuilder();

            for (var i = 0; i < days.Count; i++)
            {
                if (!DaysOfWeekMap.TryGetValue(days[i], out var stringDay))
                {
                    throw new KeyNotFoundException();
                }

                if (i == days.Count - 1 && days.Count > 1)
                {
                    result.Append(" and ");
                }
                else if (i > 0)
                {
                    result.Append(", ");
                }

                result.Append(stringDay);
            }

            return result.ToString();
        }
    }
}
