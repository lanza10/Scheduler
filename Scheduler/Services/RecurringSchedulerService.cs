
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Utilities;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public class RecurringDailySchedulerService(SchedulerConfiguration sc) : ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int maxLength)
        {
            List<DateTime> recurringDates;
            if (sc.DailyType == DailyOccursType.Once)
            {
                recurringDates = CalculateAllNextOnceDates(maxLength);
            }
            else
            {
                recurringDates = CalculateAllNextRecurringDates(maxLength);
            }
            return recurringDates;
        }
        public DateTime CalculateNextDate()
        {
            var currentDateOnly = DateOnly.FromDateTime(sc.CurrentDate);
            DateTime nextDate;
            if (sc.DailyType == DailyOccursType.Once)
            {
                nextDate = GetNextDateWhenOnce(currentDateOnly);
            }
            else{
                nextDate = GetNextDateWhenEvery(currentDateOnly);
            }
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(nextDate, sc.StartDate, sc.EndDate);
            return nextDate;
        }

        public string GenerateDescription(DateTime date)
        {
            var frequency = OccurrenceDictionary.GetFrequencyQuote(1, sc.Occurs);
            var formattedNextExecTime = date.ToString("dd/MM/yyyy 'at' HH:mm");
            var formattedStartDate = sc.StartDate.ToString("dd/MM/yyyy");
            return
                $"Occurs every {frequency}." +
                $"Schedule will be used on {formattedNextExecTime} starting on {formattedStartDate}";
        }

        private DateTime GetNextDateWhenOnce(DateOnly currentDateOnly)
        {
            var occursAt = new TimeOnly(sc.DailyOccursOnceAt.Hours, sc.DailyOccursOnceAt.Minutes,
                sc.DailyOccursOnceAt.Seconds);
            return sc.CurrentDate.TimeOfDay > sc.DailyOccursOnceAt
                ? new DateTime(currentDateOnly.AddDays(1), occursAt)
                : new DateTime(currentDateOnly, occursAt);
        }
        private DateTime GetNextDateWhenEvery(DateOnly currentDateOnly)
        {
            var occursAt = new TimeOnly(sc.DailyStartingAt.Hours, sc.DailyStartingAt.Minutes,
                sc.DailyStartingAt.Seconds);
            var isInLimits = sc.CurrentDate.TimeOfDay >= sc.DailyStartingAt && sc.CurrentDate.TimeOfDay <= sc.DailyEndingAt;
            return isInLimits ? sc.CurrentDate : new DateTime(currentDateOnly.AddDays(1), occursAt);
        }

        private TimeSpan GetSpan()
        {
            switch (sc.OccursEveryType)
            {
                case DailyOccursEveryType.Hours:
                    return new TimeSpan(sc.DailyOccursEvery, 0, 0);
                case DailyOccursEveryType.Minutes:
                    return new TimeSpan(0, sc.DailyOccursEvery, 0);
                default:
                    return new TimeSpan(0, 0, sc.DailyOccursEvery);
            }
        }

        private List<DateTime> CalculateAllNextOnceDates(int maxLength)
        {
            var auxDate = CalculateNextDate();
            var recurringDates = new List<DateTime>();
            for (var i = 0; i < maxLength && auxDate <= sc.EndDate; i++)
            {
                recurringDates.Add(auxDate);
                auxDate = auxDate.AddDays(1);
            }
            return recurringDates;
        }

        private List<DateTime> CalculateAllNextRecurringDates(int maxLength)
        {
            var auxDate = CalculateNextDate();
            var everySpan = GetSpan();
            var recurringDates = new List<DateTime>();

            for (var i = 0; i < maxLength && auxDate <= sc.EndDate; i++)
            {
                recurringDates.Add(auxDate);
                auxDate = auxDate.Add(everySpan);

                if (auxDate.TimeOfDay > sc.DailyEndingAt)
                {
                    auxDate = auxDate.Date.AddDays(1)
                        .Add(new TimeSpan(sc.DailyStartingAt.Hours, sc.DailyStartingAt.Minutes, sc.DailyStartingAt.Seconds));
                }
            }

            return recurringDates;
        }
    }
}
