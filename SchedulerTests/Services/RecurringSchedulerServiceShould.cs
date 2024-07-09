using Scheduler.Enums;
using Scheduler.Interfaces;
using Scheduler.Models;
using Xunit;
using Scheduler.Services;

namespace SchedulerTests.Services
{
    public class RecurringSchedulerServiceShould
    {
        [Theory]
        [InlineData("2020-01-01", 2 )]
        [InlineData("9999-12-26", 5)]
        [InlineData("0001-01-01", 0)]
        public void ReturnCorrectDate(string stringDate, int days)
        {
            //Arrange
            var currentDate = DateTime.Parse(stringDate);
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
        [InlineData("2020-01-01 14:30", "Occurs every day.Schedule will be used on 01/01/2020 at 14:30 starting on 01/01/0001", Occurrence.Daily)]
        [InlineData("9999-12-31 12:00", "Occurs every day.Schedule will be used on 31/12/9999 at 12:00 starting on 01/01/0001", Occurrence.Daily)]
        [InlineData("2222-01-01 23:59", "Occurs every day.Schedule will be used on 01/01/2222 at 23:59 starting on 01/01/0001", Occurrence.Daily)]
        public void ReturnCorrectDescription(string stringDate, string expectedDescription, Occurrence occurs)
        {
            //Arrange
            var expectedDate = DateTime.Parse(stringDate);
            var configuration = new Configuration(null, true, 0, occurs, ConfigurationType.Once);
            var input = new Input(expectedDate);
            var limits = new Limits(DateTime.MinValue, null);
            ISchedulerInput schedulerInput = new SchedulerInput(input, configuration, limits);
            var service = new RecurringSchedulerService();
            //Act
            var resultDescription = service.GenerateDescription(schedulerInput);
            //Assert
            Assert.Equal(expectedDescription, resultDescription);
        }
    }
}
