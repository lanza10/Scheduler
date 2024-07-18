using Scheduler.Models;
using Scheduler.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Enums;

namespace Scheduler.Services
{
    public class RecurringWeeklySchedulerService(SchedulerConfiguration sc) : ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int maxLength)
        {
            var auxDate = sc.CurrentDate; 
            var resultList = new List<DateTime>();
            while (resultList.Count < maxLength && auxDate <= sc.EndDate)
            {
                if (sc.DaysOfWeek.Contains(auxDate.DayOfWeek))
                {
                    AuxiliarService.GetDatesOfDay(sc, auxDate, resultList, maxLength);
                }
                else
                {
                   auxDate = auxDate.AddDays(1);
                }

                if (auxDate.Day - sc.CurrentDate.Day == 7)
                {
                    auxDate = auxDate.AddDays(7 * (sc.WeeklyFrequency - 1));
                }
            }
            return resultList;
        }

        public DateTime CalculateNextDate()
        {
            var auxDate = sc.CurrentDate;
            while (!sc.DaysOfWeek.Contains(auxDate.DayOfWeek))
            {
                auxDate = auxDate.AddDays(1);
            }

            auxDate = auxDate.Date;
            return auxDate;

        }

        public string GenerateDescription(DateTime date)
        {
            var frequency = OccurrenceDictionary.GetFrequencyQuote(sc.WeeklyFrequency, sc.Occurs);
            var formattedStartDate = sc.StartDate.ToString("dd/MM/yyyy");
            var startingAt = sc.DailyStartingAt.ToString(@"hh\:mm");
            var endingAt = sc.DailyEndingAt.ToString(@"hh\:mm");
            var interval = OccurrenceDictionary.GetIntervalQuote(sc.DailyOccursEvery, sc.OccursEveryType);
            return
                $"Occurs every {frequency}, every {interval} between {startingAt} and {endingAt} starting on {formattedStartDate}";
        }
    }
}
