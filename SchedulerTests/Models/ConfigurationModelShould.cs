using Xunit;
using Scheduler.Enums;
using Scheduler.Models;
using System.Reflection.Metadata;
using FluentAssertions;
using Scheduler.Interfaces;
using Scheduler.Services;
using Scheduler.Exceptions;
using Xunit.Sdk;

namespace SchedulerTests.Models;


public class ConfigurationModelShould
{


    [Theory]
    [InlineData(true, 0, Occurrence.Daily, ConfigurationType.Recurring)]
    [InlineData(false, 10, Occurrence.Daily, ConfigurationType.Recurring)]
    public void SetPropertiesCorrectly(bool expectedEnabled, int expectedDays, Occurrence expectedOccurs,
        ConfigurationType expectedType)
    {
        //Arrange

        //Act
        var config = new Configuration(null, expectedEnabled, expectedDays, expectedOccurs, expectedType);

        //Assert
        Assert.Null(config.Date);
        Assert.Equal(expectedEnabled, config.IsEnabled);
        Assert.Equal(expectedDays, config.Days);
        Assert.Equal(expectedOccurs, config.Occurs);
        Assert.Equal(expectedType, config.Type);
    }

    [Fact]
    public void SetDateCorrectlyWhenDateNotNull()
    {
        //Arrange
        var expectedDate = new DateTime(2020, 1, 1);
        //Act
        var config = new Configuration(
            expectedDate,
            true,
            0,
            Occurrence.Daily,
            ConfigurationType.Recurring
        );
        //Assert
        Assert.Equal(expectedDate, config.Date);
    }

    [Fact]
    public void RaiseErrorWhenInvalidDateAndTypeSelected()
    {
        //Arrange

        //Act
        var act = () => new Configuration(null, true, 0, Occurrence.Daily, ConfigurationType.Once);
        //Assert
        act.Should().Throw<ConfigurationException>()
            .WithMessage("This configuration isn't valid, date can´t be null if \"Once\" is selected.");
    }

    [Fact]
    public void RaiseErrorWhenInvalidDays()
    {
        //Arrange

        //Act
        var act = () => new Configuration(null, true, -1, Occurrence.Daily, ConfigurationType.Recurring);
        //Assert
        act.Should().Throw<ConfigurationException>()
            .WithMessage("This configuration isn't valid, days can´t be lower than 0.");
    }
}