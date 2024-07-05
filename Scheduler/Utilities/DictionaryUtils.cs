using Scheduler.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Utilities
{
    public static class DictionaryUtils
    {
        public static readonly Dictionary<Occurrence, string> OcurrenceMap = new()
        {
            {Occurrence.Daily, "day" },
        };

        // Método para obtener un valor del diccionario
        public static string GetValue(Occurrence key)
        {
            if (OcurrenceMap.TryGetValue(key, out string value))
            {
                return value;
            }
            throw new KeyNotFoundException($"La clave '{key}' no existe en el diccionario.");
        }
    }
}
