using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Utilities;
using System.Globalization;

namespace Scheduler.Services
{
    public class DescriptionCalculator
    {
        public static string GetDailyDescription(SchedulerConfiguration sc)
        {
            return $"Occurs every day {GetDailyQuote(sc)} {GetStartingOnQuote(sc)}";
        }

        public static string GetWeeklyDescription(SchedulerConfiguration sc)
        {
            var daysOfWeek = WeeklyDictionaries.GetDaysQuote(sc.DaysOfWeek);
            var frequency = OccurrenceDictionaries.GetFrequencyQuote(sc.WeeklyFrequency, sc.Occurs);


            return
                $"Occurs every {frequency} {daysOfWeek} {GetDailyQuote(sc)} {GetStartingOnQuote(sc)}";
        }

        public static string GetMonthlyDescription(SchedulerConfiguration sc)
        {
            return $"Occurs the {GetMonthlyQuote(sc)} {GetDailyQuote(sc)} {GetStartingOnQuote(sc)}";
        }
        private static string GetDailyQuote(SchedulerConfiguration sc)
        {
            if (sc.DailyType == DailyOccursType.Every)
            {
                var startingAt = sc.DailyStartingAt.ToString(@"hh\:mm");
                var endingAt = sc.DailyEndingAt.ToString(@"hh\:mm");
                var interval = OccurrenceDictionaries.GetIntervalQuote(sc.DailyOccursEvery, sc.OccursEveryType);
                return $"every {interval} between {startingAt} and {endingAt}";
            }
            var formmattedOnceAt = sc.DailyOccursOnceAt.ToString(@"hh\:mm");
            return $"at {formmattedOnceAt}";
        }

        private static string GetStartingOnQuote(SchedulerConfiguration sc)
        {
            var culture = CultureInfo.CurrentCulture;
            var formattedStartDate = sc.StartDate.ToString("d", culture);
            return $"starting on {formattedStartDate}";
        }

        private static string GetMonthlyQuote(SchedulerConfiguration sc)
        {
            if (sc.MonthlyType == MonthlyType.Day)
            {
                return $"{sc.MonthlyDay} of every {sc.MonthlyDayFrequency} months";
            }

            return MonthlyDictionaries.GetOrderDayQuote(sc.MonthlyDateOrder, sc.MonthlyDateDay) +
                   $" of every {sc.MonthlyDateFrequency} months";
        }
    }
}
