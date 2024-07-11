using System.Runtime.InteropServices.JavaScript;
using Scheduler.Enums;
using Scheduler.Models;
using Xunit;

namespace SchedulerTests.Models
{
    public class SchedulerInputModelShould
    {
        [Fact]
        public void SetPropertiesCorrectly()
        {
            //Arrange
            var expectedCurrentDate = DateTime.Now;
            var expectedStartDate = DateTime.MinValue;
            var expectedEndDate = DateTime.MaxValue;
            var expectedConfiguration = new Configuration(null, true, 0, Occurrence.Daily, ConfigurationType.Recurring);
            var expectedLimits = new Limits(expectedStartDate, expectedEndDate);

            //Act
            var schedulerInput = new SchedulerInput(expectedCurrentDate, expectedConfiguration, expectedLimits);

            //Assert
            Assert.Equal(expectedCurrentDate, schedulerInput.CurrentDate);
            Assert.Equal(expectedConfiguration, schedulerInput.Configuration);
            Assert.Equal(expectedLimits, schedulerInput.Limits);
        }
    }
}
