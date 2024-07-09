using Scheduler.Enums;
using Scheduler.Interfaces;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Exceptions;
using Scheduler.Services;

namespace SchedulerTests.Services
{
    public class OnceSchedulerServiceShould
    {
        [Theory]
        [InlineData("2020-01-01")]
        [InlineData("9999-12-31")]
        [InlineData("2222-01-01")]
        public void ReturnCorrectDate(string stringDate)
        {
            //Arrange
            var expectedDate = DateTime.Parse(stringDate);
            var configuration = new Configuration(expectedDate, true, 5, Occurrence.Daily, ConfigurationType.Once);
            var input = new Input(DateTime.Now);
            var limits = new Limits(DateTime.MinValue, null);
            ISchedulerInput schedulerInput = new SchedulerInput(input, configuration, limits);
            var service = new OnceSchedulerService();
            //Act
            var resultDate = service.CalculateNextDate(schedulerInput);
            //Assert
            Assert.Equal(expectedDate, resultDate);
        }

        [Theory]
        [InlineData("2020-01-01", "Occurs once.Schedule will be used on 01/01/2020 at 00:00 starting on 01/01/0001")]
        [InlineData("9999-12-31", "Occurs once.Schedule will be used on 31/12/9999 at 00:00 starting on 01/01/0001")]
        [InlineData("2222-01-01", "Occurs once.Schedule will be used on 01/01/2222 at 00:00 starting on 01/01/0001")]
        public void ReturnCorrectDescription(string stringDate, string expectedDescription)
        {
            //Arrange
            var expectedDate = DateTime.Parse(stringDate);
            var configuration = new Configuration(expectedDate, true, 5, Occurrence.Daily, ConfigurationType.Once);
            var input = new Input(DateTime.Now);
            var limits = new Limits(DateTime.MinValue, null);
            ISchedulerInput schedulerInput = new SchedulerInput(input, configuration, limits);
            var service = new OnceSchedulerService();
            //Act
            var resultDescription = service.GenerateDescription(schedulerInput);
            //Assert
            Assert.Equal(expectedDescription, resultDescription);
        }

        [Theory]
        [InlineData("2020-01-01", "2020-01-03", "2024-01-01")]
        [InlineData("2020-01-01", "2020-01-03", "2018-01-01")]
        public void RaiseErrorWhenExceedLimits(string stringStartDate, string stringEndDate, string stringDate)
        {
            //Arrange
            var expectedDate = DateTime.Parse(stringDate);
            var startDate = DateTime.Parse(stringStartDate);
            var endDate = DateTime.Parse(stringEndDate);
            var configuration = new Configuration(expectedDate, true, 5, Occurrence.Daily, ConfigurationType.Once);
            var input = new Input(DateTime.Now);
            var limits = new Limits(startDate, endDate);
            ISchedulerInput schedulerInput = new SchedulerInput(input, configuration, limits);
            var service = new OnceSchedulerService();
            //Act

            //Assert
            Assert.Throws<LimitsException>(() => service.CalculateNextDate(schedulerInput));
        }
    }
}
