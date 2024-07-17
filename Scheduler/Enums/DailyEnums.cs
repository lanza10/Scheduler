using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Enums
{
    public enum DailyOccursType
    { 
        Once,
        Every
    }

    public enum DailyOccursEveryType 
    {
        Hours,
        Minutes,
        Seconds
    }
}
