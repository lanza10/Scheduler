using Scheduler.Enums;
using Scheduler.Models;

namespace Scheduler.Services.HoursCalculators
{
    public class HoursCalculatorEvery : IHoursCalculator
    {
        public List<DateTime> GetHoursOfDates(List<DateTime> datesList, SchedulerConfiguration sc,
            int maxLength, TimeSpan initTime)
        {
            var interval = GetSpan(sc);
            var resultList = new List<DateTime>();


            foreach (var date in datesList)
            {
                var currentDateTime = date.Add(initTime);
                var limitOfDay = currentDateTime.Date.Add(sc.DailyEndingAt);

                if (resultList.Count == maxLength)
                {
                    break;
                }

                while (currentDateTime <= sc.EndDate && resultList.Count < maxLength)
                {
                    resultList.Add(currentDateTime);
                    currentDateTime = currentDateTime.Add(interval);

                    if (currentDateTime > limitOfDay)
                    {
                        initTime = sc.DailyStartingAt;
                        break;
                    }
                }
            }

            return resultList;
        }

        public DateTime CalculateNextHour(DateTime currentDate, SchedulerConfiguration sc)
        {
            var currentTime = currentDate.TimeOfDay;
            if (currentTime < sc.DailyStartingAt)
            {
                return currentDate.Date.Add(sc.DailyStartingAt);
            }

            if (currentTime >= sc.DailyStartingAt && currentTime <= sc.DailyEndingAt)
            {
                return currentDate;
            }

            return currentDate.Date.AddDays(1).Add(sc.DailyStartingAt);
        }
        private static TimeSpan GetSpan(SchedulerConfiguration sc)
        {
            switch (sc.OccursEveryType)
            {
                case DailyOccursEveryType.Hours:
                    return new TimeSpan(sc.DailyOccursEvery, 0, 0);
                case DailyOccursEveryType.Minutes:
                    return new TimeSpan(0, sc.DailyOccursEvery, 0);
                default:
                    return new TimeSpan(0, 0, sc.DailyOccursEvery);
            }
        }
        public DateTime CalculateStartLimit(DateTime date, SchedulerConfiguration sc)
        {
            return date.Date.Add(sc.DailyStartingAt);
        }
        public DateTime CalculateEndLimit(DateTime date, SchedulerConfiguration sc)
        {
            return date.Date.Add(sc.DailyEndingAt);
        }
    }
}
