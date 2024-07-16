using FluentAssertions;
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services;

namespace SchedulerTests
{
    public class ServiceWhenRecurringShould
    {
        [Fact]
        public void ReturnRecurringOutput()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Days = 1,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2020, 1, 2));
            output.Description.Should()
                .Be("Occurs every day.Schedule will be used on 02/01/2020 at 00:00 starting on 01/01/0001");
        }
    
        [Fact]
        public void ReturnTheExpectedNumberOutputsIfEndDateIsTooHigh()
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
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            outputList.Should().HaveCount(7);
            outputList[0].NextExecTime.Should().Be(new DateTime(2020, 1, 3));
            outputList[1].NextExecTime.Should().Be(new DateTime(2020, 1, 5));
            outputList[2].NextExecTime.Should().Be(new DateTime(2020, 1, 7));
            outputList[3].NextExecTime.Should().Be(new DateTime(2020, 1, 9));
            outputList[4].NextExecTime.Should().Be(new DateTime(2020, 1, 11));
            outputList[5].NextExecTime.Should().Be(new DateTime(2020, 1, 13));
            outputList[6].NextExecTime.Should().Be(new DateTime(2020, 1, 15));
        }
        [Fact]
        public void ReturnOutputsUntilTheEndDate()
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
                EndDate = new DateTime(2020, 1, 8),
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            outputList.Should().HaveCount(3);
            outputList[0].NextExecTime.Should().Be(new DateTime(2020, 1, 3));
            outputList[1].NextExecTime.Should().Be(new DateTime(2020, 1, 5));
            outputList[2].NextExecTime.Should().Be(new DateTime(2020, 1, 7));
        }

        [Fact]
        public void ReturnExpectedDates()
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
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            outputList[0].NextExecTime.Should().Be(new DateTime(2020, 1, 3));
            outputList[1].NextExecTime.Should().Be(new DateTime(2020, 1, 5));
            outputList[2].NextExecTime.Should().Be(new DateTime(2020, 1, 7));
            outputList[3].NextExecTime.Should().Be(new DateTime(2020, 1, 9));
            outputList[4].NextExecTime.Should().Be(new DateTime(2020, 1, 11));
            outputList[5].NextExecTime.Should().Be(new DateTime(2020, 1, 13));
            outputList[6].NextExecTime.Should().Be(new DateTime(2020, 1, 15));
        }

        [Fact]
        public void ReturnExpectedDescriptions()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Days = 1,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            outputList[0].Description.Should().Be("Occurs every day.Schedule will be used on 02/01/2020 at 00:00 starting on 01/01/0001");
            outputList[1].Description.Should().Be("Occurs every day.Schedule will be used on 03/01/2020 at 00:00 starting on 01/01/0001");
            outputList[2].Description.Should().Be("Occurs every day.Schedule will be used on 04/01/2020 at 00:00 starting on 01/01/0001");
            outputList[3].Description.Should().Be("Occurs every day.Schedule will be used on 05/01/2020 at 00:00 starting on 01/01/0001");
            outputList[4].Description.Should().Be("Occurs every day.Schedule will be used on 06/01/2020 at 00:00 starting on 01/01/0001");
            outputList[5].Description.Should().Be("Occurs every day.Schedule will be used on 07/01/2020 at 00:00 starting on 01/01/0001");
            outputList[6].Description.Should().Be("Occurs every day.Schedule will be used on 08/01/2020 at 00:00 starting on 01/01/0001");
        }

        [Theory]
        [InlineData(1, "Occurs every day.Schedule will be used on 02/01/2020 at 00:00 starting on 01/01/0001")]
        [InlineData(2, "Occurs every 2 days.Schedule will be used on 03/01/2020 at 00:00 starting on 01/01/0001")]
        [InlineData(4, "Occurs every 4 days.Schedule will be used on 05/01/2020 at 00:00 starting on 01/01/0001")]
        [InlineData(5, "Occurs every 5 days.Schedule will be used on 06/01/2020 at 00:00 starting on 01/01/0001")]
        public void ReturnDifferentDescriptionsDependingOnDays(int days, string expectedDescription)
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Days = days,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            //Act
            var output = service.GetOutput();

            //Assert
            output.Description.Should().Be(expectedDescription);
        }
        [Fact]
        public void ChangeMonthIfExceedsActualMonth()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 31),
                Days = 2,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Month.Should().Be(2);
        }

        [Fact]
        public void ChangeYearIfExceedsActualYear()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 12, 31),
                Days = 2,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Year.Should().Be(2021);
            output.NextExecTime.Month.Should().Be(1);
        }

        [Fact]
        public void ReturnMaxDatesWhenCoincideWithLimits()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                Days = 1,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = new DateTime(2020,1,1).AddDays(7),
            };
            var service = new Service(sc);

            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            outputList.Should().HaveCount(7);
            outputList[0].NextExecTime.Should().Be(new DateTime(2020, 1, 2));
            outputList[1].NextExecTime.Should().Be(new DateTime(2020, 1, 3));
            outputList[2].NextExecTime.Should().Be(new DateTime(2020, 1, 4));
            outputList[3].NextExecTime.Should().Be(new DateTime(2020, 1, 5));
            outputList[4].NextExecTime.Should().Be(new DateTime(2020, 1, 6));
            outputList[5].NextExecTime.Should().Be(new DateTime(2020, 1, 7));
            outputList[6].NextExecTime.Should().Be(new DateTime(2020, 1, 8));
        }
    

        [Theory]
        [InlineData("Occurs every 2 days.Schedule will be used on 05/02/2024 at 00:00 starting on 01/01/0001", 2024, 2, 3, 2)]
        [InlineData("Occurs every 10 days.Schedule will be used on 10/02/2024 at 00:00 starting on 01/01/0001", 2024, 1, 31, 10)]
        [InlineData("Occurs every 365 days.Schedule will be used on 02/01/2025 at 00:00 starting on 01/01/0001", 2024, 1,3, 365)]
        public void ReturnExpectedDescription(string expectedDescription, int year, int month, int day, int days)
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(year, month, day),
                Days = days,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = new DateTime(year, month, day),
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.Description.Should().Be(expectedDescription);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(200)]
        public void ReturnExpectedDatesDependingOnDays(int days)
        {
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1,12,37,0),
                Days = days,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = null,
                Type = ConfigurationType.Recurring,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            var expectedDate = new DateTime(2020, 1, 1, 12, 37, 0).AddDays(days);

            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            foreach (var output in outputList)
            {
                output.NextExecTime.Should().Be(expectedDate);
                expectedDate = expectedDate.AddDays(days);
            }
        }
    }
}
