using Microsoft.Extensions.Localization;
using System.Globalization;
namespace Scheduler.Services
{
    internal class LocalizationManager : IStringLocalizer
    {
        private readonly Dictionary<string, Dictionary<string, string>> _localizations = new()
        {
            ["DailyDesc"] = new Dictionary<string, string>
            {
                ["en-US"] = "Occurs every day {0} {1}",
                ["es-ES"] = "Ocurre cada día {0} {1}",
                ["en-GB"] = "Occurs every day {0} {1}"
            },
        };

        public LocalizedString this[string name]
        {
            get
            {
                var culture = CultureInfo.CurrentCulture.Name;
                var value = _localizations.ContainsKey(name) && _localizations[name].ContainsKey(culture)
                    ? _localizations[name][culture]
                    : name;

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
