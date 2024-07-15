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
        public static void ValidateSchedulerConfiguration(SchedulerConfiguration sc)
        {
            ValidDateAndType(sc.Type, sc.ConfigurationDate);
            ValidDays(sc.Days, sc.Type);
            ValidLimits(sc.StartDate, sc.EndDate);
            ValidIsEnabled(sc.IsEnabled);
        }
        public static void ValidDateAndType(ConfigurationType type, DateTime? date)
        {
            if (type == ConfigurationType.Once && !date.HasValue)
            {
                throw new SchedulerException(
                    "This configuration isn't valid, date can´t be null if \"Once\" is selected.");
            }
        }

        public static void ValidDays(int days, ConfigurationType type)
        {
            var minDays = type == ConfigurationType.Once ? 0 : 1;
            if (days < minDays)
            {
                throw new SchedulerException($"This configuration isn't valid, days can´t be lower than {minDays} for the selected configuration type.");
            }
        }
        public static void ValidLimits(DateTime startDate, DateTime? endDate)
        {
            if (endDate < startDate)
            {
                throw new SchedulerException("Start date must be earlier than the end date");
            }
        }

        public static void ValidIsEnabled(bool isEnabled)
        {
            if (!isEnabled)
            {
                throw new SchedulerException("To create a configuration is mandatory to establish it enabled");
            }
        }

    }
}
