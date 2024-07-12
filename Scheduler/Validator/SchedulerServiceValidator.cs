using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Scheduler.Exceptions;
using Scheduler.Interfaces;
using Scheduler.Models;

namespace Scheduler.Validator
{
    public class SchedulerServiceValidator
    {
        public static void ValidateResultDoNotExceedLimits(DateTime date, Limits limits)
        {
            if (date < limits.StartDate)
            {
                throw new SchedulerException("The result date must not be earlier than the specified start date.");
            }
            if (date > limits.EndDate)
            {
                throw new SchedulerException("The result date must not be later than the specified end date.");
            }
        }
    }
}
