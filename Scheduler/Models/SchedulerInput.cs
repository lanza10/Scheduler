using Scheduler.Interfaces;

namespace Scheduler.Models
{
    public class SchedulerInput(DateTime currentDate, IConfiguration configuration, Limits limits) : ISchedulerInput
    {
        public DateTime CurrentDate { get; } = currentDate;
        public IConfiguration Configuration { get; } = configuration;
        public Limits Limits { get; } = limits;
    }
}
