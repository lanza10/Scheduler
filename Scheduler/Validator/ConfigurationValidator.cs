using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Enums;
using Scheduler.Models;

namespace Scheduler.Validator
{
    public class ConfigurationValidator
    {
        public static bool ValidDateAndType(ConfigurationType type, DateTime? date)
        {
            return type != ConfigurationType.Once || date.HasValue;
        }

        public static bool ValidDays(int days)
        {
            return days >= 0;
        }
    }
}
