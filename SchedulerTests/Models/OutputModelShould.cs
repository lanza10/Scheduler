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
            var expectedNextExecTime = DateTime.Now;
            const string expectedDescription = "test";

            //Act
            var output = new Output(expectedNextExecTime, expectedDescription);
            
            //Assert
            Assert.IsType<Output>(output);
            Assert.Equal(expectedNextExecTime, output.NextExecTime);
            Assert.Equal(expectedDescription, output.Description);
        }

        [Theory]
        [InlineData("2020-01-05", "test")]
        public void SetPropertiesCorrectly(string date, string expectedDescription)
        {
            //Arrange
            var expectedNextExecTime = DateTime.Parse(date);

            //Act
            var output = new Output(expectedNextExecTime, expectedDescription)
            {
                NextExecTime = expectedNextExecTime,
                Description = expectedDescription
            };

            //Assert
            Assert.Equal(expectedNextExecTime, output.NextExecTime);
            Assert.Equal(expectedDescription, output.Description);
        }
    }
}
