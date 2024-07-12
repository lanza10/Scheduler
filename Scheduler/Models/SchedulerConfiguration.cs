using Scheduler.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Validator;

namespace Scheduler.Models
{
    public class SchedulerConfiguration
    {
        public DateTime CurrentDate { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? ConfigurationDate { get; set; }
        public int Days { get; set; }
        public Occurrence Occurs { get; set; }
        public ConfigurationType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //public SchedulerConfiguration(DateTime currentDate, bool isEnabled, DateTime? configurationDate, int days, Occurrence occurs, ConfigurationType type, DateTime startDate, DateTime? endDate)
        //{
        //    EndDate = endDate ?? DateTime.MaxValue;
        //    SchedulerValidator.ValidateSchedulerConfiguration(type,configurationDate,days,startDate,EndDate);
        //    CurrentDate = currentDate;
        //    IsEnabled = isEnabled;
        //    ConfigurationDate = configurationDate;
        //    Days = days;
        //    Occurs = occurs;
        //    Type = type;
        //    StartDate = startDate;
        //}
    }
}
