using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services.HoursCalculators;
using Scheduler.Utilities;
using Scheduler.Validator;
using System;

namespace Scheduler.Services
{
    public class RecurringMonthlySchedulerService(SchedulerConfiguration sc, IHoursCalculator hc): ISchedulerService
    {
        public List<DateTime> CalculateAllNextDates(int maxLength)
        {
            var currentDate = CalculateFirstDate();
            var initTime = currentDate.TimeOfDay;
            var datesList = new List<DateTime>();
            if (sc.MonthlyType == MonthlyType.Date)
            {
                datesList = GetAllDatesWhenDateMode(datesList, maxLength, currentDate);
            }
            else
            {
                datesList = GetAllDatesWhenDayMode(datesList, maxLength, currentDate);
            }

            return hc.GetHoursOfDates(datesList, sc, maxLength, initTime);
        }


        public DateTime CalculateFirstDate()
        {
            var firstDateOnly = sc.MonthlyType == MonthlyType.Day ? GetFirstDateDayMode() : GetFirstDateDateMode();

            var resultDate = hc.CalculateNextHour(firstDateOnly, sc);
            SchedulerServiceValidator.ValidateResultDoNotExceedLimits(resultDate,sc.StartDate,sc.EndDate);
            return resultDate;
        }


        public string GenerateDescription(DateTime date)
        {
            return DescriptionCalculator.GetMonthlyDescription(sc);
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

            return CalculateDayOfMonth(currentDate.AddMonths(sc.MonthlyDateFrequency));
        }

        private DateTime CalculateDayOfMonth(DateTime startingDate)
        {
            var currentDate = new DateTime(startingDate.Year, startingDate.Month, 1);
            var days = GetDayOfWeekList(sc.MonthlyDateDay);

            if (days.Count == 7)
            {
                return CalculateWhenSearchingDateDay(currentDate);
            }

            while (!days.Contains(currentDate.DayOfWeek))
            {
                    currentDate = currentDate.AddDays(1);
            }

            var auxDate =  currentDate.AddDays((int)sc.MonthlyDateOrder * 7);

            if (auxDate.Month > currentDate.Month)
            {
                auxDate = auxDate.AddDays(-7);
            }

            return auxDate;
        }

        private DateTime CalculateWhenSearchingDateDay(DateTime date)
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

        

        private List<DateTime> GetAllDatesWhenDateMode(List<DateTime> datesList, int maxLength, DateTime currentDate)
        {
            if (sc.MonthlyDateDay == MonthlyDateDay.Day)
            {
                return GetAllDatesWhenSearchingDayOnDate(datesList, maxLength, currentDate);
            }

            return GetAllDatesWhenSearchingOtherDate(datesList, maxLength, currentDate);
        }

        private List<DateTime> GetAllDatesWhenSearchingDayOnDate(List<DateTime> datesList, int maxLength,
            DateTime currentDate)
        {
            while (datesList.Count < maxLength && currentDate <= sc.EndDate)
            {
                datesList.Add(currentDate.Date);
                currentDate = new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(sc.MonthlyDateFrequency);
                currentDate = CalculateWhenSearchingDateDay(currentDate);
            }
            return datesList;
        }
        private List<DateTime> GetAllDatesWhenSearchingOtherDate(List<DateTime> datesList, int maxLength,
            DateTime currentDate)
        {
            var daysOfWeek = GetDayOfWeekList(sc.MonthlyDateDay);
            while (datesList.Count < maxLength && currentDate <= sc.EndDate)
            {
                var i = daysOfWeek.FindIndex(day => day == currentDate.DayOfWeek);
                while (i < daysOfWeek.Count && datesList.Count < maxLength && currentDate <= sc.EndDate)
                {
                    var month = currentDate.Month;

                    datesList.Add(currentDate.Date);
                    currentDate = currentDate.AddDays(1);
                    i++;

                    if (month != currentDate.Month)
                    {
                        currentDate = currentDate.AddDays(-1);
                        break;
                    }
                }

                currentDate = CalculateDayOfMonth(new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(sc.MonthlyDateFrequency));

            }

            return datesList;
        }
        private List<DateTime> GetAllDatesWhenDayMode(List<DateTime> datesList, int maxLength, DateTime currentDate)
        {
            while (datesList.Count < maxLength && currentDate <= sc.EndDate)
            {
                datesList.Add(currentDate.Date);
                currentDate = currentDate.AddMonths(sc.MonthlyDayFrequency);
            }

            return datesList;
        }

        private static List<DayOfWeek> GetDayOfWeekList(MonthlyDateDay key)
        {
            if (!MonthlyDictionaries.WeekDaysMap.TryGetValue(key, out var res))
            {
                throw new KeyNotFoundException();
            }
            return res;
        }
    }
}
