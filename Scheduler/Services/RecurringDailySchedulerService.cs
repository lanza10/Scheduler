
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services.HoursCalculators;
using Scheduler.Utilities;
using Scheduler.Validator;
using System.Globalization;

namespace Scheduler.Services
{
    public class RecurringDailySchedulerService(SchedulerConfiguration sc, IHoursCalculator hc) : ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int maxLength)
        {
            var currentDate = CalculateFirstDate();
            var initTime = currentDate.TimeOfDay;
            var dateList = new List<DateTime>();


            while (currentDate <= sc.EndDate && dateList.Count < maxLength)
            {
                dateList.Add(currentDate.Date);
                currentDate = currentDate.AddDays(1);
            }


            var resultList = hc.GetHoursOfDates(dateList, sc, maxLength, initTime);
            return resultList;
        }
        public DateTime CalculateFirstDate()
        {
            var resultDate = hc.CalculateNextHour(sc.CurrentDate, sc);
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(resultDate, sc.StartDate, sc.EndDate);
            return resultDate;
        }

        public string GenerateDescription(DateTime date)
        {
            var culture = CultureInfo.CurrentCulture;
            var formattedStartDate = sc.StartDate.ToString("d", culture);

            if (sc.DailyType == DailyOccursType.Every)
            {
                return GenerateDescriptionWhenEvery(formattedStartDate);
            }

            return GenerateDescriptionWhenOnce(formattedStartDate);
        }

        private string GenerateDescriptionWhenEvery(string startingDate)
        {
            var startingAt = sc.DailyStartingAt.ToString(@"hh\:mm");
            var endingAt = sc.DailyEndingAt.ToString(@"hh\:mm");
            var interval = OccurrenceDictionaries.GetIntervalQuote(sc.DailyOccursEvery, sc.OccursEveryType);
            return
                $"Occurs every day, every {interval} between {startingAt} and {endingAt} starting on {startingDate}";
        }
        private string GenerateDescriptionWhenOnce(string startingDate)
        {
            var formmattedOnceAt = sc.DailyOccursOnceAt.ToString(@"hh\:mm");
            return $"Occurs every day at {formmattedOnceAt} starting on {startingDate}";
        }
    }
}
