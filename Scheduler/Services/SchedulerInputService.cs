using Scheduler.Exceptions;
using Scheduler.Interfaces;
using Scheduler.Models;
using Scheduler.Utilities;

namespace Scheduler.Services
{
    public class SchedulerInputService(ISchedulerInput schedulerInput)
    {
        private readonly ISchedulerInput _schedulerInput = schedulerInput ?? throw new ArgumentNullException(nameof(schedulerInput));

        //public DateTime CalculateNextDate()
        //{
            
        //    var res = schedulerInput.Configuration.Type == Enums.ConfigurationType.Once
        //        ? (DateTime)schedulerInput.Configuration.Date!
        //        : schedulerInput.Input.CurrentDate.AddDays(schedulerInput.Configuration.Days);
        //    if(res < schedulerInput.Limits.StartDate ||  res > schedulerInput.Limits.EndDate)
        //    {
        //        throw new LimitsException("The result date exceeds the limits established.");
        //    }
        //    return res;
        //}

        //public string GenerateDescription()
        //{
        //    return "";
        //}
    }
}
