using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Scheduler.Models;

namespace SchedulerTests.Models
{
    public class OutputModelShould
    {
        [Fact]
        public void InitializeProperly()
        {
            //Arrange
            var expectedNextExecDate = DateTime.Now;
            const string expectedDescription = "test";

            //Act
            var output = new Output(expectedNextExecDate, expectedDescription);
            
            //Assert
            Assert.IsType<Output>(output);
            Assert.Equal(expectedNextExecDate, output.NextExecTime);
            Assert.Equal(expectedDescription, output.Description);
        }

        [Theory]
        [InlineData("2020-01-05", "test")]
        public void SetPropertiesCorrectly(string date, string expectedDescription)
        {
            //Arrange
            var expectedNextExecDate = DateTime.Parse(date);

            //Act
            var output = new Output(expectedNextExecDate, expectedDescription)
            {
                NextExecTime = expectedNextExecDate,
                Description = expectedDescription
            };

            //Assert
            Assert.Equal(expectedNextExecDate, output.NextExecTime);
            Assert.Equal(expectedDescription, output.Description);
        }
    }
}
