using Scheduler.Models;
using Scheduler.Utilities;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public class RecurringSchedulerService(SchedulerConfiguration sc) : ISchedulerService
    {
        public const int MaxDates = 7;
        public List<DateTime> CalculateAllNextDates()
        {
            var auxDate = sc.CurrentDate.AddDays(sc.Days);
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(auxDate, sc.StartDate, sc.EndDate);
            var recurringDates = new List<DateTime>();
            for (var i = 0; i < MaxDates && auxDate <= sc.EndDate; i++)
            {
                recurringDates.Add(auxDate);
                auxDate = auxDate.AddDays(sc.Days);
            }
            return recurringDates;
        }
        public DateTime CalculateNextDate()
        {
            return CalculateAllNextDates().First();
        }

        public string GenerateDescription()
        {
            if (!OccurrenceDictionary.OccurrenceMap.TryGetValue(sc.Occurs, out var frequency))
            {
                throw new KeyNotFoundException();
            }
            var nextExecTime = CalculateNextDate();
            var formattedNextExecTime = nextExecTime.ToString("dd/MM/yyyy 'at' HH:mm");
            var formattedStartDate = sc.StartDate.ToString("dd/MM/yyyy");
            return
                $"Occurs every {frequency}." +
                $"Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }
    }
}
