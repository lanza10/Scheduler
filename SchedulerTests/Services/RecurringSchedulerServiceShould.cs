using System.Runtime.InteropServices.JavaScript;
using Scheduler.Enums;
using Scheduler.Interfaces;
using Scheduler.Models;
using Xunit;
using Scheduler.Services;
using Scheduler.Exceptions;
using FluentAssertions;

namespace SchedulerTests.Services
{
    public class RecurringSchedulerServiceShould
    {
        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(0)]
        public void ReturnExpectedDate(int days)
        {
            //Arrange
            var currentDate = new DateTime(2020, 1, 1);
            var expectedDate = currentDate.AddDays(days);
            ISchedulerInput schedulerInput = new SchedulerInput(
                currentDate,
                new Configuration(null, true, days, Occurrence.Daily, ConfigurationType.Recurring),
                new Limits(DateTime.MinValue, null)
                );
            var service = new RecurringSchedulerService();

            //Act
            var resultDate = service.CalculateNextDate(schedulerInput);

            //Assert
            Assert.Equal(expectedDate, resultDate);
        }

        [Theory]
        [InlineData(2, "Occurs every day.Schedule will be used on 03/01/2020 at 14:00 starting on 01/01/0001", Occurrence.Daily)]
        [InlineData(5, "Occurs every day.Schedule will be used on 06/01/2020 at 14:00 starting on 01/01/0001", Occurrence.Daily)]
        [InlineData(0, "Occurs every day.Schedule will be used on 01/01/2020 at 14:00 starting on 01/01/0001", Occurrence.Daily)]
        public void ReturnExpectedDescription(int days, string expectedDescription, Occurrence occurs)
        {
            //Arrange
            ISchedulerInput schedulerInput = new SchedulerInput(
                new DateTime(2020, 1, 1, 14, 0, 0),
                new Configuration(null, true, days, occurs, ConfigurationType.Recurring),
                new Limits(DateTime.MinValue, null)
                );
            var service = new RecurringSchedulerService();

            //Act
            var resultDescription = service.GenerateDescription(schedulerInput);

            //Assert
            Assert.Equal(expectedDescription, resultDescription);
        }

        [Fact]
        public void RaiseErrorWhenExceedStartDateLimits()
        {
            //Arrange
            ISchedulerInput schedulerInput = new SchedulerInput(
                DateTime.MinValue,
                new Configuration(null, true, 0, Occurrence.Daily, ConfigurationType.Recurring),
                new Limits(DateTime.MaxValue, null)
                );
            var service = new RecurringSchedulerService();

            //Act
            var act = () => service.CalculateNextDate(schedulerInput);

            //Assert
            act.Should().Throw<LimitsException>()
                .WithMessage("The result date must not be earlier than the specified start date.");
        }

        [Fact]
        public void RaiseErrorWhenExceedEndDateLimits()
        {
            //Arrange
            ISchedulerInput schedulerInput = new SchedulerInput(
                DateTime.MaxValue,
                new Configuration(null, true, 0, Occurrence.Daily, ConfigurationType.Recurring),
                new Limits(DateTime.MinValue, DateTime.MinValue));
            var service = new RecurringSchedulerService();

            //Act
            var act = () => service.CalculateNextDate(schedulerInput);

            //Assert
            act.Should().Throw<LimitsException>()
                .WithMessage("The result date must not be later than the specified end date.");
        }

        [Theory]
        [InlineData(99)]
        [InlineData(1000)]
        public void RaiseErrorWhenInvalidOccurrence(int occurs)
        {
            //Arrange
            ISchedulerInput schedulerInput = new SchedulerInput(
                new DateTime(2020, 1, 1),
                new Configuration(null, true, 0, (Occurrence)occurs, ConfigurationType.Recurring),
                new Limits(DateTime.MinValue, null)
                );
            var service = new RecurringSchedulerService();

            //Act
            var act = () => service.GenerateDescription(schedulerInput);

            //Assert
            act.Should().Throw<KeyNotFoundException>();
        }
    }
}
