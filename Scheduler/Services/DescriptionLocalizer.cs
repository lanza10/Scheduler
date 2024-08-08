using Microsoft.Extensions.Localization;
using System.Globalization;
using Scheduler.Exceptions;

namespace Scheduler.Services
{
    internal class DescriptionLocalizer : IStringLocalizer
    {
        private readonly Dictionary<string, Dictionary<string, string>> _localizations = new()
        {
            ["DailyDesc"] = new Dictionary<string, string>
            {
                ["en-US"] = "Occurs every day {0} {1}",
                ["es-ES"] = "Ocurre cada día {0} {1}",
                ["en-GB"] = "Occurs every day {0} {1}"
            },
            ["EveryDayQuote"] = new Dictionary<string, string>
            {
                ["en-US"] = "every {0} between {1} and {2}",
                ["es-ES"] = "cada {0} entre la/s {1} y la/s {2}",
                ["en-GB"] = "every {0} between {1} and {2}"
            },
            ["MonthlyDateQuote"] = new Dictionary<string, string>
            {
                ["en-US"] = "of every {0} months",
                ["es-ES"] = "de cada {0} meses",
                ["en-GB"] = "of every {0} months"
            },
            ["MonthlyDayQuote"] = new Dictionary<string, string>
            {
                ["en-US"] = "{0} of every {1} months",
                ["es-ES"] = "{0} de cada {1} meses",
                ["en-GB"] = "{0} of every {1} months"
            },
            ["MonthlyDesc"] = new Dictionary<string, string>
            {
                ["en-US"] = "Occurs the {0} {1} {2}",
                ["es-ES"] = "Ocurre el {0} {1} {2}",
                ["en-GB"] = "Occurs the {0} {1} {2}"
            },
            ["OnceDayQuote"] = new Dictionary<string, string>
            {
                ["en-US"] = "at {0}",
                ["es-ES"] = "a la/s {0}",
                ["en-GB"] = "at {0}"
            },
            ["OnceDesc"] = new Dictionary<string, string>
            {
                ["en-US"] = "Occurs once.Schedule will be used on {0} at {1} {2}",
                ["es-ES"] = "Ocurre una vez.Será usado el {0} a la/s {1} {2}",
                ["en-GB"] = "Occurs once.Schedule will be used on {0} at {1} {2}"
            },
            ["StartingOn"] = new Dictionary<string, string>
            {
                ["en-US"] = "starting on {0}",
                ["es-ES"] = "empezando el {0}",
                ["en-GB"] = "starting on {0}"
            },
            ["WeeklyDesc"] = new Dictionary<string, string>
            {
                ["en-US"] = "Occurs every {0} {1} {2}",
                ["es-ES"] = "Ocurre cada {0} {1} {2}",
                ["en-GB"] = "Occurs every {0} {1} {2}"
            },
            ["Day"] = new Dictionary<string, string>
            {
                ["en-US"] = "day",
                ["es-ES"] = "día",
                ["en-GB"] = "day"
            },
            ["First"] = new Dictionary<string, string>
            {
                ["en-US"] = "first",
                ["es-ES"] = "primer",
                ["en-GB"] = "first"
            },
            ["Second"] = new Dictionary<string, string>
            {
                ["en-US"] = "second",
                ["es-ES"] = "segundo",
                ["en-GB"] = "second"
            },
            ["Third"] = new Dictionary<string, string>
            {
                ["en-US"] = "third",
                ["es-ES"] = "tercer",
                ["en-GB"] = "third"
            },
            ["Fourth"] = new Dictionary<string, string>
            {
                ["en-US"] = "fourth",
                ["es-ES"] = "cuarto",
                ["en-GB"] = "fourth"
            },
            ["Last"] = new Dictionary<string, string>
            {
                ["en-US"] = "last",
                ["es-ES"] = "último",
                ["en-GB"] = "last"
            },
            ["Monday"] = new Dictionary<string, string>
            {
                ["en-US"] = "monday",
                ["es-ES"] = "lunes",
                ["en-GB"] = "monday"
            },
            ["Tuesday"] = new Dictionary<string, string>
            {
                ["en-US"] = "tuesday",
                ["es-ES"] = "martes",
                ["en-GB"] = "tuesday"
            },
            ["Wednesday"] = new Dictionary<string, string>
            {
                ["en-US"] = "wednesday",
                ["es-ES"] = "miércoles",
                ["en-GB"] = "wednesday"
            },
            ["Thursday"] = new Dictionary<string, string>
            {
                ["en-US"] = "thursday",
                ["es-ES"] = "jueves",
                ["en-GB"] = "thursday"
            },
            ["Friday"] = new Dictionary<string, string>
            {
                ["en-US"] = "friday",
                ["es-ES"] = "viernes",
                ["en-GB"] = "friday"
            },
            ["Saturday"] = new Dictionary<string, string>
            {
                ["en-US"] = "saturday",
                ["es-ES"] = "sábado",
                ["en-GB"] = "saturday"
            },
            ["Sunday"] = new Dictionary<string, string>
            {
                ["en-US"] = "sunday",
                ["es-ES"] = "domingo",
                ["en-GB"] = "sunday"
            },
            ["Weekday"] = new Dictionary<string, string>
            {
                ["en-US"] = "weekday",
                ["es-ES"] = "día de semana",
                ["en-GB"] = "weekday"
            },
            ["WeekendDay"] = new Dictionary<string, string>
            {
                ["en-US"] = "weekend day",
                ["es-ES"] = "día del fin de semana",
                ["en-GB"] = "weekend day"
            },
            ["Daily"] = new Dictionary<string, string>
            {
                ["en-US"] = "day",
                ["es-ES"] = "día",
                ["en-GB"] = "day"
            },
            ["Weekly"] = new Dictionary<string, string>
            {
                ["en-US"] = "week",
                ["es-ES"] = "semana",
                ["en-GB"] = "week"
            },
            ["Monthly"] = new Dictionary<string, string>
            {
                ["en-US"] = "month",
                ["es-ES"] = "mes",
                ["en-GB"] = "month"
            },
            ["Hours"] = new Dictionary<string, string>
            {
                ["en-US"] = "hour",
                ["es-ES"] = "hora",
                ["en-GB"] = "hour"
            },
            ["Minutes"] = new Dictionary<string, string>
            {
                ["en-US"] = "minute",
                ["es-ES"] = "minuto",
                ["en-GB"] = "minute"
            },
            ["Seconds"] = new Dictionary<string, string>
            {
                ["en-US"] = "second",
                ["es-ES"] = "segundo",
                ["en-GB"] = "second"
            },
            ["And"] = new Dictionary<string, string>
            {
                ["en-US"] = "and",
                ["es-ES"] = "y",
                ["en-GB"] = "and"
            },
            ["Everyday"] = new Dictionary<string, string>
            {
                ["en-US"] = "everyday",
                ["es-ES"] = "todos los días",
                ["en-GB"] = "everyday"
            },
            ["On"] = new Dictionary<string, string>
            {
                ["en-US"] = "on",
                ["es-ES"] = "en",
                ["en-GB"] = "on"
            },
        };

        public LocalizedString this[string name]
        {
            get
            {
                var allStrings = GetAllStrings(false);

                var value = allStrings.FirstOrDefault(ls => ls.Name == name)
                            ?? throw new SchedulerException("This string is not implemented in this culture");

                return new LocalizedString(name, value, resourceNotFound: value == name);
            }
        }
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = this[name];
                var value = string.Format(format.Value, arguments);

                return new LocalizedString(name, value, format.ResourceNotFound);
            }
        }
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var culture = CultureInfo.CurrentCulture.Name;
            var allStrings = new List<LocalizedString>();

            foreach (var kvp in _localizations)
            {
                var value = kvp.Value.TryGetValue(culture, out var value1) ? value1 : kvp.Key;
                allStrings.Add(new LocalizedString(kvp.Key, value, resourceNotFound: value == kvp.Key));
            }

            return allStrings;
        }
    }
}
