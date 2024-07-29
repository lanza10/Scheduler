using Scheduler.Models;
using Scheduler.Validator;

namespace Scheduler.Services.SchedulerServices
{
    public class OnceSchedulerService(SchedulerConfiguration sc) : ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int length)
        {
            var datesList = new List<DateTime> { CalculateFirstDate() };
            return datesList;
        }

        public DateTime CalculateFirstDate()
        {
            var resultDate = (DateTime)sc.ConfigurationDate!;
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(resultDate, sc.StartDate, sc.EndDate);
            return resultDate;
        }

        public string GenerateDescription(DateTime date)
        {
            return DescriptionCalculator.GeOnceDescription(date, sc);
        }
    }
}
