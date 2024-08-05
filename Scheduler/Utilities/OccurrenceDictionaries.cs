﻿using System.Globalization;
using Scheduler.Enums;
using System.Resources;

namespace Scheduler.Utilities
{
    public static class OccurrenceDictionaries
    {
        private static readonly ResourceManager Rm = new("Scheduler.StringCultures.OccurrenceStrings.occurrenceStrings", typeof(OccurrenceDictionaries).Assembly);
        private static readonly Dictionary<Occurrence, string> OccurrenceMap = new()
        {
            {
                Occurrence.Daily, "Day"
            },
            {
                Occurrence.Weekly, "Week"
            },
            {
                Occurrence.Monthly, "Month"
            }
        };

        private static readonly Dictionary<DailyOccursEveryType, string> OccursEveryType = new()
        {
            {
                DailyOccursEveryType.Hours, "Hour"
            },
            {
                DailyOccursEveryType.Minutes, "Minute"
            },
            {
                DailyOccursEveryType.Seconds, "Second"
            },

        };

        public static string GetFrequencyQuote(int frequency, Occurrence occurrence)
        {
            OccurrenceMap.TryGetValue(occurrence, out var occurs);

            return (frequency == 1 
                ? Rm.GetString(occurs!, CultureInfo.CurrentCulture) 
                : $"{frequency} {Rm.GetString(occurs!,CultureInfo.CurrentCulture)}s")!;
        }

        public static string GetIntervalQuote(int quantity, DailyOccursEveryType type)
        {
            if (!OccursEveryType.TryGetValue(type, out var unitsMeasure))
            {
                throw new KeyNotFoundException();
            }

            return (quantity == 1 
                ? Rm.GetString(unitsMeasure, CultureInfo.CurrentCulture) 
                : $"{quantity} {Rm.GetString(unitsMeasure, CultureInfo.CurrentCulture)}s")!;
        }
    }
}
