using FluentAssertions;
using Scheduler.Enums;
using Scheduler.Exceptions;
using Scheduler.Models;
using Scheduler.Services;
namespace SchedulerTests
{
    public class SchedulerServiceShouldRaise
    {
        [Fact]
        public void ErrorWhenResultIsNotEnabled()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Days = 2,
                IsEnabled = false,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };

            //Act
            var action = () => new SchedulerService(sc);

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("To create a configuration is mandatory to establish it enabled");
        }
        [Fact]
        public void ErrorWhenNullDateAndOnceConfiguration()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Days = 1,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Once,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };

            //Act
            var action = () => new SchedulerService(sc);

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("This configuration isn't valid, date can´t be null if \"Once\" is selected.");

        }
        [Fact]
        public void ErrorWhenInvalidDaysInOnceConfiguration()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Days = -4,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = new DateTime(2020, 1, 1),
                Type = ConfigurationType.Once,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };

            //Act
            var action = () => new SchedulerService(sc);

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("This configuration isn't valid, days can´t be lower than 0 for the selected configuration type.");
        }
        [Fact]
        public void ErrorWhenInvalidDaysInRecurringConfiguration()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Days = 0,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };

            //Act
            var action = () => new SchedulerService(sc);

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("This configuration isn't valid, days can´t be lower than 1 for the selected configuration type.");
        }

        [Fact]
        public void ErrorWhenInvalidLimits()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Days = 2,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MaxValue,
                EndDate = DateTime.MinValue,
            };

            //Act
            var action = () => new SchedulerService(sc);

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("Start date must be earlier than the end date");
        }

        [Fact]
        public void ErrorWhenResultDateExceedsStartDate()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Days = 2,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = new DateTime(2020, 1, 6),
                EndDate = DateTime.MaxValue,
            };
            var service = new SchedulerService(sc);
            //Act
            var action = () => service.GetOutput();

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("The result date must not be earlier than the specified start date.");
        }

        [Fact]
        public void ErrorWhenResultDateExceedsEndDate()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Days = 2,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = new DateTime(2020, 1, 2),
            };
            var service = new SchedulerService(sc);
            //Act
            var action = () => service.GetOutput();

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("The result date must not be later than the specified end date.");
        }
        [Fact]
        public void ErrorWhenNoExistingOccurrence()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Days = 2,
                IsEnabled = true,
                Occurs = (Occurrence)10,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new SchedulerService(sc);
            //Act
            var action = () => service.GetOutput();

            //Assert
            action.Should().Throw<KeyNotFoundException>();
        }
    }
}
