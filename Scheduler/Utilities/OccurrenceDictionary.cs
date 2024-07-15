using Scheduler.Enums;

namespace Scheduler.Utilities
{
    public static class OccurrenceDictionary
    {
        public static readonly Dictionary<Occurrence, string> OccurrenceMap = new()
        {
            {Occurrence.Daily, "day" },
        };

    }
}
