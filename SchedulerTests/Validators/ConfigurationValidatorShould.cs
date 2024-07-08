using System.Runtime.InteropServices;
using Xunit;
using FluentAssertions;
using FluentAssertions.Common;
using FluentAssertions.Execution;
using Scheduler.Enums;
using Scheduler.Validator;
using Scheduler.Models;
using Configuration = Scheduler.Models.Configuration;

namespace SchedulerTests.Validators
{
    public class ConfigurationValidatorShould
    {
        [Theory]
        [InlineData("2024-07-10", true, 5, Occurrence.Daily, ConfigurationType.Once)]
        [InlineData(null, false, 3, Occurrence.Daily, ConfigurationType.Recurring)]
        public void ValidateValidConfigurations(string? date,bool isEnabled, int days,Occurrence occurs,ConfigurationType type)
        {
            //Arrange
            DateTime? parsedDate = string.IsNullOrEmpty(date) ? null : DateTime.Parse(date);
            var validator = new ConfigurationValidator();
            var configuration = new Configuration(parsedDate, isEnabled, days, occurs, type);

            //Act
            var isValid = validator.ValidConfiguration(configuration);
            //Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("2024-07-10", true, -1, Occurrence.Daily, ConfigurationType.Once)]
        [InlineData(null, false, 3, Occurrence.Daily, ConfigurationType.Once)]
        public void InvalidateInvalidConfigurations(string? date, bool isEnabled, int days, Occurrence occurs, ConfigurationType type)
        {
            //Arrange
            DateTime? parsedDate = string.IsNullOrEmpty(date) ? null : DateTime.Parse(date);
            var validator = new ConfigurationValidator();
            var configuration = new Configuration(parsedDate, isEnabled, days, occurs, type);

            //Act
            var isValid = validator.ValidConfiguration(configuration);
            //Assert
            Assert.False(isValid);
        }
    }
}
