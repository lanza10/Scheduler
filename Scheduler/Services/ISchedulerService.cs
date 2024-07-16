namespace Scheduler.Services
{
    public interface ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int length);
        public DateTime CalculateNextDate();
        public string GenerateDescription(DateTime date);
    }
}
