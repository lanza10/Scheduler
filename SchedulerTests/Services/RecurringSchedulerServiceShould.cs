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
        public void ReturnCorrectDate(int days)
        {
            //Arrange
            var currentDate = DateTime.Now;
            var expectedDate = currentDate.AddDays(days);
            var configuration = new Configuration(null, true, days, Occurrence.Daily, ConfigurationType.Recurring);
            var input = new Input(currentDate);
            var limits = new Limits(DateTime.MinValue, null);
            ISchedulerInput schedulerInput = new SchedulerInput(input, configuration, limits);
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
        public void ReturnCorrectDescription(int days, string expectedDescription, Occurrence occurs)
        {
            //Arrange
            var expectedDate = new DateTime(2020,1,1, 14,0,0);
            var configuration = new Configuration(null, true, days, occurs, ConfigurationType.Recurring);
            var input = new Input(expectedDate);
            var limits = new Limits(DateTime.MinValue, null);
            ISchedulerInput schedulerInput = new SchedulerInput(input, configuration, limits);
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
            var configuration = new Configuration(null, true, 0, Occurrence.Daily, ConfigurationType.Recurring);
            var input = new Input(DateTime.MinValue);
            var limits = new Limits(DateTime.Now, null);
            ISchedulerInput schedulerInput = new SchedulerInput(input, configuration, limits);
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
            var configuration = new Configuration(null, true, 0, Occurrence.Daily, ConfigurationType.Recurring);
            var input = new Input(DateTime.MaxValue);
            var limits = new Limits(DateTime.MinValue, DateTime.Now);
            ISchedulerInput schedulerInput = new SchedulerInput(input, configuration, limits);
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
            var configuration = new Configuration(null, true, 0, (Occurrence)occurs, ConfigurationType.Recurring);
            var input = new Input(DateTime.Now);
            var limits = new Limits(DateTime.MinValue, null);
            ISchedulerInput schedulerInput = new SchedulerInput(input, configuration, limits);
            var service = new RecurringSchedulerService();
            //Act
            var act = () => service.GenerateDescription(schedulerInput);
            //Assert
            Assert.Throws<KeyNotFoundException>(() => service.GenerateDescription(schedulerInput));
        }
    }
}
