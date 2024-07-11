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
    public class LimitsValidator
    {
        public static void ValidLimits(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new LimitsException("Start date must be earlier than the end date");
            }
        }
    }
}
