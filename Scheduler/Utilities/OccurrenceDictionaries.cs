using Scheduler.Enums;
using System.Globalization;
using System.Resources;

namespace Scheduler.Utilities
{
    public static class OccurrenceDictionaries
    {
        private static readonly ResourceManager Rm = new("Scheduler.StringCultures.OccurrenceStrings.occurrenceStrings", typeof(OccurrenceDictionaries).Assembly);
        public static string GetFrequencyQuote(int frequency, Occurrence occurrence)
        {
            var occursString = Rm.GetString(occurrence.ToString("G"), CultureInfo.CurrentCulture);

            return (frequency == 1
                ? occursString
                : $"{frequency} {occursString}s")!;
        }

        public static string GetIntervalQuote(int quantity, DailyOccursEveryType type)
        {

            var unitsString = Rm.GetString(type.ToString("G"), CultureInfo.CurrentCulture);

            return (quantity == 1
                ? unitsString
                : $"{quantity} {unitsString}s")!;
        }
    }
}
