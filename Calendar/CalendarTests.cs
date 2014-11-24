using System;
using NUnit.Framework;

namespace Calendar
{
    [TestFixture]
    class CalendarTests
    {
        public void Calendar_StringWithIncorrectDate_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => new Calendar("incorrect date"));
        }

        public void Calendar_OnNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Calendar(null));
        }

        [TestCase("24.11.2014", 24, 11, 2014)]
        [TestCase("04.01.2010", 4, 1, 2010)]
        [TestCase("4.1.10", 4, 1, 2010)]
        [TestCase("1.1.0001", 1, 1, 1)]
        [TestCase("1.01.1", 1, 1, 2001)]
        public void Calendar_DateInRussianCultureFormat_CreatesAppropriateDateTimeInstance(string date, int day, int month, int year)
        {
            var result = new Calendar(date).Date;

            Assert.AreEqual(new DateTime(year,month,day), result);
        }

    }
}