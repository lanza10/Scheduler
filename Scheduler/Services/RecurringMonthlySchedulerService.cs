
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services.HoursCalculators;
using Scheduler.Utilities;
using Scheduler.Validator;

namespace Scheduler.Services
{
    public class RecurringMonthlySchedulerService(SchedulerConfiguration sc, IHoursCalculator hc): ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int maxLength)
        {
            var currentDate = CalculateFirstDate();
            var initDate = currentDate;
            var initTime = currentDate.TimeOfDay;
            var daysOfWeek = GetDayOfWeekList(sc.MonthlyDateDay);
            var datesList = new List<DateTime>();
            while (datesList.Count < maxLength && currentDate <= sc.EndDate)
            {
                var i = daysOfWeek.FindIndex(day => day == currentDate.DayOfWeek);
                while (i < daysOfWeek.Count && datesList.Count < maxLength && currentDate <= sc.EndDate)
                {
                    var month = currentDate.Month;
                    if (currentDate.DayOfWeek != daysOfWeek[i])
                    {
                        break;
                    }

                    datesList.Add(currentDate.Date);
                    currentDate = currentDate.AddDays(1);
                    i++;

                    if (month != currentDate.Month)
                    {
                        break;
                    }
                }

                currentDate = CalculateDayOfMonth(new DateTime(currentDate.Year,currentDate.Month,1).AddMonths(sc.MonthlyDateFrequency -1));


            }

            return hc.GetHoursOfDates(datesList, sc, maxLength, initTime);
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
            var endLimit = hc.CalculateEndLimit(monthlyDayDate, sc);
            var startLimit = hc.CalculateStartLimit(monthlyDayDate, sc);

            if (currentDate <= endLimit)
            {
                return currentDate >= startLimit ? currentDate : monthlyDayDate;
            }
            return monthlyDayDate.AddMonths(sc.MonthlyDayFrequency - 1);
        }
        private DateTime GetFirstDateDateMode()
        {
            var currentDate = sc.CurrentDate;
            var monthlyDayDate = CalculateDayOfMonth(currentDate);
            var endLimit = hc.CalculateEndLimit(monthlyDayDate, sc);
            var startLimit = hc.CalculateStartLimit(monthlyDayDate, sc);

            if (currentDate <= endLimit)
            {
                return currentDate >= startLimit ? currentDate : monthlyDayDate;
            }
            return CalculateDayOfMonth(currentDate.AddMonths(sc.MonthlyDateFrequency - 1));
        }

        private DateTime CalculateDayOfMonth(DateTime startingDate)
        {
            var test = new DateTime(startingDate.Year, startingDate.Month, 1);
            var days = GetDayOfWeekList(sc.MonthlyDateDay);
            if (days.Count == 7)
            {
                return CalculateWhenSearchingDay(test);
            }
            while (days.First() != test.DayOfWeek)
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

        private DateTime CalculateWhenSearchingDay(DateTime date)
        {
            if (sc.MonthlyDateOrder == MonthlyDateOrder.Last)
            {
                date = date.AddDays(27);
                while (date.AddDays(1).Month == date.Month)
                {
                    date = date.AddDays(1);
                }

                return date;
            }

            return date.AddDays((int)sc.MonthlyDateOrder);
        }

        private List<DayOfWeek> GetDayOfWeekList(MonthlyDateDay key)
        {
            if (!MonthlyDictionaries.WeekDaysMap.TryGetValue(key, out var res))
            {
                throw new KeyNotFoundException();
            }
            return res;
        }
    }
}
