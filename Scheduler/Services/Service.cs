using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services.HoursCalculators;
using Scheduler.Services.SchedulerServices;
using Scheduler.Utilities;
using Scheduler.Validator;
using System.Globalization;

namespace Scheduler.Services
{
    public class Service
    {

        private readonly ISchedulerService _schedulerService;

        public Service(SchedulerConfiguration sc)
        {
            SchedulerValidator.ValidateSchedulerConfiguration(sc);

            _schedulerService = sc.Type == ConfigurationType.Once ? new OnceSchedulerService(sc) : GetRecurringService(sc);

            CultureInfo.CurrentCulture = LanguageDictionary.GetCulture(sc.DescriptionLanguage);
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

        private IHoursCalculator GetHoursCalculator(SchedulerConfiguration sc)
        {
            if (sc.DailyType == DailyOccursType.Every)
            {
                return new HoursCalculatorEvery();
            }
            return new HoursCalculatorOnce();
        }

        private ISchedulerService GetRecurringService(SchedulerConfiguration sc)
        {
            var hc = GetHoursCalculator(sc);
            switch (sc.Occurs)
            {
                case Occurrence.Daily:
                    return new RecurringDailySchedulerService(sc, hc);
                case Occurrence.Weekly:
                    return new RecurringWeeklySchedulerService(sc, hc);
                default:
                    return new RecurringMonthlySchedulerService(sc, hc);
            }
        }
    }
}
