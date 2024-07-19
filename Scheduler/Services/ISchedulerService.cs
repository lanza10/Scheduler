namespace Scheduler.Services
{
    public interface ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int length);
        public DateTime CalculateFirstDate();
        public string GenerateDescription(DateTime date);
    }
}
