using FluentAssertions;
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services;

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
                .Be("Occurs every 4 weeks on monday at 12:30 starting on 1/1/0001");
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
            output.Description.Should().Be("Occurs every 2 weeks on monday at 23:30 starting on 1/1/0001");

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
            output.Description.Should().Be("Occurs every 2 weeks on monday every 90 minutes between 08:00 and 10:00 starting on 1/1/0001");
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
                    "Occurs every 2 weeks on monday, tuesday, wednesday, thursday and friday every 90 minutes between 08:00 and 10:00 starting on 1/1/0001");
        }

        [Fact]
        public void ReturnExpectedOutputsWhenWeeklyFrequencyIsOne()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 1,
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
            var outputList = service.GetOutputList(4);

            //Assert
            outputList[0].NextExecTime.Should().Be(new DateTime(2024, 1, 8, 8, 0, 0));
            outputList[1].NextExecTime.Should().Be(new DateTime(2024, 1, 8, 9, 30, 0));
            outputList[2].NextExecTime.Should().Be(new DateTime(2024, 1, 15, 8, 0, 0));
            outputList[3].NextExecTime.Should().Be(new DateTime(2024, 1, 15, 9, 30, 0));
            outputList[0].Description.Should()
                .Be(
                    "Occurs every week on monday every 90 minutes between 08:00 and 10:00 starting on 1/1/0001");
            outputList[1].Description.Should()
                .Be(
                    "Occurs every week on monday every 90 minutes between 08:00 and 10:00 starting on 1/1/0001");
            outputList[2].Description.Should()
                .Be(
                    "Occurs every week on monday every 90 minutes between 08:00 and 10:00 starting on 1/1/0001");
            outputList[3].Description.Should()
                .Be(
                    "Occurs every week on monday every 90 minutes between 08:00 and 10:00 starting on 1/1/0001");
        }
        [Fact]
        public void ReturnExactlyTheSameDatesAsDailyIfEverydayEachWeekSelected()
        {
            //Arrange
            var scWeek = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 1,
                DaysOfWeek = [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday,
                    DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday],

                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 90,
                OccursEveryType = DailyOccursEveryType.Minutes,

                DailyStartingAt = new TimeSpan(8, 0, 0),
                DailyEndingAt = new TimeSpan(10, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var serviceWeek = new Service(scWeek);

            var scDay = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Daily,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 90,
                OccursEveryType = DailyOccursEveryType.Minutes,

                DailyStartingAt = new TimeSpan(8, 0, 0),
                DailyEndingAt = new TimeSpan(10, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var serviceDay = new Service(scDay);

            //Act
            var outputWeek = serviceWeek.GetOutput();
            var outputWeekList = serviceWeek.GetOutputList(7);
            var outputDay = serviceDay.GetOutput();
            var outputDayList = serviceDay.GetOutputList(7);

            //Assert
            outputWeek.NextExecTime.Should().Be(outputDay.NextExecTime);
            outputWeekList[0].NextExecTime.Should().Be(outputDayList[0].NextExecTime);
            outputWeekList[1].NextExecTime.Should().Be(outputDayList[1].NextExecTime);
            outputWeekList[2].NextExecTime.Should().Be(outputDayList[2].NextExecTime);
            outputWeekList[3].NextExecTime.Should().Be(outputDayList[3].NextExecTime);
            outputWeekList[4].NextExecTime.Should().Be(outputDayList[4].NextExecTime);
            outputWeekList[5].NextExecTime.Should().Be(outputDayList[5].NextExecTime);
            outputWeekList[6].NextExecTime.Should().Be(outputDayList[6].NextExecTime);
        }

        [Fact]
        public void ReturnDaysOfWeekSelected()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 1,
                DaysOfWeek = [DayOfWeek.Monday, DayOfWeek.Thursday],

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
            var outputList = service.GetOutputList(10);

            //Assert
            foreach (var output in outputList)
            {
                output.NextExecTime.DayOfWeek.Should().BeOneOf([DayOfWeek.Monday, DayOfWeek.Thursday]);
            }
        }
        [Fact]
        public void ReturnDatesThatRespectHourRanges()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 1,
                DaysOfWeek = [DayOfWeek.Monday, DayOfWeek.Thursday],

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
            var outputList = service.GetOutputList(10);

            //Assert
            foreach (var output in outputList)
            {
                output.NextExecTime.TimeOfDay.Should().BeGreaterThanOrEqualTo(sc.DailyStartingAt);
                output.NextExecTime.TimeOfDay.Should().BeLessOrEqualTo(sc.DailyEndingAt);
            }
        }

        [Fact]
        public void ReturnExpectedOutputWithToHighFrequency()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 1, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 50,
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
            output.NextExecTime.Should().Be(new DateTime(2024, 12, 16, 8, 0, 0));
            output.Description.Should()
                .Be("Occurs every 50 weeks on monday every 90 minutes between 08:00 and 10:00 starting on 1/1/0001");
        }

        [Fact]
        public void ReturnExpectedDatesWithDifferentDaysOfWeek()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 7, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 5,
                DaysOfWeek = [DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Saturday],

                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 75,
                OccursEveryType = DailyOccursEveryType.Minutes,

                DailyStartingAt = new TimeSpan(11, 0, 0),
                DailyEndingAt = new TimeSpan(15, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(23);

            //Assert
            outputList.Should().HaveCount(23);
            outputList[0].NextExecTime.Should().Be(new DateTime(2024, 7, 1, 12, 0, 0));
            outputList[1].NextExecTime.Should().Be(new DateTime(2024, 7, 1, 13, 15, 0));
            outputList[2].NextExecTime.Should().Be(new DateTime(2024, 7, 1, 14, 30, 0));
            outputList[3].NextExecTime.Should().Be(new DateTime(2024, 7, 3, 11, 0, 0));
            outputList[4].NextExecTime.Should().Be(new DateTime(2024, 7, 3, 12, 15, 0));
            outputList[5].NextExecTime.Should().Be(new DateTime(2024, 7, 3, 13, 30, 0));
            outputList[6].NextExecTime.Should().Be(new DateTime(2024, 7, 3, 14, 45, 0));
            outputList[7].NextExecTime.Should().Be(new DateTime(2024, 7, 6, 11, 0, 0));
            outputList[8].NextExecTime.Should().Be(new DateTime(2024, 7, 6, 12, 15, 0));
            outputList[9].NextExecTime.Should().Be(new DateTime(2024, 7, 6, 13, 30, 0));
            outputList[10].NextExecTime.Should().Be(new DateTime(2024, 7, 6, 14, 45, 0));
            outputList[11].NextExecTime.Should().Be(new DateTime(2024, 8, 5, 11, 0, 0));
            outputList[12].NextExecTime.Should().Be(new DateTime(2024, 8, 5, 12, 15, 0));
            outputList[13].NextExecTime.Should().Be(new DateTime(2024, 8, 5, 13, 30, 0));
            outputList[14].NextExecTime.Should().Be(new DateTime(2024, 8, 5, 14, 45, 0));
            outputList[15].NextExecTime.Should().Be(new DateTime(2024, 8, 7, 11, 0, 0));
            outputList[16].NextExecTime.Should().Be(new DateTime(2024, 8, 7, 12, 15, 0));
            outputList[17].NextExecTime.Should().Be(new DateTime(2024, 8, 7, 13, 30, 0));
            outputList[18].NextExecTime.Should().Be(new DateTime(2024, 8, 7, 14, 45, 0));
            outputList[19].NextExecTime.Should().Be(new DateTime(2024, 8, 10, 11, 0, 0));
            outputList[20].NextExecTime.Should().Be(new DateTime(2024, 8, 10, 12, 15, 0));
            outputList[21].NextExecTime.Should().Be(new DateTime(2024, 8, 10, 13, 30, 0));
            outputList[22].NextExecTime.Should().Be(new DateTime(2024, 8, 10, 14, 45, 0));


        }

        [Fact] 
        public void ReturnExpectedDatesWithDailyModeOnceAndInterval()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 7, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 5,
                DaysOfWeek = [DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Saturday],

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12,0,0),

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            outputList.Should().HaveCount(7);
            outputList[0].NextExecTime.Should().Be(new DateTime(2024, 7, 1, 12, 0, 0));
            outputList[1].NextExecTime.Should().Be(new DateTime(2024, 7, 3, 12, 0, 0));
            outputList[2].NextExecTime.Should().Be(new DateTime(2024, 7, 6, 12, 0, 0));
            outputList[3].NextExecTime.Should().Be(new DateTime(2024, 8, 5, 12, 0, 0));
            outputList[4].NextExecTime.Should().Be(new DateTime(2024, 8, 7, 12, 0, 0));
            outputList[5].NextExecTime.Should().Be(new DateTime(2024, 8, 10, 12, 0, 0));
            outputList[6].NextExecTime.Should().Be(new DateTime(2024, 9, 9, 12, 0, 0));

        }
        [Fact]
        public void ReturnExpectedDatesWithDailyModeOnceAndIntervalUntilEndDate()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2024, 7, 1, 12, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 5,
                DaysOfWeek = [DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Saturday],

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = new DateTime(2024,8,10,11,59,59),
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            outputList.Should().HaveCount(5);
            outputList[0].NextExecTime.Should().Be(new DateTime(2024, 7, 1, 12, 0, 0));
            outputList[1].NextExecTime.Should().Be(new DateTime(2024, 7, 3, 12, 0, 0));
            outputList[2].NextExecTime.Should().Be(new DateTime(2024, 7, 6, 12, 0, 0));
            outputList[3].NextExecTime.Should().Be(new DateTime(2024, 8, 5, 12, 0, 0));
            outputList[4].NextExecTime.Should().Be(new DateTime(2024, 8, 7, 12, 0, 0));

        }

        [Fact]
        public void ReturnExpectedDatesWithAYearOfInterval()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1, 0, 0, 0),
                IsEnabled = true,
                Occurs = Occurrence.Weekly,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                WeeklyFrequency = 53,
                DaysOfWeek = [DayOfWeek.Wednesday],

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(3);

            //Assert
            outputList.Should().HaveCount(3);
            outputList[0].NextExecTime.Should().Be(new DateTime(2020, 1, 1, 12, 0, 0));
            outputList[1].NextExecTime.Should().Be(new DateTime(2021, 1, 6, 12, 0, 0));
            outputList[2].NextExecTime.Should().Be(new DateTime(2022, 1, 12, 12, 0, 0));
        }


    }

}

