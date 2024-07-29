using System.Globalization;
using FluentAssertions;
using Scheduler.Enums;
using Scheduler.Models;
using Scheduler.Services;

namespace SchedulerTests
{
    public class ServiceWhenOnceShould
    {
        [Fact]
        public void ReturnOnceOutput()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = new DateTime(2020, 1, 4, 14, 0, 0),
                Type = ConfigurationType.Once,

                StartDate = DateTime.MinValue,
                EndDate = null,
            };
            var service = new Service(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(new DateTime(2020, 1, 4, 14, 0, 0));
            output.Description.Should()
                .Be("Occurs once.Schedule will be used on 04/01/2020 at 14:00 starting on 01/01/0001");
        }

        [Fact]
        public void ReturnOnlyOneOutputInOutputList()
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(2020, 1, 1),
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = new DateTime(2020, 1, 1),
                Type = ConfigurationType.Once,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);
            var output = service.GetOutput();

            //Act
            var outputList = service.GetOutputList(7);

            //Assert
            outputList.Should().HaveCount(1);
            outputList.First().NextExecTime.Should().Be(output.NextExecTime);
            outputList.First().Description.Should().Be(output.Description);
        }
        [Theory]
        [InlineData("Occurs once.Schedule will be used on 03/02/2024 at 00:00 starting on 01/01/0001", 2024, 2, 3)]
        [InlineData("Occurs once.Schedule will be used on 01/01/0001 at 00:00 starting on 01/01/0001", 1, 1, 1)]
        [InlineData("Occurs once.Schedule will be used on 31/12/9999 at 00:00 starting on 01/01/0001", 9999, 12, 31)]
        public void ReturnExpectedDescription(string expectedDescription, int year, int month, int day)
        {
            //Arrange
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(year, month, day),
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = new DateTime(year, month, day),
                Type = ConfigurationType.Once,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.Description.Should().Be(expectedDescription);
        }

        [Theory]
        [InlineData(2024, 2, 3)]
        [InlineData(1, 1, 1)]
        [InlineData(9999, 12, 31)]
        public void ReturnExpectedDate(int year, int month, int day)
        {
            //Arrange
            var expectedDate = new DateTime(year, month, day);
            var sc = new SchedulerConfiguration
            {
                CurrentDate = new DateTime(year, month, day),
                IsEnabled = true,
                Occurs = Occurrence.Daily,

                ConfigurationDate = new DateTime(year, month, day),
                Type = ConfigurationType.Once,

                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
            var service = new Service(sc);

            //Act
            var output = service.GetOutput();

            //Assert
            output.NextExecTime.Should().Be(expectedDate);
        }

        [Theory]
        [InlineData("en-US", "Occurs once.Schedule will be used on 2/3/2024 at 00:00 starting on 1/1/0001")]
        [InlineData("fr-FR", "Occurs once.Schedule will be used on 03/02/2024 at 00:00 starting on 01/01/0001")]
        [InlineData("es-ES", "Occurs once.Schedule will be used on 03/02/2024 at 00:00 starting on 01/01/0001")]
        public void WorkWithDifferentCultures(string cultureName, string expectedDesc)
        {
            var originalCulture = CultureInfo.CurrentCulture;

            try
            {
                //Arrange
                CultureInfo.CurrentCulture = new CultureInfo(cultureName);

                var sc = new SchedulerConfiguration
                {
                    CurrentDate = new DateTime(2024, 2, 3),
                    IsEnabled = true,
                    Occurs = Occurrence.Daily,

                    ConfigurationDate = new DateTime(2024, 2, 3),
                    Type = ConfigurationType.Once,

                    StartDate = DateTime.MinValue,
                    EndDate = DateTime.MaxValue,
                };
                var service = new Service(sc);

                //Act
                var output = service.GetOutput();

                //Assert
                output.Description.Should().Be(expectedDesc);
            }
            finally
            {
                CultureInfo.CurrentCulture = originalCulture;
            }
        }
    }
}
