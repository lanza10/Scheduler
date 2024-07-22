using Scheduler.Enums;
using Scheduler.Exceptions;
using Scheduler.Models;
using System;

namespace Scheduler.Validator
{
    public class SchedulerValidator
    {
        public static void ValidateSchedulerConfiguration(SchedulerConfiguration sc)
        {
            ValidDateAndType(sc.Type, sc.ConfigurationDate);
            ValidLimits(sc.StartDate, sc.EndDate);
            ValidIsEnabled(sc.IsEnabled);
            ValidCurrentAndConfigurationDates(sc.CurrentDate, sc.ConfigurationDate);
            ValidWeeklyConfiguration(sc.DaysOfWeek, sc.Occurs);
            ValidFrequency(sc.WeeklyFrequency, sc.DailyOccursEvery, sc.Occurs);
            ValidDailyHoursRange(sc.DailyStartingAt, sc.DailyEndingAt);
            ValidDaysOfWeek(sc.DaysOfWeek);
        }

        private static void ValidDateAndType(ConfigurationType type, DateTime? date)
        {
            if (type == ConfigurationType.Once && !date.HasValue)
            {
                throw new SchedulerException(
                    "This configuration isn't valid, date can´t be null if \"Once\" is selected.");
            }
        }

        private static void ValidLimits(DateTime startDate, DateTime? endDate)
        {
            if (endDate < startDate)
            {
                throw new SchedulerException("Start date must be earlier than the end date");
            }
        }

        private static void ValidIsEnabled(bool isEnabled)
        {
            if (!isEnabled)
            {
                throw new SchedulerException("To create a configuration is mandatory to establish it enabled");
            }
        }

        private static void ValidCurrentAndConfigurationDates(DateTime currentDate, DateTime? configurationDate)
        {
            if (configurationDate < currentDate)
            {
                throw new SchedulerException("Configuration date can´t be earlier than the currentDate.");
            }
        }

        private static void ValidWeeklyConfiguration(List<DayOfWeek> daysOfWeek, Occurrence occurs)
        {
            if (occurs == Occurrence.Weekly && daysOfWeek.Count == 0)
            {
                throw new SchedulerException("Weekly configuration requires to select at least one day of week.");
            }
        }

        private static void ValidFrequency(int weeklyFreq, int dailyFreq, Occurrence occurs)
        {
            if (weeklyFreq < 1 && occurs == Occurrence.Weekly)
            {
                throw new SchedulerException("Weekly configuration requires a frequency higher than 0.");
            }

            if (dailyFreq < 1)
            {
                throw new SchedulerException("Daily frequency must be higher than 0.");
            }
        }

        private static void ValidDailyHoursRange(TimeSpan start, TimeSpan end)
        {
            if (start >= end)
            {
                throw new SchedulerException("Starting hour must be earlier than the end hour.");
            }
        }

        private static void ValidDaysOfWeek(List<DayOfWeek> daysOfWeek)
        {

            var uniqueDays = new HashSet<DayOfWeek>();

            foreach (var day in daysOfWeek)
            {
                if (!uniqueDays.Add(day))
                {
                    throw new SchedulerException("Days of week should not be repeated.");
                }
            }
        }
    }
}
