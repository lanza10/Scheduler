using Scheduler.Interfaces;

namespace Scheduler.Models
{
    public class SchedulerInput(IConfiguration configuration, IInput input, ILimits limits) : ISchedulerInput
    {
        public required IInput Input { get; set; } = input;
        public required IConfiguration Configuration { get; set; } = configuration;
        public required ILimits Limits { get; set; } = limits;
    }
}
