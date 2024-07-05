using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Enums;
using Scheduler.Models;

namespace Scheduler.Validator
{
    public class ConfigurationValidator
    {
        public bool ValidConfiguration(Configuration configuration)
        {
            if (configuration.Type == ConfigurationType.Once)
            {
                return configuration.Date!=null;
            }

            return true;
        }
    }
}
