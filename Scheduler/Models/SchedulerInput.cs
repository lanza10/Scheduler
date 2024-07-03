using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Interfaces;

namespace Scheduler.Models
{
    public class SchedulerInput
    {
        public required IInput Input { get; set; }
        public required Configuration Configuration { get; set; }
        public required Limits Limits { get; set; }

        public SchedulerInput(Configuration configuration, IInput input, Limits limits)
        {
            this.Configuration = configuration;
            this.Input = input;
            this.Limits = limits;
        }
    }
}
