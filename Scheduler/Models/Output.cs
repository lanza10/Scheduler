namespace Scheduler.Models
{
    public class Output(DateTime nextExecTime, string description, List<DateTime> allNextDates)
    {
        public DateTime NextExecTime { get; } = nextExecTime;
        public List<DateTime>? RecurringDates { get; } = allNextDates;
        public string Description { get; } = description;
    }
}
