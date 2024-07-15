using Scheduler.Enums;

namespace Scheduler.Models
{
    public class SchedulerConfiguration
    {
        public DateTime CurrentDate { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? ConfigurationDate { get; set; }
        public int Days { get; set; }
        public Occurrence Occurs { get; set; }
        public ConfigurationType Type { get; set; }
        public DateTime StartDate { get; set; }

        private DateTime _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set => _endDate = value ?? DateTime.MaxValue;
        }
    }
}
