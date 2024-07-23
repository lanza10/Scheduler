using Scheduler.Models;

namespace Scheduler.Services.HoursCalculators
{
    public interface IHoursCalculator
    {
        public List<DateTime> GetHoursOfDates(List<DateTime> datesList, SchedulerConfiguration sc,
            int maxLength, TimeSpan initTime);

        public DateTime CalculateNextHour(DateTime currentDate, SchedulerConfiguration sc);

    }
}
