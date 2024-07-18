using Scheduler.Enums;
using Scheduler.Models;

namespace Scheduler.Services
{
    public static class AuxiliarService
    {
        public static List<DateTime> GetDatesOfDay(SchedulerConfiguration sc, DateTime date, List<DateTime> datesList,
            int maxLength, TimeSpan span)
        {
            if (sc.DailyType == DailyOccursType.Once)
            {
                return GetDatesOfDayTypeOnce(sc, date, datesList, maxLength);
            }
            return GetDatesOfDayTypeEvery(sc, date, datesList, maxLength, span);
        }

        private static List<DateTime> GetDatesOfDayTypeEvery(SchedulerConfiguration sc, DateTime date, List<DateTime> datesList,
            int maxLength, TimeSpan span)
        {
            var auxDate = date;
            while (datesList.Count < maxLength && auxDate.Day == date.Day && auxDate <= sc.EndDate)
            {
                datesList.Add(auxDate);
                auxDate = auxDate.Add(span);

                if (auxDate.TimeOfDay > sc.DailyEndingAt)
                {
                    auxDate = auxDate.Date.AddDays(1)
                        .Add(new TimeSpan(sc.DailyStartingAt.Hours, sc.DailyStartingAt.Minutes,
                            sc.DailyStartingAt.Seconds));
                }
            }

            return datesList;
        }
        private static List<DateTime> GetDatesOfDayTypeOnce(SchedulerConfiguration sc, DateTime date, List<DateTime> datesList,
            int maxLength)
        {
            var auxDate = date;
            while (datesList.Count < maxLength && auxDate.Day == date.Day && auxDate <= sc.EndDate)
            {
                datesList.Add(auxDate);
                auxDate = auxDate.AddDays(1);
            }
            return datesList;
        }
    }
}
