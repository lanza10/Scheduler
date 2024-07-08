using Scheduler.Interfaces;
using Scheduler.Models;

namespace SchedulerTests.Models
{
    public class InputModelShould
    {
        [Fact]
        public void InitializeProperly()
        {
            //Arrange
            var expectedCurrentDate = DateTime.Now;

            //Act
            var input = new Input(expectedCurrentDate);

            //Assert
            Assert.IsType<Input>(input);
            Assert.Equal(expectedCurrentDate, input.CurrentDate);

        }

        [Theory]
        [InlineData("2020,01,04")]
        public void SetPropertiesCorrectly(string date)
        {
            //Arrange
            var expectedCurrentDate = DateTime.Parse(date);

            //Act
            var input = new Input(expectedCurrentDate)
            {
                CurrentDate = expectedCurrentDate
            };

            //Assert
            Assert.Equal(expectedCurrentDate, input.CurrentDate);
        }


    }
}
