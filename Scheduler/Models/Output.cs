namespace Scheduler.Models
{
    public class Output(DateTime nextExecTime, string description)
    {
        public DateTime NextExecTime { get; } = nextExecTime;
        public string Description { get; } = description;
    }
}
