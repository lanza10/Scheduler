using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Interfaces;
using Scheduler.Utilities;

namespace Scheduler.Services
{
    public class RecurringSchedulerService : ISchedulerService
    {
        public DateTime CalculateNextDate(ISchedulerInput schedulerInput)
        {
            return schedulerInput.Input.CurrentDate.AddDays(schedulerInput.Configuration.Days);
        }

        public string GenerateDescription(ISchedulerInput schedulerInput)
        {
            var nextExecTime = CalculateNextDate(schedulerInput);
            var formattedNextExecTime = nextExecTime.ToString("dd/MM/yyyy 'at' HH:mm");
            var formattedStartDate = schedulerInput.Limits.StartDate.ToString("dd/MM/yyyy");
            return
                $"Occurs every {DictionaryUtils.GetValue(schedulerInput.Configuration.Occurs)}." +
                $"Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }
    }
}
