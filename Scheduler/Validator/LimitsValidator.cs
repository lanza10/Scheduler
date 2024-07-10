using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Interfaces;
using Scheduler.Models;

namespace Scheduler.Validator
{
    public class LimitsValidator
    {
        public static bool ValidLimits(DateTime startDate, DateTime endDate)
        {
            return endDate >= startDate;
        }
    }
}
