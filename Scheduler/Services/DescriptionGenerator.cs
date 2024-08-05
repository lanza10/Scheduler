using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Utilities;
using System.Globalization;
using Scheduler.StringCultures.Descriptions;
using System.Resources;

namespace Scheduler.Services
{
    public class DescriptionGenerator
    {
        private static readonly ResourceManager Rm = new("Scheduler.StringCultures.Descriptions.descriptions", typeof(DescriptionGenerator).Assembly);
        public static string GetOnceDescription(DateTime date,SchedulerConfiguration sc)
        {
            var formattedNextExecTime = date.Date.ToString("d",CultureInfo.CurrentCulture);
            var hour = date.TimeOfDay.ToString(@"hh\:mm");
            var desc = Rm.GetString("OnceDesc", CultureInfo.CurrentCulture);
            return string.Format(desc!, formattedNextExecTime, hour, GetStartingOnQuote(sc.StartDate));
        }
        public static string GetDailyDescription(SchedulerConfiguration sc)
        {
            var desc = Rm.GetString("DailyDesc", CultureInfo.CurrentCulture);
            return string.Format(desc!, GetDailyQuote(sc), GetStartingOnQuote(sc.StartDate));
        }

        public static string GetWeeklyDescription(SchedulerConfiguration sc)
        {
            var desc = Rm.GetString("WeeklyDesc", CultureInfo.CurrentCulture);
            return
               string.Format(desc!, GetWeeklyQuote(sc), GetDailyQuote(sc), GetStartingOnQuote(sc.StartDate));
        }

        public static string GetMonthlyDescription(SchedulerConfiguration sc)
        {
            var desc = Rm.GetString("MonthlyDesc", CultureInfo.CurrentCulture);
            return string.Format(desc!, GetMonthlyQuote(sc), GetDailyQuote(sc), GetStartingOnQuote(sc.StartDate));
        }
        private static string GetDailyQuote(SchedulerConfiguration sc)
        {
            string quote;
            if (sc.DailyType == DailyOccursType.Every)
            {
                var startingAt = sc.DailyStartingAt.ToString(@"hh\:mm");
                var endingAt = sc.DailyEndingAt.ToString(@"hh\:mm");
                var interval = OccurrenceDictionaries.GetIntervalQuote(sc.DailyOccursEvery, sc.OccursEveryType);
                quote = Rm.GetString("EveryDayQuote", CultureInfo.CurrentCulture)!;
                return string.Format(quote, interval, startingAt, endingAt);
            }
            var formmattedOnceAt = sc.DailyOccursOnceAt.ToString(@"hh\:mm");
            quote = Rm.GetString("OnceDayQuote", CultureInfo.CurrentCulture)!;
            return string.Format(quote,formmattedOnceAt);
        }

        private static string GetStartingOnQuote(DateTime stDate)
        {
            var formattedStartDate = stDate.ToString("d", CultureInfo.CurrentCulture);
            var quote = Rm.GetString("StartingOn", CultureInfo.CurrentCulture);
            return string.Format(quote!, formattedStartDate);
        }

        private static string GetWeeklyQuote(SchedulerConfiguration sc)
        {
            var daysOfWeek = WeeklyDictionaries.GetDaysQuote(sc.DaysOfWeek);
            var frequency = OccurrenceDictionaries.GetFrequencyQuote(sc.WeeklyFrequency, sc.Occurs);
            return $"{frequency} {daysOfWeek}";
        }

        private static string GetMonthlyQuote(SchedulerConfiguration sc)
        {
            string quote;
            if (sc.MonthlyType == MonthlyType.Day)
            {
                quote = Rm.GetString("MonthlyDayQuote", CultureInfo.CurrentCulture)!;
                return string.Format(quote!, sc.MonthlyDay, sc.MonthlyDayFrequency);
            }

            quote = Rm.GetString("MonthlyDateQuote", CultureInfo.CurrentCulture)!;
            return 
                $"{MonthlyDictionaries.GetOrderDayQuote(sc.MonthlyDateOrder, sc.MonthlyDateDay)} {string.Format(quote, sc.MonthlyDateFrequency)}";
        }

    }
}
