using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Utilities;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public class SchedulerService
    {   
        public const int MaxDates = 7;
        private readonly SchedulerConfiguration _configuration;

        public SchedulerService(SchedulerConfiguration sc)
        {
            SchedulerValidator.ValidateSchedulerConfiguration(sc);
            _configuration = sc;
        }
        public DateTime CalculateNextDate()
        {
            var resultDate = (DateTime)_configuration.ConfigurationDate!;
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(resultDate, _configuration.StartDate, _configuration.EndDate);
            return resultDate;
        }

        public string GenerateDescriptionOnce()
        {
            var formattedNextExecTime = CalculateNextDate().ToString("dd/MM/yyyy 'at' HH:mm");
            var formattedStartDate = _configuration.StartDate.ToString("dd/MM/yyyy");
            return $"Occurs once.Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }
        public string GenerateDescriptionRecurring()
        {
            if (!OccurrenceDictionary.OccurrenceMap.TryGetValue(_configuration.Occurs, out var frequency))
            {
                throw new KeyNotFoundException();
            }
            var nextExecTime = CalculateRecurringDates().First();
            var formattedNextExecTime = nextExecTime.ToString("dd/MM/yyyy 'at' HH:mm");
            var formattedStartDate = _configuration.StartDate.ToString("dd/MM/yyyy");
            return
                $"Occurs every {frequency}." +
                $"Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }
    

        public List<DateTime> CalculateRecurringDates()
        {
            var auxDate = _configuration.CurrentDate.AddDays(_configuration.Days);
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(auxDate, _configuration.StartDate, _configuration.EndDate);
            var recurringDates = new List<DateTime>();
            for (var i = 0; i < MaxDates && auxDate <= _configuration.EndDate;i++)
            {
                recurringDates.Add(auxDate);
                auxDate = auxDate.AddDays(_configuration.Days);
            }
            return recurringDates;
        }
        public Output GetOutput()
        {
            switch (_configuration.Type)
            {
                case ConfigurationType.Once:
                    return GetOnceOutput();
                default:
                    return GetRecurringOutput();
            }
        }

        public Output GetOnceOutput()
        {
            return new Output(CalculateNextDate(), GenerateDescriptionOnce(), null);
        }
        public Output GetRecurringOutput()
        {
            var recurringDates = CalculateRecurringDates();
            return new Output(recurringDates.First(), GenerateDescriptionRecurring(), recurringDates);
        }
    }
}
