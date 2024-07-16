using Scheduler.Enums;

namespace Scheduler.Utilities
{
    public static class OccurrenceDictionary
    {
        public static readonly Dictionary<Occurrence, string> OccurrenceMap = new()
        {
            {Occurrence.Daily, "day" },
        };

        public static string GetFrequencyQuote(int frequency, Occurrence occurrence)
        {
            if (!OccurrenceMap.TryGetValue(occurrence, out var occurs))
            {
                throw new KeyNotFoundException();
            }

            return frequency == 1 ? occurs : $"{frequency} {occurs}s";
        }
    }
}
