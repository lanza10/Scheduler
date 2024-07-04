

namespace Scheduler.Interfaces
{
    public interface ISchedulerInput
    {
        IInput Input { get; }
        IConfiguration Configuration { get; } 
        ILimits Limits { get; }
    }
}
