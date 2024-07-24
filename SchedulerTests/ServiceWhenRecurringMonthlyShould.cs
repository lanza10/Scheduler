﻿
using FluentAssertions;
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services;

namespace SchedulerTests
{
    public class ServiceWhenRecurringMonthlyShould
    {
        [Fact]
        public void ReturnExpectedDateWhenDayModeAndCurrentIsLessThanFirst()
        {
            //Arrange
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Day,
                MonthlyDay = 8,
                MonthlyDayFrequency = 3,

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, 8, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenDayModeAndCurrentIsEqualToFirst()
        {
            //Arrange
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 8,12,30,0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Day,
                MonthlyDay = 8,
                MonthlyDayFrequency = 3,

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, 8, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenDayModeAndCurrentIsMoreThanFirst()
        {
            //Arrange
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 9),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Day,
                MonthlyDay = 8,
                MonthlyDayFrequency = 3,

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 3, 8, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenDayModeAndCurrentIsMoreThanFirstByTime()
        {
            //Arrange
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 8, 14,0,0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Day,
                MonthlyDay = 8,
                MonthlyDayFrequency = 3,

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 3, 8, 12, 30, 0));

        }

        [Fact]
        public void ReturnExpectedDateWhenDateModeAndCurrentIsLessThanFirst()
        {
            //Arrange
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.First,
                MonthlyDateDay = MonthlyDateDay.Thursday,
                MonthlyDateFrequency = 3,
                

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, 4, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenDateModeAndCurrentIsEqualToFirst()
        {
            //Arrange
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 4, 12, 30, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.First,
                MonthlyDateDay = MonthlyDateDay.Thursday,
                MonthlyDateFrequency = 3,


                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, 4, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenDateModeAndCurrentIsMoreThanFirst()
        {
            //Arrange
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 5, 10, 30, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.First,
                MonthlyDateDay = MonthlyDateDay.Thursday,
                MonthlyDateFrequency = 3,


                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 3, 7, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenDateModeAndCurrentIsMoreThanFirstByTime()
        {
            //Arrange
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 4, 14, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.First,
                MonthlyDateDay = MonthlyDateDay.Thursday,
                MonthlyDateFrequency = 3,


                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 3, 7, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenModeDayAndDailyModeEvery()
        {
            //Arrange
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 14, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Day,
                MonthlyDay = 8,
                MonthlyDayFrequency = 3,


                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 2,
                OccursEveryType = DailyOccursEveryType.Hours,
                DailyStartingAt = new TimeSpan(12,20,40),
                DailyEndingAt = new TimeSpan(14,0,0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, 8, 12, 20, 40));

        }
        [Fact]
        public void ReturnExpectedDateWhenModeDateAndDailyModeEvery()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 14, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.First,
                MonthlyDateDay = MonthlyDateDay.Thursday,
                MonthlyDateFrequency = 3,


                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 2,
                OccursEveryType = DailyOccursEveryType.Hours,
                DailyStartingAt = new TimeSpan(12, 20, 40),
                DailyEndingAt = new TimeSpan(14, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, 4, 12, 20, 40));

        }
        [Fact]
        public void ReturnExpectedDateWhenDateModeAndCurrentIsLessThanLast()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 14, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.Last,
                MonthlyDateDay = MonthlyDateDay.Monday,
                MonthlyDateFrequency = 3,



                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, 29, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenDateModeAndCurrentIsEqualToLast()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 29, 12, 30, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.Last,
                MonthlyDateDay = MonthlyDateDay.Monday,
                MonthlyDateFrequency = 3,



                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, 29, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenDateModeAndCurrentIsMoreThanLast()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 30, 12, 30, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.Last,
                MonthlyDateDay = MonthlyDateDay.Monday,
                MonthlyDateFrequency = 3,



                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 3, 25, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenDateModeAndCurrentIsMoreThanLastByTime()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 29, 12, 30, 1),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.Last,
                MonthlyDateDay = MonthlyDateDay.Monday,
                MonthlyDateFrequency = 3,



                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 3, 25, 12, 30, 0));

        }

        [Fact]
        public void ReturnExpectedDateOnDayModeDailyModeEveryAndCurrentDateBetweenHourLimits()
        {
            //Arrange
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 8, 12, 45, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Day,
                MonthlyDay = 8,
                MonthlyDayFrequency = 3,


                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 2,
                OccursEveryType = DailyOccursEveryType.Hours,
                DailyStartingAt = new TimeSpan(12, 20, 40),
                DailyEndingAt = new TimeSpan(14, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, 8, 12, 45, 0));

        }
        [Fact]
        public void ReturnExpectedDateOnDateModeDailyModeEveryAndCurrentDateBetweenHourLimits()
        {
            //Arrange
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 8, 12, 45, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateFrequency = 3,
                MonthlyDateDay = MonthlyDateDay.Weekday,
                MonthlyDateOrder = MonthlyDateOrder.Second,


                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 2,
                OccursEveryType = DailyOccursEveryType.Hours,
                DailyStartingAt = new TimeSpan(12, 20, 40),
                DailyEndingAt = new TimeSpan(14, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, 8, 12, 45, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenSearchingWeekendDay()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 15, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.Last,
                MonthlyDateDay = MonthlyDateDay.WeekendDay,
                MonthlyDateFrequency = 3,

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, 27, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenSearchingWeekendDayButCurrentIsLater()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 27, 15, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.Last,
                MonthlyDateDay = MonthlyDateDay.WeekendDay,
                MonthlyDateFrequency = 3,

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 3, 30, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenSearchingWeekDay()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 15, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.Last,
                MonthlyDateDay = MonthlyDateDay.Weekday,
                MonthlyDateFrequency = 3,

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, 29, 12, 30, 0));

        }
        [Fact]
        public void ReturnExpectedDateWhenSearchingWeekDayButCurrentIsLater()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 15, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = MonthlyDateOrder.First,
                MonthlyDateDay = MonthlyDateDay.Weekday,
                MonthlyDateFrequency = 3,

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 3, 4, 12, 30, 0));

        }

        [Theory]
        [InlineData(MonthlyDateOrder.First, 1)]
        [InlineData(MonthlyDateOrder.Second, 2)]
        [InlineData(MonthlyDateOrder.Third, 3)]
        [InlineData(MonthlyDateOrder.Fourth, 4)]
        [InlineData(MonthlyDateOrder.Last, 31)]

        public void ReturnExpectedDatesDependingOnOrderRequested(MonthlyDateOrder order, int day)
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 0, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = order,
                MonthlyDateDay = MonthlyDateDay.Day,
                MonthlyDateFrequency = 3,

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 1, day, 12, 30, 0));
        }
        [Theory]
        [InlineData(MonthlyDateOrder.First, 1)]
        [InlineData(MonthlyDateOrder.Second, 2)]
        [InlineData(MonthlyDateOrder.Third, 3)]
        [InlineData(MonthlyDateOrder.Fourth, 4)]
        [InlineData(MonthlyDateOrder.Last, 31)]

        public void ReturnExpectedDatesDependingOnOrderRequestedButCurrentIsLate(MonthlyDateOrder order, int day)
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, day, 23, 59, 0),
                IsEnabled = true,
                Occurs = Occurrence.Monthly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                MonthlyType = MonthlyType.Date,
                MonthlyDateOrder = order,
                MonthlyDateDay = MonthlyDateDay.Day,
                MonthlyDateFrequency = 3,

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2024, 3, day, 12, 30, 0));
        }

    }
}
