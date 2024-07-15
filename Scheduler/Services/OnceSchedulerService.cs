using Scheduler.Validator;
using Scheduler.Models;

namespace Scheduler.Services
{
    public class OnceSchedulerService(SchedulerConfiguration sc) : ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates()
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

        public string GenerateDescription()
        {
            var formattedNextExecTime = CalculateNextDate().ToString("dd/MM/yyyy 'at' HH:mm");
            var formattedStartDate = sc.StartDate.ToString("dd/MM/yyyy");
            return $"Occurs once.Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }
    }
}
