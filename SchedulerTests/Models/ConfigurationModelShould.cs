using Xunit;
using Scheduler.Enums;
using Scheduler.Models;
using System.Reflection.Metadata;
using Scheduler.Interfaces;

namespace SchedulerTests.Models;


public class ConfigurationModelShould
{

    [Fact]
    public void InitializeProperly()
    {
        //Arrange
        var expectedDate = DateTime.Now;
        const bool expectedEnabled = true;
        const int expectedDays = 1;
        const Occurrence expectedOccurs = Occurrence.Daily;
        const ConfigurationType expectedType = ConfigurationType.Once;

        //Act
        var config = new Configuration(expectedDate, expectedEnabled, expectedDays, expectedOccurs, expectedType);

        //Assert
        Assert.IsType<Configuration>(config);
        Assert.Equal(expectedDate, config.Date);
        Assert.Equal(expectedEnabled, config.IsEnabled);
        Assert.Equal(expectedDays, config.Days);
        Assert.Equal(expectedOccurs, config.Occurs);
        Assert.Equal(expectedType, config.Type);
    }

    [Theory]
    [InlineData(null, true, 0, Occurrence.Daily, ConfigurationType.Recurring)]
    [InlineData("2020-04-01", true, 0, Occurrence.Daily, ConfigurationType.Recurring)]
    public void SetPropertiesCorrectly(string? date, bool expectedEnabled, int expectedDays, Occurrence expectedOccurs,
        ConfigurationType expectedType)
    {
        //Arrange
        DateTime? expectedDate = string.IsNullOrEmpty(date) ? null : DateTime.Parse(date!);

        //Act
        var config = new Configuration(expectedDate, expectedEnabled, expectedDays, expectedOccurs, expectedType)
        {
            Date = expectedDate,
            IsEnabled = expectedEnabled,
            Days = expectedDays,
            Occurs = expectedOccurs,
            Type = expectedType

        };

        //Assert
        Assert.Equal(expectedDate, config.Date);
        Assert.Equal(expectedEnabled, config.IsEnabled);
        Assert.Equal(expectedDays, config.Days);
        Assert.Equal(expectedOccurs, config.Occurs);
        Assert.Equal(expectedType, config.Type);
    }
}