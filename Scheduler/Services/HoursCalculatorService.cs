using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public static class HoursCalculatorService
    {
        public static List<DateTime> GetDatesOfDays(List<DateTime> datesList,  SchedulerConfiguration sc,
            int maxLength, TimeSpan initTime)
        {
            if (sc.DailyType == DailyOccursType.Once)
            {
                return GetDatesOfDayTypeOnce(datesList, sc, maxLength, initTime);
            }
            return GetDatesOfDayTypeEvery(datesList, sc, maxLength, initTime);
        }

        private static List<DateTime> GetDatesOfDayTypeEvery(List<DateTime> datesList, SchedulerConfiguration sc,
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
                        initTime = AdjustInitTime(currentDateTime, sc, interval);
                        break;
                    }
                }
            }
            return resultList;
        }
        private static List<DateTime> GetDatesOfDayTypeOnce(List<DateTime> datesList, SchedulerConfiguration sc,
            int maxLength, TimeSpan initTime)
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

        private static TimeSpan AdjustInitTime(DateTime currentDateTime, SchedulerConfiguration sc, TimeSpan interval)
        {
            if (currentDateTime.TimeOfDay >= sc.DailyStartingAt && currentDateTime.TimeOfDay <= sc.DailyEndingAt)
            {
                return currentDateTime.TimeOfDay;
            }
            return sc.DailyStartingAt;
        }
        public static TimeSpan GetSpan(SchedulerConfiguration sc)
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

        public static DateTime CalculateNextHour(DateTime currentDate, SchedulerConfiguration sc)
        {
            DateTime nextDate;
            if (sc.DailyType == DailyOccursType.Once)
            {
                nextDate = GetFirstHourWhenOnce(currentDate, sc);
            }
            else
            {
                nextDate = GetFirstHourWhenEvery(currentDate, sc);
            }
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(nextDate, sc.StartDate, sc.EndDate);
            return nextDate;
        }
        private static DateTime GetFirstHourWhenOnce(DateTime currentDate, SchedulerConfiguration sc)
        {
            return sc.CurrentDate.TimeOfDay > sc.DailyOccursOnceAt
                ? currentDate.Date.AddDays(1).Add(sc.DailyOccursOnceAt)
                : currentDate.Date.Add(sc.DailyOccursOnceAt);
        }
        private static DateTime GetFirstHourWhenEvery(DateTime currentDate, SchedulerConfiguration sc)
        {
            var currentTime = currentDate.TimeOfDay;
            if (currentTime < sc.DailyStartingAt)
            {
                return currentDate.Date.Add(sc.DailyStartingAt);
            }
            if (currentTime >= sc.DailyStartingAt && currentTime <= sc.DailyEndingAt)
            {
                return sc.CurrentDate;
            }

            return currentDate.Date.AddDays(1).Add(sc.DailyStartingAt);
        }
    }
}
