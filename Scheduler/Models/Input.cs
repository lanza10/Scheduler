using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Interfaces;

namespace Scheduler.Models
{
    public class Input : IInput
    {
        public DateTime CurrentDate { get; set; }

        public Input(DateTime currentDate)
        {
            this.CurrentDate = currentDate;
        }
    }
}
