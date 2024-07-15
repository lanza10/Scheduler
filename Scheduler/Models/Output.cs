
namespace Scheduler.Models
{
    public class Output(DateTime nextExecTime, string description, List<DateTime>? recurringDates)
    {
        public DateTime NextExecTime { get; } = nextExecTime;
        public List<DateTime>? RecurringDates { get; } = recurringDates;
        public string Description { get; } = description;
    }
}
