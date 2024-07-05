using Scheduler.Interfaces;

namespace Scheduler.Models
{
    public class Output(DateTime nextExecTime, string description) : IOutput
    {
        public DateTime NextExecTime { get; set; } = nextExecTime;
        public string Description { get; set; } = description;
    }
}
