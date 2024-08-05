using Scheduler.Models;
using Scheduler.Services.HoursCalculators;
using Scheduler.Utilities;
using Scheduler.Validator;
using System.Globalization;

namespace Scheduler.Services.SchedulerServices
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
            CultureInfo.CurrentCulture = LanguageDictionary.GetCulture(sc.DescriptionLanguage);
            return DescriptionGenerator.GetDailyDescription(sc);
        }
    }
}
