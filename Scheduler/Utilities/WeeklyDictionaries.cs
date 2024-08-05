using System.Globalization;
using System.Resources;
using System.Text;

namespace Scheduler.Utilities
{
    public class WeeklyDictionaries
    {
        private static readonly ResourceManager Rm = new("Scheduler.StringCultures.WeeklyStrings.weeklyStrings", typeof(WeeklyDictionaries).Assembly);
        private static readonly Dictionary<DayOfWeek, string> DaysOfWeekMap = new()
        {
            {
               DayOfWeek.Monday, "Monday"
            },
            {
                DayOfWeek.Tuesday, "Tuesday"
            },
            {
                DayOfWeek.Wednesday, "Wednesday"
            },
            {
                DayOfWeek.Thursday, "Thursday"
            },
            {
                DayOfWeek.Friday, "Friday"
            },
            {
                DayOfWeek.Saturday, "Saturday"
            },
            {
                DayOfWeek.Sunday, "Sunday"
            }
        };

        public static string GetDaysQuote(List<DayOfWeek> days)
        {
            var result = new StringBuilder();
            if (days.Count == 7)
            {
                return Rm.GetString("Everyday", CultureInfo.CurrentCulture)!;
            }
            result.Append($"{Rm.GetString("On", CultureInfo.CurrentCulture)} ");
            for (var i = 0; i < days.Count; i++)
            {
                DaysOfWeekMap.TryGetValue(days[i], out var stringDay);

                if (i == days.Count - 1 && days.Count > 1)
                {
                    result.Append($" {Rm.GetString("And", CultureInfo.CurrentCulture)} ");
                }
                else if (i > 0)
                {
                    result.Append(", ");
                }

                result.Append(Rm.GetString(stringDay!, CultureInfo.CurrentCulture));
            }

            return result.ToString();
        }
    }
}
