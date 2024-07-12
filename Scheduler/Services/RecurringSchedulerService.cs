using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Exceptions;
using Scheduler.Interfaces;
using Scheduler.Services.Interfaces;
using Scheduler.Utilities;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public class RecurringSchedulerService : ISchedulerService
    {
        public DateTime CalculateNextDate(ISchedulerInput schedulerInput)
        {
            var res = schedulerInput.CurrentDate.AddDays(schedulerInput.Configuration.Days);
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(res, schedulerInput.Limits);
            return res;
        }

        public string GenerateDescription(ISchedulerInput schedulerInput)
        {
            if (!OccurrenceDictionary.OccurrenceMap.TryGetValue(schedulerInput.Configuration.Occurs, out var frequency))
            {
                throw new KeyNotFoundException();
            }
            var nextExecTime = CalculateNextDate(schedulerInput);
            var formattedNextExecTime = nextExecTime.ToString("dd/MM/yyyy 'at' HH:mm");
            var formattedStartDate = schedulerInput.Limits.StartDate.ToString("dd/MM/yyyy");
            return
                $"Occurs every {frequency}." +
                $"Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }
    }
}
