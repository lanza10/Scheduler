using Scheduler.Enums;
using Scheduler.Interfaces;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Scheduler.Exceptions;
using Scheduler.Services;

namespace SchedulerTests.Services
{
    public class OnceSchedulerServiceShould
    {
        [Fact]
        public void ReturnCorrectDate()
        {
            //Arrange
            var expectedDate = new DateTime(2020,1,1);
            var configuration = new Configuration(expectedDate, true, 5, Occurrence.Daily, ConfigurationType.Once);
            var limits = new Limits(DateTime.MinValue, null);
            ISchedulerInput schedulerInput = new SchedulerInput(DateTime.Now, configuration, limits);
            var service = new OnceSchedulerService();
            //Act
            var resultDate = service.CalculateNextDate(schedulerInput);
            //Assert
            Assert.Equal(expectedDate, resultDate);
        }

        [Fact]
        public void ReturnCorrectDescription()
        {
            //Arrange
            var expectedDate = new DateTime(2020,1,1);
            const string expectedDescription = "Occurs once.Schedule will be used on 01/01/2020 at 00:00 starting on 01/01/0001";
            var configuration = new Configuration(expectedDate, true, 5, Occurrence.Daily, ConfigurationType.Once);
            var limits = new Limits(DateTime.MinValue, null);
            ISchedulerInput schedulerInput = new SchedulerInput(DateTime.Now, configuration, limits);
            var service = new OnceSchedulerService();
            //Act
            var resultDescription = service.GenerateDescription(schedulerInput);
            //Assert
            Assert.Equal(expectedDescription, resultDescription);
        }

        [Fact]
        public void RaiseErrorWhenExceedStartDateLimits()
        {
            //Arrange
            var expectedDate = DateTime.MinValue;
            var startDate = DateTime.Now;
            var configuration = new Configuration(expectedDate, true, 5, Occurrence.Daily, ConfigurationType.Once);
            var limits = new Limits(startDate, null);
            ISchedulerInput schedulerInput = new SchedulerInput(DateTime.Now, configuration, limits);
            var service = new OnceSchedulerService();

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
            var expectedDate = DateTime.MaxValue;
            var startDate = DateTime.MinValue;
            var endDate = DateTime.Now;
            var configuration = new Configuration(expectedDate, true, 5, Occurrence.Daily, ConfigurationType.Once);
            var limits = new Limits(startDate, endDate);
            ISchedulerInput schedulerInput = new SchedulerInput(DateTime.Now, configuration, limits);
            var service = new OnceSchedulerService();

            //Act
            var act = () => service.CalculateNextDate(schedulerInput);

            //Assert
            act.Should().Throw<LimitsException>()
                .WithMessage("The result date must not be later than the specified end date.");
        }
    }
}
