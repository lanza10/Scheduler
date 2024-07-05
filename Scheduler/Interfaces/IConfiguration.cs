using Scheduler.Enums;

namespace Scheduler.Interfaces
{
    public interface IConfiguration
    {
        DateTime? Date { get; }
        bool IsEnabled { get; }
        int Days { get; }
        Occurrence Occurs { get; }
        ConfigurationType Type { get; }
    }
}
