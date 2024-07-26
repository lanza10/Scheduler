﻿using Scheduler.Enums;

namespace Scheduler.Utilities
{
    public static class OccurrenceDictionaries
    {
        private static readonly Dictionary<Occurrence, string> OccurrenceMap = new()
        {
            {
                Occurrence.Daily, "day"
            },
            {
                Occurrence.Weekly, "week"
            },
            {
                Occurrence.Monthly, "month"
            }
        };

        private static readonly Dictionary<DailyOccursEveryType, string> OccursEveryType = new()
        {
            {
                DailyOccursEveryType.Hours, "hour"
            },
            {
                DailyOccursEveryType.Minutes, "minute"
            },
            {
                DailyOccursEveryType.Seconds, "second"
            },

        };

        public static string GetFrequencyQuote(int frequency, Occurrence occurrence)
        {
            OccurrenceMap.TryGetValue(occurrence, out var occurs);

            return (frequency == 1 ? occurs : $"{frequency} {occurs}s")!;
        }

        public static string GetIntervalQuote(int quantity, DailyOccursEveryType type)
        {
            if (!OccursEveryType.TryGetValue(type, out var unitsMeasure))
            {
                throw new KeyNotFoundException();
            }

            return quantity == 1 ? unitsMeasure : $"{quantity} {unitsMeasure}s";
        }
    }
}
