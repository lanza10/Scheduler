using Scheduler.Interfaces;
using Scheduler.Enums;

namespace Scheduler.Models
{
    public class Configuration(DateTime? date, bool isEnabled, int days, Occurrence occurs, ConfigurationType type) : IConfiguration
    {
        public DateTime? Date { get; set; } = date;
        public bool IsEnabled { get; set; } = isEnabled;
        public int Days { get; set; } = days;
        public Occurrence Occurs { get; set; } = occurs;
        public ConfigurationType Type { get; set; } = type;
    }
}
