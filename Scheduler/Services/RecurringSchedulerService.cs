using Scheduler.Models;
using Scheduler.Utilities;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public class RecurringSchedulerService(SchedulerConfiguration sc) : ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int maxLength)
        {
            var auxDate = CalculateNextDate();
            var recurringDates = new List<DateTime>();
            for (var i = 0; i < maxLength && auxDate <= sc.EndDate; i++)
            {
                recurringDates.Add(auxDate);
                auxDate = auxDate.AddDays(sc.Days);
            }
            return recurringDates;
        }
        public DateTime CalculateNextDate()
        {
            var nextDate = sc.CurrentDate.AddDays(sc.Days);
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(nextDate, sc.StartDate, sc.EndDate);
            return nextDate;
        }

        public string GenerateDescription(DateTime date)
        {
            var frequency = OccurrenceDictionary.GetFrequencyQuote(sc.Days, sc.Occurs);
            var formattedNextExecTime = date.ToString("dd/MM/yyyy 'at' HH:mm");
            var formattedStartDate = sc.StartDate.ToString("dd/MM/yyyy");
            return
                $"Occurs every {frequency}." +
                $"Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }
    }
}
