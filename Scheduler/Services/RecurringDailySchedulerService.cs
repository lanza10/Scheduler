
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services.HoursCalculators;
using Scheduler.Utilities;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public class RecurringDailySchedulerService(SchedulerConfiguration sc, IHoursCalculator hc) : ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int maxLength)
        {
            var auxDate = CalculateFirstDate();
            var initTime = auxDate.TimeOfDay;
            var dateList = new List<DateTime>();


            while (auxDate <= sc.EndDate && dateList.Count < maxLength)
            {
                dateList.Add(auxDate.Date);
                auxDate = auxDate.AddDays(1);
            }


            var resultList = hc.GetHoursOfDates(dateList, sc, maxLength, initTime);
            return resultList;
        }
        public DateTime CalculateFirstDate()
        {
           return hc.CalculateNextHour(sc.CurrentDate, sc);
        }

        public string GenerateDescription(DateTime date)
        {
            var formattedStartDate = sc.StartDate.ToString("dd/MM/yyyy");

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
