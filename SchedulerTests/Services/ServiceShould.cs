using System;
using System.Collections.Generic;
using System.Linq;
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
                new Input(DateTime.Now),
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

        [Theory]
        [InlineData("2024-07-09 10:30", "Occurs every day.Schedule will be used on 09/07/2024 at 10:30 starting on 01/01/0001", "2024-07-09 10:30", null, ConfigurationType.Recurring)]
        [InlineData("2023-12-25 12:00", "Occurs every day.Schedule will be used on 25/12/2023 at 12:00 starting on 01/01/0001", "2023-12-25 12:00", "2023-12-24 08:00", ConfigurationType.Recurring)]
        [InlineData("2025-01-01 08:00", "Occurs once.Schedule will be used on 01/01/2025 at 08:00 starting on 01/01/0001", "2024-12-31", "2025-01-01 08:00", ConfigurationType.Once)]
        public void CalculateCorrectOutput(string stringNextExecTime, string expectedDescription, string stringCurrentDate, string stringDate, ConfigurationType type)
        {
            //Arrange
            var currentDate = DateTime.Parse(stringCurrentDate);
            DateTime? date = string.IsNullOrEmpty(stringDate)? null: DateTime.Parse(stringDate);
            var expectedNextExecTime = DateTime.Parse(stringNextExecTime);
            var expectedOutput = new Output(expectedNextExecTime, expectedDescription);
            var schedulerInput = new SchedulerInput(
                new Input(currentDate),
                new Configuration(date, true, 0, Occurrence.Daily, type),
                new Limits(DateTime.MinValue, null)
            );
            var service = new Service();

            //Act
            var resultOutput = service.CalculateOutput(schedulerInput);


            //Assert
            Assert.Equal(expectedOutput.NextExecTime, resultOutput.NextExecTime);
            Assert.Equal(expectedOutput.Description, resultOutput.Description);
            Assert.Equal(expectedNextExecTime, resultOutput.NextExecTime);
            Assert.Equal(expectedDescription, resultOutput.Description);
        }
    }
}
