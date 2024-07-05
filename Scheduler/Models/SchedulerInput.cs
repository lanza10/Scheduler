using Scheduler.Interfaces;

namespace Scheduler.Models
{
    public class SchedulerInput(IInput input, IConfiguration configuration, ILimits limits) : ISchedulerInput
    {
        public IInput Input { get; set; } = input;
        public IConfiguration Configuration { get; set; } = configuration;
        public ILimits Limits { get; set; } = limits;
    }
}
