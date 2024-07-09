using Scheduler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Services
{
    public interface ISchedulerService
    {
        DateTime CalculateNextDate(ISchedulerInput schedulerInput);

        string GenerateDescription(ISchedulerInput schedulerInput);  
    }
}
