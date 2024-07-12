using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Enums;
using Scheduler.Exceptions;
using Scheduler.Models;

namespace Scheduler.Validator
{
    public class SchedulerValidator
    {
        public static void ValidateSchedulerConfiguration(ConfigurationType type, DateTime? configurationDate, int days,
            DateTime startDate, DateTime endDate)
        {
            ValidDateAndType(type, configurationDate);
            ValidDays(days);
            ValidLimits(startDate, endDate);
        }
        public static void ValidDateAndType(ConfigurationType type, DateTime? date)
        {
            if (type == ConfigurationType.Once && !date.HasValue)
            {
                throw new SchedulerException(
                    "This configuration isn't valid, date can´t be null if \"Once\" is selected.");
            }
        }

        public static void ValidDays(int days)
        {
            if (days < 0)
            {
                throw new SchedulerException("This configuration isn't valid, days can´t be lower than 0.");
            }
        }
        public static void ValidLimits(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new SchedulerException("Start date must be earlier than the end date");
            }
        }
    }
}
