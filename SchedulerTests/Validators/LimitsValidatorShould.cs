using Scheduler.Models;
using Scheduler.Validator;
using Xunit;

namespace SchedulerTests.Validators
{
    public class LimitsValidatorShould
    {
        [Theory]
        [InlineData("2020-01-01", null)]
        [InlineData("2020-01-01 00:00", "2020-01-01 00:00")]
        [InlineData("2020-01-01", "2020-01-02")]
        public void ValidateValidLimits(string startDate, string? endDate)
        {
            //Arrange
            var parsedStartDate = DateTime.Parse(startDate);
            DateTime? parsedEndDate = string.IsNullOrEmpty(endDate)? null: DateTime.Parse(endDate);
            var limits = new Limits(parsedStartDate, parsedEndDate);
            //Act
            var isValid = LimitsValidator.ValidLimits(limits);
            //Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("2020-01-02", "2020-01-01")]
        public void InvalidateInvalidLimits(string startDate, string? endDate)
        {
            //Arrange
            var parsedStartDate = DateTime.Parse(startDate);
            var parsedEndDate = DateTime.Parse(endDate);
            var limits = new Limits(parsedStartDate, parsedEndDate);
            //Act
            var isValid = LimitsValidator.ValidLimits(limits);
            //Assert
            Assert.False(isValid);
        }
    }
}
