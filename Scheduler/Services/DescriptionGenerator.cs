﻿using Microsoft.Extensions.Localization;
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Utilities;
using System.Globalization;
using System.Resources;

namespace Scheduler.Services
{
    public class DescriptionGenerator
    {
        private static readonly LocalizationManager Localizer = new();
        public static string GetOnceDescription(DateTime date, SchedulerConfiguration sc)
        {
            var formattedNextExecTime = date.Date.ToString("d", CultureInfo.CurrentCulture);
            var hour = date.TimeOfDay.ToString(@"hh\:mm");
            var desc = Localizer["OnceDesc", [formattedNextExecTime, hour, GetStartingOnQuote(sc.StartDate)]];
            return desc;
        }
        public static string GetDailyDescription(SchedulerConfiguration sc)
        {
            var desc = Localizer["DailyDesc",[GetDailyQuote(sc), GetStartingOnQuote(sc.StartDate)]];
            //var desc = Rm.GetString("DailyDesc", CultureInfo.CurrentCulture);
            return desc;
        }

        public static string GetWeeklyDescription(SchedulerConfiguration sc)
        {
            var desc = Localizer["WeeklyDesc",[GetWeeklyQuote(sc), GetDailyQuote(sc), GetStartingOnQuote(sc.StartDate)]];
            return desc;
        }

        public static string GetMonthlyDescription(SchedulerConfiguration sc)
        {
            var desc = Localizer["MonthlyDesc",
                [GetMonthlyQuote(sc), GetDailyQuote(sc), GetStartingOnQuote(sc.StartDate)]];
            return desc;
        }
        private static string GetDailyQuote(SchedulerConfiguration sc)
        {
            string quote;
            if (sc.DailyType == DailyOccursType.Every)
            {
                var startingAt = sc.DailyStartingAt.ToString(@"hh\:mm");
                var endingAt = sc.DailyEndingAt.ToString(@"hh\:mm");
                var interval = OccurrenceDictionaries.GetIntervalQuote(sc.DailyOccursEvery, sc.OccursEveryType);
                
                quote = Localizer["EveryDayQuote", [interval,startingAt, endingAt]];
                return quote;
            }
            var formmattedOnceAt = sc.DailyOccursOnceAt.ToString(@"hh\:mm");
            quote = Localizer["OnceDayQuote", formmattedOnceAt];
            return quote;
        }

        private static string GetStartingOnQuote(DateTime stDate)
        {
            var formattedStartDate = stDate.ToShortDateString();
            var quote = Localizer["StartingOn", formattedStartDate];
            return quote;
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
                quote =Localizer["MonthlyDayQuote", [sc.MonthlyDay, sc.MonthlyDayFrequency]];
                return quote;
            }

            quote = Localizer["MonthlyDateQuote", sc.MonthlyDateFrequency];
            return
                $"{MonthlyDictionaries.GetOrderDayQuote(sc.MonthlyDateOrder, sc.MonthlyDateDay)} {quote}";
        }

    }
}
