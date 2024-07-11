using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services;
using Xunit.Sdk;

namespace SchedulerTests.Services
{
    public class ServiceShould
    {
        [Theory]
        [InlineData(ConfigurationType.Once, true)]
        [InlineData(ConfigurationType.Recurring, false)]
        public void GetCorrectSchedulerService(ConfigurationType type, bool isOnce)
        {
            //Arrange
            var schedulerInput = new SchedulerInput(
                DateTime.Now,
                new Configuration(DateTime.Today, true, 0, Occurrence.Daily, type),
                new Limits(DateTime.MinValue, null)
            );
            ISchedulerService expectedSchedulerService = isOnce ? new OnceSchedulerService() : new RecurringSchedulerService();
            var service = new Service();
            //Act
            var resultSchedulerService = service.GetSchedulerService(schedulerInput);
            //Assert
            Assert.Equal(expectedSchedulerService.GetType(), resultSchedulerService.GetType());
        }

        [Fact]
        public void CalculateCorrectOutputWhenRecurring()
        {
            //Arrange
            const string expectedDescription = "Occurs every day.Schedule will be used on 09/07/2024 at 10:30 starting on 01/01/0001";
            var currentDate = new DateTime(2024, 7, 9, 10, 30, 0);
            var expectedOutput = new Output(currentDate, expectedDescription);
            var schedulerInput = new SchedulerInput(
                currentDate,
                new Configuration(null, true, 0, Occurrence.Daily, ConfigurationType.Recurring),
                new Limits(DateTime.MinValue, null)
            );
            var service = new Service();

            //Act
            var resultOutput = service.CalculateOutput(schedulerInput);

            //Assert
            Assert.Equal(expectedOutput.NextExecTime, resultOutput.NextExecTime);
            Assert.Equal(expectedOutput.Description, resultOutput.Description);
            Assert.Equal(currentDate, resultOutput.NextExecTime);
            Assert.Equal(expectedDescription, resultOutput.Description);
        }

        [Fact]
        public void CalculateCorrectOutputWhenOnce()
        {
            //Arrange
            const string expectedDescription = "Occurs once.Schedule will be used on 09/07/2024 at 10:30 starting on 01/01/0001";
            var date = new DateTime(2024, 7, 9, 10, 30, 0);
            var expectedOutput = new Output(date, expectedDescription);
            var schedulerInput = new SchedulerInput(
                DateTime.Now,
                new Configuration(date, true, 0, Occurrence.Daily, ConfigurationType.Once),
                new Limits(DateTime.MinValue, null)
            );
            var service = new Service();

            //Act
            var resultOutput = service.CalculateOutput(schedulerInput);

            //Assert
            Assert.Equal(expectedOutput.NextExecTime, resultOutput.NextExecTime);
            Assert.Equal(expectedOutput.Description, resultOutput.Description);
            Assert.Equal(date, resultOutput.NextExecTime);
            Assert.Equal(expectedDescription, resultOutput.Description);
        }
    }
}
