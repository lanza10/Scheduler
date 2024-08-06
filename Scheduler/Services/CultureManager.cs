using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Enums;
using Scheduler.Utilities;

namespace Scheduler.Services
{
    public class CultureManager
    {
        public static void ChangeCurrentCulture(Language l)
        {
            CultureInfo.CurrentCulture = LanguageDictionary.GetCulture(l);
        }
    }
}
