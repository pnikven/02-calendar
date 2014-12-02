using NUnit.Framework;

namespace Calendar.Tests
{
    [TestFixture]
    class WeekDayTests
    {
        [Test]
        public void Equals_ForEqualWeekDays_ReturnsTrue()
        {
            var day1 = new WeekDay(1, false, false, false);
            var day2 = new WeekDay(1, false, false, false);

            Assert.AreEqual(day1, day2);
        }

        private readonly object[] _equalsFalseTestCases =
        {
            new [] { 
                new WeekDay(1, false, false, false), 
                new WeekDay(1, false, false, true)},
            new []
            {
                new WeekDay(1, false, false, false), 
                new WeekDay(1, false, true, false)
            },
            new [] { 
                new WeekDay(1, false, false, false), 
                new WeekDay(1, true, false, false)},
            new []
            {
                new WeekDay(1, false, false, false), 
                new WeekDay(2, false, false, false)
            }
        };
        [TestCaseSource("_equalsFalseTestCases")]
        public void Equals_ForDifferentWeekDays_ReturnsFalse(WeekDay first, WeekDay second)
        {
            Assert.AreNotEqual(first, second);
        }
    }
}