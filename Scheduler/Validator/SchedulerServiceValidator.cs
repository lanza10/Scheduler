using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Scheduler.Exceptions;
using Scheduler.Models;

namespace Scheduler.Validator
{
    public class SchedulerServiceValidator
    {
        public static void ValidateResultDoNotExceedLimits(DateTime date, DateTime startDate, DateTime endDate)
        {
            if (date < startDate)
            {
                throw new SchedulerException("The result date must not be earlier than the specified start date.");
            }
            if (date > endDate)
            {
                throw new SchedulerException("The result date must not be later than the specified end date.");
            }
        }
    }
}
