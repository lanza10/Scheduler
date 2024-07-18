using FluentAssertions;
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services;

namespace SchedulerTests
{
    public class ServiceWhenRecurringDailyShould
    {
        [Fact]
        public void ReturnRecurringOutput()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Daily,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,
                
                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(12,30,0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2020, 1, 1,12,30,0));
            output.Description.Should()
                .Be("Occurs every day at 12:30 starting on 01/01/0001");
        }

        [Fact]
        public void ReturnTheExpectedNumberOutputsIfEndDateIsTooHigh()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1,12,30,0),
                IsEnabled = true,
                Occurs = Occurrence.Daily,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 40,
                OccursEveryType = DailyOccursEveryType.Minutes,
                DailyOccursOnceAt = new TimeSpan(12, 30, 0),
                DailyStartingAt = new TimeSpan(11, 0, 0),
                DailyEndingAt = new TimeSpan(13,0,0),

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            outputList.Should().HaveCount(7);
            outputList[0].NextExecTime.Should().Be(new DateTime(2020, 1, 1,12,30,0));
            outputList[1].NextExecTime.Should().Be(new DateTime(2020, 1, 2, 11,0,0));
            outputList[2].NextExecTime.Should().Be(new DateTime(2020, 1, 2,11,40,0));
            outputList[3].NextExecTime.Should().Be(new DateTime(2020, 1, 2,12,20,0));
            outputList[4].NextExecTime.Should().Be(new DateTime(2020, 1, 2, 13, 0, 0));
            outputList[5].NextExecTime.Should().Be(new DateTime(2020, 1, 3, 11, 0, 0));
            outputList[6].NextExecTime.Should().Be(new DateTime(2020, 1, 3, 11, 40, 0));
        }

        [Fact]
        public void ReturnOutputsUntilTheEndDate()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1, 12, 30, 0),
                IsEnabled = true,
                Occurs = Occurrence.Daily,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 40,
                OccursEveryType = DailyOccursEveryType.Minutes,
                DailyStartingAt = new TimeSpan(11, 0, 0),
                DailyEndingAt = new TimeSpan(13, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = new DateTime(2020, 1, 2, 12, 30, 0)
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            outputList.Should().HaveCount(4);
            outputList[0].NextExecTime.Should().Be(new DateTime(2020, 1, 1, 12, 30, 0));
            outputList[1].NextExecTime.Should().Be(new DateTime(2020, 1, 2, 11, 0, 0));
            outputList[2].NextExecTime.Should().Be(new DateTime(2020, 1, 2, 11, 40, 0));
            outputList[3].NextExecTime.Should().Be(new DateTime(2020, 1, 2, 12, 20, 0));

        }

        [Fact]
        public void ReturnExpectedDatesWhenOnce()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1, 12, 30, 0),
                IsEnabled = true,
                Occurs = Occurrence.Daily,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(22,30,15),

                StartDate = DateTime.MinValue,
                EndDate = new DateTime(2020, 1, 5, 12, 30, 0)
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(10);

            //Assert
            outputList.Should().HaveCount(4);
            outputList[0].NextExecTime.Should().Be(new DateTime(2020, 1, 1,22,30,15));
            outputList[1].NextExecTime.Should().Be(new DateTime(2020, 1, 2, 22, 30, 15));
            outputList[2].NextExecTime.Should().Be(new DateTime(2020, 1, 3, 22, 30, 15));
            outputList[3].NextExecTime.Should().Be(new DateTime(2020, 1, 4, 22, 30, 15));
        }

        [Fact]
        public void ReturnExpectedDescriptionsWhenOnce()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1, 12, 30, 0),
                IsEnabled = true,
                Occurs = Occurrence.Daily,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                DailyType = DailyOccursType.Once,
                DailyOccursOnceAt = new TimeSpan(22, 30, 15),

                StartDate = DateTime.MinValue,
                EndDate = new DateTime(2020, 1, 5, 12, 30, 0)
            };
            var service = new Service(sc);
            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            outputList.Should().HaveCount(4);
            outputList[0].Description.Should().Be("Occurs every day at 22:30 starting on 01/01/0001");
            outputList[1].Description.Should().Be("Occurs every day at 22:30 starting on 01/01/0001");
            outputList[2].Description.Should().Be("Occurs every day at 22:30 starting on 01/01/0001");
            outputList[3].Description.Should().Be("Occurs every day at 22:30 starting on 01/01/0001");
        }
        [Fact]
        public void ReturnExpectedDescriptionsWhenEvery()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1, 12, 30, 0),
                IsEnabled = true,
                Occurs = Occurrence.Daily,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                DailyType = DailyOccursType.Every,
                DailyOccursEvery = 40,
                OccursEveryType = DailyOccursEveryType.Minutes,
                DailyStartingAt = new TimeSpan(11, 0, 0),
                DailyEndingAt = new TimeSpan(13, 0, 0),

                StartDate = DateTime.MinValue,
                EndDate = new DateTime(2020, 1, 2, 12, 30, 0)
            };
            var service = new Service(sc);
            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            outputList.Should().HaveCount(4);
            outputList[0].Description.Should().Be("Occurs every day, every 40 minutes between 11:00 and 13:00 starting on 01/01/0001");
            outputList[1].Description.Should().Be("Occurs every day, every 40 minutes between 11:00 and 13:00 starting on 01/01/0001");
            outputList[2].Description.Should().Be("Occurs every day, every 40 minutes between 11:00 and 13:00 starting on 01/01/0001");
            outputList[3].Description.Should().Be("Occurs every day, every 40 minutes between 11:00 and 13:00 starting on 01/01/0001");
        }
        [Theory]
        [InlineData("Occurs every day, every 30 minutes between 11:00 and 13:00 starting on 01/01/0001", DailyOccursType.Every)]
        [InlineData("Occurs every day at 12:00 starting on 01/01/0001", DailyOccursType.Once)]
        public void ReturnDifferentDescriptionsDependingOnType(string expectedDescription, DailyOccursType occursEveryType)
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1, 12, 30, 0),
                IsEnabled = true,
                Occurs = Occurrence.Daily,
                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                DailyType = occursEveryType,
                DailyOccursEvery = 30,
                OccursEveryType = DailyOccursEveryType.Minutes,
                DailyStartingAt = new TimeSpan(11, 0, 0),
                DailyEndingAt = new TimeSpan(13, 0, 0),

                DailyOccursOnceAt = new TimeSpan(12,0,0),
                

                StartDate = DateTime.MinValue,
                EndDate = new DateTime(2020, 1, 2, 12, 30, 0)

            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.Description.Should().Be(expectedDescription);
        }

    }
}
