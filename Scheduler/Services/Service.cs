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

            if (sc.Type == ConfigurationType.Once)
            {
                _schedulerService = new OnceSchedulerService(sc);
            }
            else
            {
                _schedulerService = sc.Occurs switch
                {
                    Occurrence.Daily => new RecurringDailySchedulerService(sc),
                    //Occurrence.Weekly => new RecurringMonthlySchedulerService(sc)new RecurringWeeklySchedulerService(sc),
                    _ => new RecurringWeeklySchedulerService(sc)
                };
            }
        }
        public Output GetOutput()
        {
            var date = _schedulerService.CalculateFirstDate();
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
