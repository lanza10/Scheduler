using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Services
{
    public class RecurringWeeklySchedulerService(SchedulerConfiguration sc) : ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int length)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
