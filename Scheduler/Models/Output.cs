using Scheduler.Interfaces;

namespace Scheduler.Models
{
    public class Output(DateTime nextExecTime, string description) : IOutput
    {
        public DateTime NextExecTime { get; } = nextExecTime;
        public string Description { get; } = description;
    }
}
