using Scheduler.Enums;
using Scheduler.Models;
using Xunit;

namespace SchedulerTests.Models
{
    public class SchedulerInputModelShould
    {
        [Fact]
        public void InitializeProperly()
        {
            //Arrange
             var date = DateTime.Now;
             var expectedInput = new Input(date);
             var expectedConfig = new Configuration(null, true, 0, Occurrence.Daily, ConfigurationType.Recurring);
             var expectedLimits = new Limits(date, null);
             //Act
             var schedulerInput = new SchedulerInput(expectedInput, expectedConfig, expectedLimits);

             //Assert
             Assert.IsType<SchedulerInput>(schedulerInput);
             Assert.Equal(expectedInput, schedulerInput.Input);
             Assert.Equal(expectedConfig, schedulerInput.Configuration);
             Assert.Equal(expectedLimits, schedulerInput.Limits);
        }

        [Theory]
        [InlineData("2024-07-08", "2024-07-10", true, 5, Occurrence.Daily, ConfigurationType.Once, "2024-07-01", "2024-07-15")]
        [InlineData("2024-07-08", null, false, 3, Occurrence.Daily, ConfigurationType.Recurring, "2024-07-01", null)]
        [InlineData("2024-07-08", "2024-07-12", true, 1, Occurrence.Daily, ConfigurationType.Once, "2024-07-01", "2024-07-20")]
        [InlineData("2024-07-08", null, true, 10, Occurrence.Daily, ConfigurationType.Recurring, "2024-07-01", "2024-07-30")]
        public void SetPropertiesCorrectly(string currentDate, string? date, bool isEnabled, int days,
            Occurrence occurs,
            ConfigurationType type, string startDate, string? endDate)
        {
            //Arrange
            var expectedCurrentDate = DateTime.Parse(currentDate);
            var expectedStartDate = DateTime.Parse(startDate);
            DateTime? expectedDate = string.IsNullOrEmpty(date)? null : DateTime.Parse(date);
            DateTime? expectedEndDate = string.IsNullOrEmpty(endDate) ? null : DateTime.Parse(endDate);
            var expectedInput = new Input(expectedCurrentDate);
            var expectedConfiguration = new Configuration(expectedDate, isEnabled, days, occurs, type);
            var expectedLimits = new Limits(expectedStartDate, expectedEndDate);

            //Act
            var schedulerInput = new SchedulerInput(expectedInput, expectedConfiguration, expectedLimits)
            {
                Input = expectedInput,
                Configuration = expectedConfiguration,
                Limits = expectedLimits
            };

            //Assert
            Assert.Equal(expectedInput, schedulerInput.Input);
            Assert.Equal(expectedConfiguration, schedulerInput.Configuration);
            Assert.Equal(expectedLimits, schedulerInput.Limits);

        }
    }
}
