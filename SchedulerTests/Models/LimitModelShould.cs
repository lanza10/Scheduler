using FluentAssertions;
using Scheduler.Exceptions;
using Scheduler.Models;
using Xunit;

namespace SchedulerTests.Models
{
    public class LimitModelShould
    {

        [Fact]
        public void SetPropertiesCorrectly()
        {
            //Arrange
            var expectedStartDate = DateTime.MinValue;
            var expectedEndDate = DateTime.MaxValue;

            //Act
            var limits = new Limits(expectedStartDate, expectedEndDate);

            //Arrange
            Assert.Equal(expectedStartDate, limits.StartDate);
            Assert.Equal(expectedEndDate, limits.EndDate);
        }

        [Fact]
        public void AssignMaxValueWhenEndDateIsNull()
        {
            //Arrange
            var startDate = new DateTime(2020,1,1);
            var expectedEndDate = DateTime.MaxValue;

            //Act
            var limits = new Limits(startDate, null);

            //Arrange
            Assert.Equal(expectedEndDate, limits.EndDate);
        }

        [Fact]
        public void RaiseErrorWhenInvalidLimits()
        {
            //Arrange
            var startDate = DateTime.MaxValue;
            var endDate = DateTime.MinValue;
            //Act
            var act = () => new Limits(startDate, endDate);
            //Assert
            act.Should().Throw<LimitsException>().WithMessage("Start date must be earlier than de end date");
        }
    }
}
