using Microsoft.Extensions.Localization;
using Scheduler.Services;
using System.Globalization;
using System.Resources;
using System.Text;

namespace Scheduler.Utilities
{
    public class WeeklyDictionaries
    {
        private static readonly LocalizationManager Localizer = new();
        public static string GetDaysQuote(List<DayOfWeek> days)
        {
            if (days.Count == 7)
            {
                return Localizer["Everyday"];
            }

            var result = new StringBuilder();
            result.Append($"{Localizer["On"]} ");

            for (var i = 0; i < days.Count; i++)
            {
                if (i == days.Count - 1 && days.Count > 1)
                {
                    result.Append($" {Localizer["And"]} ");
                }
                else if (i > 0)
                {
                    result.Append(", ");
                }

                var day = days[i].ToString("G");
                result.Append(Localizer[day]);
            }

            return result.ToString();
        }
    }
}
