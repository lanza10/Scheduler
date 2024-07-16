using FluentAssertions;
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services;

namespace SchedulerTests
{
    public class ServiceShould
    {
        [Fact]
        public void ReturnOnceOutputWithOnceConfiguration()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020,1,1),
                Days = 1,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = new DateTime(2020,1,4 , 14, 0,0),
                Type = ConfigurationType.Once,

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2020, 1, 4,14,0,0));
            output.Description.Should()
                .Be("Occurs once.Schedule will be used on 04/01/2020 at 14:00 starting on 01/01/0001");
            output.AllNextDates.Should().HaveCount(1);
        }

        [Fact]
        public void ReturnRecurringOutputWithRecurringConfiguration()
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
            output.AllNextDates.Should().NotBeNull();
        }

        [Fact]
        public void ReturnTheMaxOfDatesIfEndDateIsTooHigh()
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
            var output = service.GetOutput();

            //Assert
            output.AllNextDates.Should().HaveCount(RecurringSchedulerService.MaxDates);
            output.AllNextDates[0].Should().Be(new DateTime(2020, 1, 3));
            output.AllNextDates[1].Should().Be(new DateTime(2020, 1, 5));
            output.AllNextDates[2].Should().Be(new DateTime(2020, 1, 7));
            output.AllNextDates[3].Should().Be(new DateTime(2020, 1, 9));
            output.AllNextDates[4].Should().Be(new DateTime(2020, 1, 11));
            output.AllNextDates[5].Should().Be(new DateTime(2020, 1, 13));
            output.AllNextDates[6].Should().Be(new DateTime(2020, 1, 15));
        }
        [Fact]
        public void ReturnDatesUntilTheEndDate()
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
            var output = service.GetOutput();

            //Assert
            output.AllNextDates.Should().HaveCount(3);
            output.AllNextDates[0].Should().Be(new DateTime(2020, 1, 3));
            output.AllNextDates[1].Should().Be(new DateTime(2020, 1, 5));
            output.AllNextDates[2].Should().Be(new DateTime(2020, 1, 7));
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
            output.AllNextDates[0].Month.Should().Be(2);
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
            output.AllNextDates.First().Year.Should().Be(2021);
            output.AllNextDates .First().Month.Should().Be(1);
        }

        [Theory]
        [InlineData("Occurs once.Schedule will be used on 03/02/2024 at 00:00 starting on 01/01/0001", 2024,2,3,ConfigurationType.Once)]
        [InlineData("Occurs every day.Schedule will be used on 05/02/2024 at 00:00 starting on 01/01/0001", 2024, 2, 3, ConfigurationType.Recurring)]
        [InlineData("Occurs once.Schedule will be used on 01/01/0001 at 00:00 starting on 01/01/0001", 1, 1, 1, ConfigurationType.Once)]
        [InlineData("Occurs once.Schedule will be used on 31/12/9999 at 00:00 starting on 01/01/0001", 9999, 12, 31, ConfigurationType.Once)]
        public void ReturnExpectedDescription(string expectedDescription, int year, int month, int day , ConfigurationType type)
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(year, month, day),
                Days = 2,
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = new DateTime(year, month, day),
                Type = type,

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
            var output = service.GetOutput();

            //Assert
            foreach (var date in output.AllNextDates)
            {
                date.Should().Be(expectedDate);
                expectedDate = expectedDate.AddDays(days);
            }
        }
    }
}
