using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public class Service
    {   

        private readonly ISchedulerService _schedulerService;

        public Service(SchedulerConfiguration sc)
        {
            SchedulerValidator.ValidateSchedulerConfiguration(sc);
            switch (sc.Type)
            {
                case ConfigurationType.Once:
                    _schedulerService = new OnceSchedulerService(sc);
                    break;
                default:
                    _schedulerService = new RecurringSchedulerService(sc);
                    break;
            }
        }
        public Output GetOutput()
        {
            return new Output(_schedulerService.CalculateNextDate(), _schedulerService.GenerateDescription(), _schedulerService.CalculateAllNextDates());
        }

    }
}
