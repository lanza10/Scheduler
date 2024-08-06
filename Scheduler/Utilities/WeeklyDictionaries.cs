using System.Globalization;
using System.Resources;
using System.Text;

namespace Scheduler.Utilities
{
    public class WeeklyDictionaries
    {
        private static readonly ResourceManager Rm = new("Scheduler.StringCultures.WeeklyStrings.weeklyStrings", typeof(WeeklyDictionaries).Assembly);
        public static string GetDaysQuote(List<DayOfWeek> days)
        {
            if (days.Count == 7)
            {
                return Rm.GetString("Everyday", CultureInfo.CurrentCulture)!;
            }

            var result = new StringBuilder();
            result.Append($"{Rm.GetString("On", CultureInfo.CurrentCulture)} ");

            for (var i = 0; i < days.Count; i++)
            {
                if (i == days.Count - 1 && days.Count > 1)
                {
                    result.Append($" {Rm.GetString("And", CultureInfo.CurrentCulture)} ");
                }
                else if (i > 0)
                {
                    result.Append(", ");
                }

                var day = days[i].ToString("G");
                result.Append(Rm.GetString(day, CultureInfo.CurrentCulture));
            }

            return result.ToString();
        }
    }
}
