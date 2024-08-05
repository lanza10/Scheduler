using Scheduler.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Utilities
{
    public class LanguageDictionary
    {
          public static readonly Dictionary<Language, CultureInfo> LanguageMap = new()
          {
            {
                Language.Us, new CultureInfo("en-US")
            },
            {
                Language.Uk, new CultureInfo("en-GB")
            },
            {
               Language.Es, new CultureInfo("es-ES")
            }
          }; 
        
        public static CultureInfo GetCulture(Language language)
        {
            if (!LanguageMap.TryGetValue(language, out var culture))
            {
                throw new KeyNotFoundException();
            }
            return culture;
        }
    }
}
