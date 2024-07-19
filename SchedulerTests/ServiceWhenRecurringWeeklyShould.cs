using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace SchedulerTests
{
    public class ServiceWhenRecurringWeeklyShould
    {
        [Fact]
        public void ReturnRecurringWeeklyOutput()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 2),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 4,
                DaysOfWeek = [DayOfWeek.Monday],

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
            output.Description.Should()
                .Be("Occurs every 4 weeks on monday at 12:30 starting on 01/01/0001");
        }

        [Fact]
        public void ReturnTheExpectedDatesOnOneDayAccordingToFrequency()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 4,
                DaysOfWeek = [DayOfWeek.Monday],

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(4);

            //Assert
            outputList.Should().HaveCount(4);
            outputList[0].NextExecTime.Should().Be(new DateTime(2024, 1, 1, 12, 30, 0));
            outputList[1].NextExecTime.Should().Be(new DateTime(2024, 1, 29, 12, 30, 0));
            outputList[2].NextExecTime.Should().Be(new DateTime(2024, 2, 26, 12, 30, 0));
            outputList[3].NextExecTime.Should().Be(new DateTime(2024, 3, 25, 12, 30, 0));
        }

        [Fact]
        public void ReturnTheExpectedDatesOnMultipleDaysAccordingToFrequency()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 4,
                DaysOfWeek = [DayOfWeek.Monday, DayOfWeek.Sunday],

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(8);

            //Assert
            outputList.Should().HaveCount(8);
            outputList[0].NextExecTime.Should().Be(new DateTime(2024, 1, 1, 12, 30, 0));
            outputList[1].NextExecTime.Should().Be(new DateTime(2024, 1, 7, 12, 30, 0));
            outputList[2].NextExecTime.Should().Be(new DateTime(2024, 1, 29, 12, 30, 0));
            outputList[3].NextExecTime.Should().Be(new DateTime(2024, 2, 4, 12, 30, 0));
            outputList[4].NextExecTime.Should().Be(new DateTime(2024, 2, 26, 12, 30, 0));
            outputList[5].NextExecTime.Should().Be(new DateTime(2024, 3, 3, 12, 30, 0));
            outputList[6].NextExecTime.Should().Be(new DateTime(2024, 3, 25, 12, 30, 0));
            outputList[7].NextExecTime.Should().Be(new DateTime(2024, 3, 31, 12, 30, 0));
        }

        [Fact]
        public void ReturnExpectedDatesOnOneWeekDayOnDailyModeEvery()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 2,
                DaysOfWeek = [DayOfWeek.Friday],

                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 90,
                OccursEveryType = DailyOccursEveryType.Minutes,

                DailyStartingAt = new TimeSpan(8, 0, 0),
                DailyEndingAt = new TimeSpan(10, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(6);

            //Assert
            outputList.Should().HaveCount(6);
            outputList[0].NextExecTime.Should().Be(new DateTime(2024, 1, 5, 8, 0, 0));
            outputList[1].NextExecTime.Should().Be(new DateTime(2024, 1, 5, 9, 30, 0));
            outputList[2].NextExecTime.Should().Be(new DateTime(2024, 1, 19, 8, 0, 0));
            outputList[3].NextExecTime.Should().Be(new DateTime(2024, 1, 19, 9, 30, 0));
            outputList[4].NextExecTime.Should().Be(new DateTime(2024, 2, 2, 8, 0, 0));
            outputList[5].NextExecTime.Should().Be(new DateTime(2024, 2, 2, 9, 30, 0));
        }

        [Fact]
        public void ReturnExpectedDatesOnMultipleWeekDaysOnDailyModeEvery()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 2,
                DaysOfWeek = [DayOfWeek.Monday, DayOfWeek.Friday],

                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 90,
                OccursEveryType = DailyOccursEveryType.Minutes,

                DailyStartingAt = new TimeSpan(8, 0, 0),
                DailyEndingAt = new TimeSpan(10, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(8);

            //Assert
            outputList.Should().HaveCount(8);
            outputList[0].NextExecTime.Should().Be(new DateTime(2024, 1, 5, 8, 0, 0));
            outputList[1].NextExecTime.Should().Be(new DateTime(2024, 1, 5, 9, 30, 0));
            outputList[2].NextExecTime.Should().Be(new DateTime(2024, 1, 15, 8, 0, 0));
            outputList[3].NextExecTime.Should().Be(new DateTime(2024, 1, 15, 9, 30, 0));
            outputList[4].NextExecTime.Should().Be(new DateTime(2024, 1, 19, 8, 0, 0));
            outputList[5].NextExecTime.Should().Be(new DateTime(2024, 1, 19, 9, 30, 0));
            outputList[6].NextExecTime.Should().Be(new DateTime(2024, 1, 29, 8, 0, 0));
            outputList[7].NextExecTime.Should().Be(new DateTime(2024, 1, 29, 9, 30, 0));
        }

        [Fact]
        public void ReturnExpectedDescriptionOnDailyModeOnce()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 2,
                DaysOfWeek = [DayOfWeek.Monday],

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(23, 30, 0),

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.Description.Should().Be("Occurs every 2 weeks on monday at 23:30 starting on 01/01/0001");
            
        }

        [Fact]
        public void ReturnExpectedDescriptionOnDailyModeEvery()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 2,
                DaysOfWeek = [DayOfWeek.Monday],

                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 90,
                OccursEveryType = DailyOccursEveryType.Minutes,

                DailyStartingAt = new TimeSpan(8, 0, 0),
                DailyEndingAt = new TimeSpan(10, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.Description.Should().Be("Occurs every 2 weeks on monday every 90 minutes between 08:00 and 10:00 starting on 01/01/0001");
        }

        [Fact]
        public void ReturnLargerDescriptionBecauseOfWeekDays()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 2,
                DaysOfWeek = [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday,
                    DayOfWeek.Friday],

                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 90,
                OccursEveryType = DailyOccursEveryType.Minutes,

                DailyStartingAt = new TimeSpan(8, 0, 0),
                DailyEndingAt = new TimeSpan(10, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.Description.Should()
                .Be(
                    "Occurs every 2 weeks on monday, tuesday, wednesday, thursday and friday every 90 minutes between 08:00 and 10:00 starting on 01/01/0001");
        }
    }
    
}

