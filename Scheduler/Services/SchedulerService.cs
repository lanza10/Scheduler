using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Models;
using Scheduler.Utilities;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public class SchedulerService
    {   
        public const int MaxDates = 7;
        public DateTime CalculateNextDate(SchedulerConfiguration sc)
        {
            var res = (DateTime)sc.ConfigurationDate!;
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(res, sc.StartDate, sc.EndDate);
            return res;
        }

        public string GenerateDescriptionOnce(SchedulerConfiguration sc)
        {
            var formattedNextExecTime = CalculateNextDate(sc).ToString("dd/MM/yyyy 'at' HH:mm");
            var formattedStartDate = sc.StartDate.ToString("dd/MM/yyyy");
            return $"Occurs once.Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }
        public string GenerateDescriptionRecurring(SchedulerConfiguration sc)
        {
            if (!OccurrenceDictionary.OccurrenceMap.TryGetValue(sc.Occurs, out var frequency))
            {
                throw new KeyNotFoundException();
            }
            var nextExecTime = CalculateRecurringDates(sc)[0];
            var formattedNextExecTime = nextExecTime.ToString("dd/MM/yyyy 'at' HH:mm");
            var formattedStartDate = sc.StartDate.ToString("dd/MM/yyyy");
            return
                $"Occurs every {frequency}." +
                $"Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }
    

        public DateTime[] CalculateRecurringDates(SchedulerConfiguration sc)
        {
            var auxDate = sc.CurrentDate.AddDays(sc.Days);
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(auxDate, sc.StartDate, sc.EndDate);
            var recurringDates = new List<DateTime>();
            for (var i = 0; i <= MaxDates && auxDate <= sc.EndDate;i++)
            {
                recurringDates.Add(auxDate);
                auxDate = auxDate.AddDays(sc.Days);
            }
            return [.. recurringDates];
        }
    }
}
