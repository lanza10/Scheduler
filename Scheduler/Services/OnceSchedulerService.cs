using Scheduler.Validator;
using Scheduler.Models;

namespace Scheduler.Services
{
    public class OnceSchedulerService(SchedulerConfiguration sc) : ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int length)
        {
            var datesList = new List<DateTime> { CalculateNextDate() };
            return datesList;
        }

        public DateTime CalculateNextDate()
        {
            var resultDate = (DateTime)sc.ConfigurationDate!;
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(resultDate, sc.StartDate, sc.EndDate);
            return resultDate;
        }

        public string GenerateDescription(DateTime date)
        {
            var formattedNextExecTime = date.ToString("dd/MM/yyyy 'at' HH:mm");
            var formattedStartDate = sc.StartDate.ToString("dd/MM/yyyy");
            return $"Occurs once.Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }
    }
}
