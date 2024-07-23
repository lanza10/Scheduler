using System.Globalization;
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services.HoursCalculators;
using Scheduler.Utilities;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public class RecurringWeeklySchedulerService(SchedulerConfiguration sc, IHoursCalculator hc) : ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int maxLength)
        {
            var currentDate = CalculateFirstDate();
            var initDate = currentDate;
            var initDayOfWeek = sc.CurrentDate.DayOfWeek;
            var datesList = new List<DateTime>();
            while (datesList.Count < maxLength && currentDate <= sc.EndDate)
            {
                if (sc.DaysOfWeek.Contains(currentDate.DayOfWeek))
                {
                    datesList.Add(currentDate.Date);
                }
                currentDate = currentDate.AddDays(1);
                

                if (currentDate.DayOfWeek == initDayOfWeek && currentDate != initDate )
                {
                    currentDate = currentDate.AddDays(7 * (sc.WeeklyFrequency - 1));
                }
            }

            return hc.GetHoursOfDates(datesList, sc, maxLength, currentDate.TimeOfDay);
        }

        public DateTime CalculateFirstDate()
        {
            var currentDate = sc.CurrentDate;
            var initDate = currentDate;
            var initDayOfWeek = sc.CurrentDate.DayOfWeek;
            while (!sc.DaysOfWeek.Contains(currentDate.DayOfWeek) || currentDate.TimeOfDay > sc.DailyEndingAt)
            {
                currentDate = currentDate.Date.AddDays(1);

                if (currentDate.DayOfWeek == initDayOfWeek && currentDate != initDate)
                {
                    currentDate = currentDate.AddDays(7 * (sc.WeeklyFrequency - 1));
                }
            }


            var resultDate =  hc.CalculateNextHour(currentDate, sc);
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(resultDate,sc.StartDate,sc.EndDate);
            return resultDate;
        }

        public string GenerateDescription(DateTime date)
        {
            var daysOfWeek = WeeklyDictionaries.GetDaysQuote(sc.DaysOfWeek);
            var frequency = OccurrenceDictionaries.GetFrequencyQuote(sc.WeeklyFrequency, sc.Occurs);
            var dailyQuote = GetDailyQuote();


            return
                $"Occurs every {frequency} {daysOfWeek} " + dailyQuote;
        }

        private string GetDailyQuote()
        {
            string result;
            //yield
            var culture = CultureInfo.CurrentCulture;
            var formattedStartDate = sc.StartDate.ToString("d", culture);
            var interval = OccurrenceDictionaries.GetIntervalQuote(sc.DailyOccursEvery, sc.OccursEveryType);
            var startingAt = sc.DailyStartingAt.ToString(@"hh\:mm");
            var endingAt = sc.DailyEndingAt.ToString(@"hh\:mm");
            var formmattedOnceAt = sc.DailyOccursOnceAt.ToString(@"hh\:mm");

            if (sc.DailyType == DailyOccursType.Once)
            {
                result = $"at {formmattedOnceAt}";
            }
            else
            {
                result = $"every {interval} between {startingAt} and {endingAt}";
            }
            return result + $" starting on {formattedStartDate}";
        }
    }
}
