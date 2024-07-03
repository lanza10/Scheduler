using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Models
{
    public class Configuration
    {
        public enum Occurrence
        {
            DAILY,
            WEEKLY,
            MONTHLY
        }
        public enum ConfigurationType
        {
            ONCE,
            RECURRING
        }
        public DateTime Date{ get; set; }
        public bool IsEnabled {  get; set; }
        public int Days {  get; set; }
        public Occurrence Occurs { get; set; }
        public ConfigurationType Type { get; set; }

        public Configuration(DateTime date, bool isEnabled, int days, Occurrence occurs, ConfigurationType type)
        {
            this.Date = date;
            this.IsEnabled = isEnabled;
            this.Days = days;
            this.Occurs = occurs;
            this.Type = type;
        }

    }
}
