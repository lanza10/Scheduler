
using FluentAssertions;
using Scheduler.Enums;
using Scheduler.Exceptions;
using Scheduler.Models;
using Scheduler.Services;

namespace SchedulerTests
{
    public class SchedulerServiceShould
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
            var service = new SchedulerService(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2020, 1, 4,14,0,0));
            output.Description.Should()
                .Be("Occurs once.Schedule will be used on 04/01/2020 at 14:00 starting on 01/01/0001");
            output.RecurringDates.Should().BeNull();
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
            var service = new SchedulerService(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2020, 1, 2));
            output.Description.Should()
                .Be("Occurs every day.Schedule will be used on 02/01/2020 at 00:00 starting on 01/01/0001");
            output.RecurringDates.Should().NotBeNull();
        }

        [Fact]
        public void ReturnSevenDatesAsMaxIfEndDateIsTooHigh()
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
            var service = new SchedulerService(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.RecurringDates.Should().HaveCount(7);
            output.RecurringDates![0].Should().Be(new DateTime(2020, 1, 3));
            output.RecurringDates![1].Should().Be(new DateTime(2020, 1, 5));
            output.RecurringDates![2].Should().Be(new DateTime(2020, 1, 7));
            output.RecurringDates![3].Should().Be(new DateTime(2020, 1, 9));
            output.RecurringDates![4].Should().Be(new DateTime(2020, 1, 11));
            output.RecurringDates![5].Should().Be(new DateTime(2020, 1, 13));
            output.RecurringDates![6].Should().Be(new DateTime(2020, 1, 15));
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
            var service = new SchedulerService(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.RecurringDates.Should().HaveCount(3);
            output.RecurringDates![0].Should().Be(new DateTime(2020, 1, 3));
            output.RecurringDates![1].Should().Be(new DateTime(2020, 1, 5));
            output.RecurringDates![2].Should().Be(new DateTime(2020, 1, 7));
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
            var service = new SchedulerService(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.RecurringDates![0].Month.Should().Be(2);
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
            var service = new SchedulerService(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.RecurringDates![0].Year.Should().Be(2021);
            output.RecurringDates![0].Month.Should().Be(1);
        }
    }
}
