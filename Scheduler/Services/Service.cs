using Scheduler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Services
{
    public class Service : IService
    {
        private SchedulerInputService schedulerInputService;

        public DateTime CalculateNextDate()
        {
            return schedulerInputService.CalculateNextDate();
        }

        public string GenerateDescription()
        {
            return schedulerInputService.GenerateDescription();
        }
    }
}
