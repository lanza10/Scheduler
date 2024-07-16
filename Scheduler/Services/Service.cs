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
            var date = _schedulerService.CalculateNextDate();
            return new Output(date, _schedulerService.GenerateDescription(date));
        }

        public List<Output> GetOutputList(int maxLength)
        {
            var outputList = new List<Output>();
            var allDates = _schedulerService.CalculateAllNextDates(maxLength);
            foreach (var date in allDates)
            {
                outputList.Add(new Output(date, _schedulerService.GenerateDescription(date)));
            }
            return outputList;
        }

    }
}
