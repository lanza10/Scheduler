using Scheduler.Validator;

namespace Scheduler.Models
{
    public class Limits
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public Limits(DateTime startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate ?? DateTime.MaxValue;
            LimitsValidator.ValidLimits(StartDate, EndDate);
        }

        
    }
}
