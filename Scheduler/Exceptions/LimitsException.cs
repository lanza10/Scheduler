using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Exceptions
{
    public class LimitsException(string message) : Exception(message);
}
