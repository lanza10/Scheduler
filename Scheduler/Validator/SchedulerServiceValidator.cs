using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Exceptions;
using Scheduler.Interfaces;
using Scheduler.Models;

namespace Scheduler.Validator
{
    public class SchedulerServiceValidator
    {
        public static void ValidateResultDoNotExceedLimits(DateTime date, ILimits limits)
        {
            if (date < limits.StartDate || date > limits.EndDate)
            {
                throw new LimitsException("The result date is out of the limits");
            }
        }
    }
}
