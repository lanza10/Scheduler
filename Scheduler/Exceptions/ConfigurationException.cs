using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Exceptions
{
    public class ConfigurationException(string message) : Exception(message);
}
