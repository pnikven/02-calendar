using System;
using NUnit.Framework;

namespace Calendar.Tests
{
    [TestFixture]
    class WeekTests
    {
        [Test]
        public void Equals_ForEqualWeeks_ReturnsTrue()
        {
            var firstWeek = new Week(new DateTime(2013, 12, 2), new DateTime(2013, 12, 25));
            var secondWeek = new Week(new DateTime(2013, 12, 2), new DateTime(2013, 12, 25));

            Assert.AreEqual(firstWeek, secondWeek);
        }

        [Test]
        public void Equals_ForDifferentWeeks_ReturnsFalse()
        {
            var firstWeek = new Week(new DateTime(2013, 12, 2), new DateTime(2013, 12, 25));
            var secondWeek = new Week(new DateTime(2013, 12, 3), new DateTime(2013, 12, 25));

            Assert.AreNotEqual(firstWeek, secondWeek);            
        }
    }
}