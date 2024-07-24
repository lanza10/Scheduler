using Scheduler.Enums;

namespace Scheduler.Models
{
    public class SchedulerConfiguration
    {
        public DateTime CurrentDate { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? ConfigurationDate { get; set; }
        public Occurrence Occurs { get; set; }
        public ConfigurationType Type { get; set; }

        public MonthlyType MonthlyType { get; set; } = MonthlyType.Day;

        public int MonthlyDay { get; set; } = 1;
        public int MonthlyDayFrequency { get; set; } = 1;

        public MonthlyDateOrder MonthlyDateOrder { get; set; } = MonthlyDateOrder.First;
        public MonthlyDateDay MonthlyDateDay { get; set; } = MonthlyDateDay.Day;
        public int MonthlyDateFrequency { get; set; } = 1;
        

        public int WeeklyFrequency { get; set; } = 1;

        public List<DayOfWeek> DaysOfWeek { get; set; } =
        [   DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
            DayOfWeek.Thursday, DayOfWeek.Friday ,DayOfWeek.Saturday, 
            DayOfWeek.Sunday,
        ];

        public DailyOccursType DailyType { get; set; } = DailyOccursType.Every;
        public TimeSpan DailyOccursOnceAt { get; set; } = TimeSpan.Zero;
        public int DailyOccursEvery { get; set; } = 1;
        public DailyOccursEveryType OccursEveryType { get; set; } = DailyOccursEveryType.Hours;
        public TimeSpan DailyStartingAt { get; set; } = new (0, 0, 0);
        public TimeSpan DailyEndingAt { get; set; } = new (23, 59, 59);

        public DateTime StartDate { get; set; }
        private DateTime _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set => _endDate = value ?? DateTime.MaxValue;
        }
    }
}
