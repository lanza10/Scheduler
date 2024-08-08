﻿using Microsoft.Extensions.Localization;
using Scheduler.Enums;
using Scheduler.Services;
using System.Globalization;
using System.Resources;

namespace Scheduler.Utilities
{
    public static class OccurrenceDictionaries
    {
        private static readonly LocalizationManager Localizer = new();
        public static string GetFrequencyQuote(int frequency, Occurrence occurrence)
        {
            var occursString = Localizer[occurrence.ToString("G")];

            return (frequency == 1
                ? occursString
                : $"{frequency} {occursString}s")!;
        }

        public static string GetIntervalQuote(int quantity, DailyOccursEveryType type)
        {

            var unitsString = Localizer[type.ToString("G")];

            return (quantity == 1
                ? unitsString
                : $"{quantity} {unitsString}s")!;
        }
    }
}
