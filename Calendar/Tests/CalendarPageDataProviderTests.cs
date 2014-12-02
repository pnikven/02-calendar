using System;
using NUnit.Framework;

namespace Calendar.Tests
{
    [TestFixture]
    class CalendarPageDataProviderTests
    {
        [Test]
        public void CalendarPageDataProvider_ForSelectedDate_InitializesWeeksWithArrayOfWeeksForCalendarPage()
        {
            var selectedDate=new DateTime(2013, 12, 25);
            var expected = new []
            {
                new Week(new DateTime(2013, 11, 25), selectedDate),
                new Week(new DateTime(2013, 12, 2), selectedDate),
                new Week(new DateTime(2013, 12, 9), selectedDate),
                new Week(new DateTime(2013, 12, 16), selectedDate),
                new Week(new DateTime(2013, 12, 23), selectedDate),
                new Week(new DateTime(2013, 12, 30), selectedDate)
            };

            var result = new CalendarPageDataProvider(selectedDate).Weeks;
            
            Assert.AreEqual(expected, result);
        }
    }
}