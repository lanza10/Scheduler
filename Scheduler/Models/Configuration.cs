using Scheduler.Interfaces;
using Scheduler.Enums;
using Scheduler.Exceptions;
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
            if (!ConfigurationValidator.ValidDateAndType(type, date))
            {
                throw new ConfigurationException(
                    "This configuration isn't valid, date can´t be null if \"Once\" is selected.");
            }

            if (!ConfigurationValidator.ValidDays(days))
            {
                throw new ConfigurationException("This configuration isn't valid, days can´t be lower than 0.");
            }

            Date = date;
            IsEnabled = isEnabled;
            Days = days;
            Occurs = occurs;
            Type = type;
        }
    }
}
