using Scheduler.Interfaces;
using Scheduler.Enums;
using Scheduler.Validator;

namespace Scheduler.Models
{
    public class Configuration : IConfiguration
    {
        public DateTime? Date { get; }
        public bool IsEnabled { get; }
        public int Days { get; }
        public Occurrence Occurs { get; }
        public ConfigurationType Type { get; }

        public Configuration(DateTime? date, bool isEnabled, int days, Occurrence occurs, ConfigurationType type)
        { 
            ConfigurationValidator.ValidDateAndType(type, date);
            ConfigurationValidator.ValidDays(days);

            Date = date;
            IsEnabled = isEnabled;
            Days = days;
            Occurs = occurs;
            Type = type;
        }
    }
}
