using Scheduler.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Utilities
{
    public static class OccurrenceDictionary
    {
        public static readonly Dictionary<Occurrence, string> OccurrenceMap = new()
        {
            {Occurrence.Daily, "day" },
        };

    }
}
