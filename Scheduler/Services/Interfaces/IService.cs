using Scheduler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Services.Interfaces
{
    public interface IService
    {
        ISchedulerService GetSchedulerService(ISchedulerInput schedulerInput);
        IOutput CalculateOutput(ISchedulerInput schedulerInput);
    }
}
