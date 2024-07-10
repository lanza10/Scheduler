using Scheduler.Interfaces;
using Scheduler.Models;

namespace SchedulerTests.Models
{
    public class InputModelShould
    {

        [Fact]
        public void SetPropertiesCorrectly()
        {
            //Arrange
            var expectedCurrentDate = new DateTime(2020, 1, 1);

            //Act
            var input = new Input(expectedCurrentDate);

            //Assert
            Assert.Equal(expectedCurrentDate, input.CurrentDate);
        }


    }
}
