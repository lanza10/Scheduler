using Scheduler.Interfaces;

namespace Scheduler.Models
{
    public class Input(DateTime currentDate) : IInput
    {
        public DateTime CurrentDate { get; } = currentDate; 
    }
}
