using Scheduler.Validator;
using Scheduler.Models;
using System.Globalization;

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
            var culture = CultureInfo.CurrentCulture;
            var formattedNextExecTime = date.ToString("dd/MM/yyyy 'at' HH:mm", culture);
            var formattedStartDate = sc.StartDate.ToString("d", culture);
            return $"Occurs once.Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }
    }
}
