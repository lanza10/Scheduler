using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class Output
    {
        public DateTime NextExecTime { get; set; }
        public required string Description { get; set; }

        public Output(DateTime nextExecTime, String description)
        {
            this.NextExecTime = nextExecTime; 
            this.Description = description;
        }
    }
}
