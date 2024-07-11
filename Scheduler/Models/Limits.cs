using Scheduler.Exceptions;
using Scheduler.Interfaces;
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
            if (!LimitsValidator.ValidLimits(StartDate, EndDate))
            {
                throw new LimitsException("Start date must be earlier than the end date");
            }
        }

        
    }
}
