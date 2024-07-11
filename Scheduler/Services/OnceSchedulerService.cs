using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Exceptions;
using Scheduler.Interfaces;
using Scheduler.Models;
using Scheduler.Services.Interfaces;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public class OnceSchedulerService : ISchedulerService
    {

        public DateTime CalculateNextDate(ISchedulerInput schedulerInput)
        {
            var res = (DateTime)schedulerInput.Configuration.Date!;
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(res, schedulerInput.Limits);
            return res;
        }

        public string GenerateDescription(ISchedulerInput schedulerInput)
        {
            var formattedNextExecTime = CalculateNextDate(schedulerInput).ToString("dd/MM/yyyy 'at' HH:mm");
            var formattedStartDate = schedulerInput.Limits.StartDate.ToString("dd/MM/yyyy");
            return $"Occurs once.Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }
    }
}
