namespace Scheduler.Models
{
    public class Output(DateTime nextExecTime, string description, List<DateTime> allNextDates)
    {
        public DateTime NextExecTime { get; } = nextExecTime;
        public List<DateTime> AllNextDates { get; } = allNextDates;
        public string Description { get; } = description;
    }
}
