

using Scheduler.Models;

namespace Scheduler.Interfaces
{
    public interface ISchedulerInput
    {
        IInput Input { get; }
        IConfiguration Configuration { get; } 
        Limits Limits { get; }
    }
}
