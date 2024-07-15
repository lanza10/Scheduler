namespace Scheduler.Services
{
    public interface ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates();
        public DateTime CalculateNextDate();
        public string GenerateDescription();
    }
}
