using FluentAssertions;
using Scheduler.Enums;
using Scheduler.Exceptions;
using Scheduler.Models;
using Scheduler.Services;
namespace SchedulerTests
{
    public class ServiceShouldRaise
    {
        [Fact]
        public void ErrorWhenResultIsNotEnabled()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = false,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };

            //Act
            var action = () => new Service(sc);

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
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Once,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };

            //Act
            var action = () => new Service(sc);

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("This configuration isn't valid, date can´t be null if \"Once\" is selected.");

        }

        [Fact]
        public void ErrorWhenInvalidLimits()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MaxValue,
                EndDate = DateTime.MinValue,
            };

            //Act
            var action = () => new Service(sc);

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("Start date must be earlier than the end date");
        }
        [Fact]
        public void ErrorWhenConfigDateIsEarlierThanCurrent()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = new DateTime(2019, 1, 1),
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };

            //Act
            var action = () => new Service(sc);

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("Configuration date can´t be earlier than the currentDate.");
        }
        [Fact]
        public void ErrorWhenResultDateExceedsStartDate()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = new DateTime(2020, 1, 1),
                Type = ConfigurationType.Once,

                StartDate = new DateTime(2020, 1, 6),
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
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
                CurrentDate = new DateTime(2020, 2, 1),
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = new DateTime(2020, 2, 1),
                Type = ConfigurationType.Once,

                StartDate = DateTime.MinValue,
                EndDate = new DateTime(2020, 1, 2),
            };
            var service = new Service(sc);
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
                IsEnabled = true,
                Occurs = (Occurrence)10,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,
                WeeklyFrequency = 2,


                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var action = () => service.GetOutput();

            //Assert
            action.Should().Throw<KeyNotFoundException>();
        }
        [Fact]
        public void ErrorWhenNoExistingDailyFrequency()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,
                WeeklyFrequency = 2,
                OccursEveryType = (DailyOccursEveryType)10,


                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var action = () => service.GetOutput();

            //Assert
            action.Should().Throw<KeyNotFoundException>();
        }
        [Fact]
        public void ErrorWhenRepeatedDaysOfWeek()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,
                WeeklyFrequency = 2,
                DaysOfWeek = [DayOfWeek.Monday, DayOfWeek.Monday],


                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };

            //Act
            var action = () => new Service(sc);

            //Assert
            action.Should().Throw<SchedulerException>().WithMessage("Days of week should not be repeated.");
        }

        [Fact]
        public void ErrorWhenNoDaysSelectedAndWeeklyConfig()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,
                WeeklyFrequency = 2,
                DaysOfWeek = [],


                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            //Act
            var action = () => new Service(sc);

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("Weekly configuration requires to select at least one day of week.");
        }

        [Fact]
        public void ErrorWhenInvalidWeeklyFrequency()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,
                WeeklyFrequency = 0,


                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            //Act
            var action = () => new Service(sc);

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("Weekly configuration requires a frequency higher than 0.");
        }

        [Fact]
        public void ErrorWhenInvalidDailyFrequency()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,
                DailyOccursEvery = 0,


                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            //Act
            var action = () => new Service(sc);

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("Daily frequency must be higher than 0.");
        }

        [Fact]
        public void ErrorWhenInvalidRangeOfHours()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,
                DailyStartingAt = TimeSpan.MaxValue,
                DailyEndingAt = TimeSpan.MaxValue,


                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            //Act
            var action = () => new Service(sc);

            //Assert
            action.Should().Throw<SchedulerException>()
                .WithMessage("Starting hour must be earlier than the end hour.");
        }
    }
    
}
