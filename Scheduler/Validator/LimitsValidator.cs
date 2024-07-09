using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Interfaces;

namespace Scheduler.Validator
{
    public class LimitsValidator
    {
        public static bool ValidLimits(ILimits limits)
        {
            if (limits.EndDate.HasValue)
            {
                return limits.EndDate >= limits.StartDate;
            }

            return true;
        }
    }
}
