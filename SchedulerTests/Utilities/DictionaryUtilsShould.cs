using Scheduler.Enums;
using Scheduler.Utilities;
using Xunit;

namespace SchedulerTests.Utilities
{
    public class DictionaryUtilsShould
    {
        [Theory]
        [InlineData(10)]
        public void RaiseErrorWhenNotValidKey(int key)
        {
            //Arrange

            //Act

            //Assert
            Assert.Throws<KeyNotFoundException>(() => DictionaryUtils.GetValue((Occurrence)key));
        }

        [Theory]
        [InlineData(Occurrence.Daily, "day")]
        public void GetCorrectValueWhenValidKey(Occurrence key, string expected)
        {
            //Arrange

            //Act
            var result = DictionaryUtils.GetValue(key);
            //Assert
            Assert.Equal(expected, result);
        }
    }
}
