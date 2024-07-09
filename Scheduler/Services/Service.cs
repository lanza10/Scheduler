using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Enums;
using Scheduler.Interfaces;
using Scheduler.Models;

namespace Scheduler.Services
{
    public class Service : IService
    {
        public ISchedulerService GetSchedulerService(ISchedulerInput schedulerInput)
        {
            if (schedulerInput.Configuration.Type == ConfigurationType.Once )
            {
                return new OnceSchedulerService();
            }

            return new RecurringSchedulerService();
        }

        public IOutput CalculateOutput(ISchedulerInput schedulerInput)
        {
            var schedulerService = GetSchedulerService(schedulerInput);
            return new Output(schedulerService.CalculateNextDate(schedulerInput),
                schedulerService.GenerateDescription(schedulerInput));
        }
    }
}

