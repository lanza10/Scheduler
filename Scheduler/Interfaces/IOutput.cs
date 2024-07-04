

namespace Scheduler.Interfaces
{
    public interface IOutput
    {
        DateTime NextExecTime { get; }
        string Description { get; }
    }
}
