using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Utilities;

namespace Scheduler.Services
{
    public class RecurringWeeklySchedulerService(SchedulerConfiguration sc) : ISchedulerService
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

            return HoursCalculatorService.GetDatesOfDays(datesList, sc, maxLength, currentDate.TimeOfDay);
        }

        public DateTime CalculateFirstDate()
        {
            var currentDate = sc.CurrentDate;
            while (!sc.DaysOfWeek.Contains(currentDate.DayOfWeek))
            {
                currentDate = currentDate.Date.AddDays(1);
            }


            return HoursCalculatorService.CalculateNextHour(currentDate, sc);


        }

        public string GenerateDescription(DateTime date)
        {
            var daysOfWeek = WeeklyDictionaries.GetDaysQuote(sc.DaysOfWeek);
            var frequency = OccurrenceDictionaries.GetFrequencyQuote(sc.WeeklyFrequency, sc.Occurs);
            var dailyQuote = GetDailyQuote();


            return
                $"Occurs every {frequency} on {daysOfWeek} " + dailyQuote;
        }

        private string GetDailyQuote()
        {
            string result;
            var formattedStartDate = sc.StartDate.ToString("dd/MM/yyyy");
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
