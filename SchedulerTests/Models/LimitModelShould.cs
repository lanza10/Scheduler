using Scheduler.Models;
using Xunit;

namespace SchedulerTests.Models
{
    public class LimitModelShould
    {
        [Fact]
        public void InitializeProperly()
        {
            //Arrange
            var expectedStartDate = DateTime.MinValue;
            var expectedEndDate = DateTime.MaxValue;

            //Act
            var limits = new Limits(expectedStartDate, expectedEndDate);

            //Assert
            Assert.IsType<Limits>(limits);
            Assert.Equal(expectedStartDate, limits.StartDate);
            Assert.Equal(expectedEndDate, limits.EndDate);
        }

        [Theory]
        [InlineData("2020-01-01", null)]
        [InlineData("2020-01-01", "2024-01-01")]
        public void SetPropertiesCorrectly(string startDate, string endDate)
        {
            //Arrange
            var expectedStartDate = DateTime.Parse(startDate);
            DateTime? expectedEndDate = string.IsNullOrEmpty(endDate) ? null : DateTime.Parse(endDate);

            //Act
            var limits = new Limits(expectedStartDate, expectedEndDate)
            {
                StartDate = expectedStartDate,
                EndDate = expectedEndDate
            };

            //Arrange
            Assert.Equal(expectedStartDate, limits.StartDate);
            Assert.Equal(expectedEndDate, limits.EndDate);
        }
    }
}
