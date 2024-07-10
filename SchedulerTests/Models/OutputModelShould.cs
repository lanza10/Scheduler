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
        public void SetPropertiesCorrectly()
        {
            //Arrange
            var expectedNextExecTime = DateTime.Now;
            const string expectedDescription = "test";

            //Act
            var output = new Output(expectedNextExecTime, expectedDescription);

            //Assert
            Assert.Equal(expectedNextExecTime, output.NextExecTime);
            Assert.Equal(expectedDescription, output.Description);
        }
    }
}
