using Scheduler.Interfaces;

namespace Scheduler.Models
{
    public class Limits(DateTime startDate, DateTime? endDate) : ILimits
    {
        public DateTime StartDate { get; set; } = startDate;
        public DateTime? EndDate { get; set; } = endDate;
    }
}
