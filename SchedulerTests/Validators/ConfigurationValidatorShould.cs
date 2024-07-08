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
        [Fact]
        public void ValidateValidConfigurations()
        {
            //Arrange
            var validator = new ConfigurationValidator();
            var onceConfiguration = new Configuration(new DateTime(2020,1,4), true, 1,Occurrence.Daily, ConfigurationType.Once);
            var recurringConfiguration = new Configuration(null, true, 1, Occurrence.Daily, ConfigurationType.Recurring);
            //Act
            var onceIsValid = validator.ValidConfiguration(onceConfiguration);
            var recurringIsValid = validator.ValidConfiguration(recurringConfiguration);
            //Assert
            Assert.True(onceIsValid);
            Assert.True(recurringIsValid);
        }

        [Fact]
        public void InvalidateInvalidConfigurations()
        {
            //Arrange
            var validator = new ConfigurationValidator();
            var onceConfiguration = new Configuration(null, true, 1, Occurrence.Daily, ConfigurationType.Once);
            //Act
            var onceIsValid = validator.ValidConfiguration(onceConfiguration);
            //Assert
            Assert.False(onceIsValid);
        }
    }
}
