using Xunit;
using Scheduler.Enums;
using Scheduler.Models;
using System.Reflection.Metadata;
using FluentAssertions;
using Scheduler.Services;
using Scheduler.Exceptions;
using Xunit.Sdk;

namespace SchedulerTests.Models;


public class SchedulerConfigurationModelShould
{
    [Fact]
    public void SetPropertiesCorrectly()
    {
        //Arrange
        var date = new DateTime(2020, 1, 1);
        //Act
        var sc = new SchedulerConfiguration{
            CurrentDate = date,
            IsEnabled = true,
            ConfigurationDate = date,
            Days = 5,

            Occurs = Occurrence.Daily,
            Type = ConfigurationType.Recurring,

            StartDate = DateTime.MinValue,
            EndDate = DateTime.MaxValue
        };
        //Assert
        sc.CurrentDate.Should().Be(date);
        sc.IsEnabled.Should().BeTrue();
        sc.ConfigurationDate.Should().Be(date);
        sc.Days.Should().Be( 5 );
        sc.Occurs.Should().Be(Occurrence.Daily);
        sc.StartDate.Should().Be(DateTime.MinValue);
        sc.EndDate.Should().Be(DateTime.MaxValue);


    }
}