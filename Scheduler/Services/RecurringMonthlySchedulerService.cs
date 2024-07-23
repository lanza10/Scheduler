
using System.Runtime.InteropServices.JavaScript;
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services.HoursCalculators;
using Scheduler.Utilities;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public class RecurringMonthlySchedulerService(SchedulerConfiguration sc, IHoursCalculator hc): ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int length)
        {
            return [];
        }

        public DateTime CalculateFirstDate()
        {
            DateTime firstDateOnly;
            if (sc.MonthlyType == MonthlyType.Day)
            {
                firstDateOnly = GetFirstDateDayMode();
            }else 
            {
                firstDateOnly = GetFirstDateDateMode();
            }

            var resultDate = hc.CalculateNextHour(firstDateOnly, sc);
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(resultDate,sc.StartDate,sc.EndDate);
            return resultDate;
        }

        public string GenerateDescription(DateTime date)
        {
            var frequency = OccurrenceDictionaries.GetFrequencyQuote(1, sc.Occurs);
            return "";
        }

        private DateTime GetFirstDateDayMode()
        {
            var currentDate = sc.CurrentDate;
            var monthlyDayDate = new DateTime(currentDate.Year, currentDate.Month, sc.MonthlyDay);
            var limitDate = hc.CalculateNextHour(monthlyDayDate, sc);

            return currentDate <= limitDate ? monthlyDayDate : monthlyDayDate.AddMonths(sc.MonthlyDayFrequency - 1);
        }
        private DateTime GetFirstDateDateMode()
        {
            var currentDate = sc.CurrentDate;
            var test = CalculateDayOfMonth(currentDate);
            test = hc.CalculateNextHour(test, sc);
            return currentDate <= test ? test : CalculateDayOfMonth(currentDate.AddMonths(sc.MonthlyDateFrequency - 1));
        }

        private DateTime CalculateDayOfMonth(DateTime startingDate)
        {
            var test = new DateTime(startingDate.Year, startingDate.Month, 1);
            while ((int)test.DayOfWeek != (int)sc.MonthlyDateDay)
            {
                test = test.AddDays(1);
            }

            var aux =  test.AddDays((int)sc.MonthlyDateOrder * 7);

            if (aux.Month > test.Month)
            {
                aux = aux.AddDays(-7);
            }

            return aux;
        }
    }
}
