

using Scheduler.Models;

namespace Scheduler.Interfaces
{
    public interface ISchedulerInput
    {
        DateTime CurrentDate { get; }
        IConfiguration Configuration { get; } 
        Limits Limits { get; }
    }
}
