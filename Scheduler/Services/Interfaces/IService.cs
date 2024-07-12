using Scheduler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Models;

namespace Scheduler.Services.Interfaces
{
    public interface IService
    {
        ISchedulerService GetSchedulerService(ISchedulerInput schedulerInput);
        Output CalculateOutput(ISchedulerInput schedulerInput);
    }
}
