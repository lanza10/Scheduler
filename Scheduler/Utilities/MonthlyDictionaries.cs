using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Enums;

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
                    DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday]
            },
            { 
                MonthlyDateDay.Weekday, [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                     DayOfWeek.Thursday, DayOfWeek.Friday]

            },
            {
                MonthlyDateDay.WeekendDay, [DayOfWeek.Saturday, DayOfWeek.Sunday]
            }
        };
    }
}
