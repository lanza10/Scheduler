using Scheduler.Models;

namespace Scheduler.Services.HoursCalculators
{
    public class HoursCalculatorOnce : IHoursCalculator
    {
        public List<DateTime> GetHoursOfDates(List<DateTime> datesList, SchedulerConfiguration sc, int maxLength, TimeSpan initTime)
        {
            var resultList = new List<DateTime>();
            var count = 0;

            foreach (var startDate in datesList)
            {
                if (count >= maxLength) break;

                var scheduledDate = startDate.Add(initTime);

                if (scheduledDate > sc.EndDate) break;

                resultList.Add(scheduledDate);
                count++;
            }

            return resultList;
        }

        public DateTime CalculateNextHour(DateTime currentDate, SchedulerConfiguration sc)
        {
            return currentDate.TimeOfDay > sc.DailyOccursOnceAt
                ? currentDate.Date.AddDays(1).Add(sc.DailyOccursOnceAt)
                : currentDate.Date.Add(sc.DailyOccursOnceAt);
        }
    }
}
