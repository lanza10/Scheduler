using Scheduler.Interfaces;

namespace Scheduler.Models
{
    public class SchedulerInput(IInput input, IConfiguration configuration, Limits limits) : ISchedulerInput
    {
        public IInput Input { get; } = input;
        public IConfiguration Configuration { get; } = configuration;
        public Limits Limits { get; } = limits;
    }
}
