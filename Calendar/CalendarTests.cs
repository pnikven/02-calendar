using System;
using NUnit.Framework;

namespace Calendar
{
    [TestFixture]
    class CalendarTests
    {
        [Test]
        public void Calendar_StringWithIncorrectDate_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => new Calendar("incorrect date"));
        }

        [Test]
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

            Assert.AreEqual(new DateTime(year, month, day), result);
        }

        [Test]
        public void InitializeValuesMatrix_OnDate_ReturnsMatrixOfDaysDistributedByWeeks()
        {
            var date = new DateTime(2013, 12, 25);
            var expected = new[]
            {
                new [] {48,0,0,0,0,0,0,1},
                new [] {49,2,3,4,5,6,7,8},
                new [] {50,9,10,11,12,13,14,15},
                new [] {51,16,17,18,19,20,21,22},
                new [] {52,23,24,25,26,27,28,29},
                new [] {1,30,31,0,0,0,0,0},
            };

            var result = Calendar.InitializeValuesMatrix(date);

            Assert.AreEqual(expected, result);
        }

        [TestCase(DayOfWeek.Monday, 1)]
        [TestCase(DayOfWeek.Tuesday, 2)]
        [TestCase(DayOfWeek.Wednesday, 3)]
        [TestCase(DayOfWeek.Thursday, 4)]
        [TestCase(DayOfWeek.Friday, 5)]
        [TestCase(DayOfWeek.Saturday, 6)]
        [TestCase(DayOfWeek.Sunday, 7)]
        public void DayOfWeekToInt_DayOfWeek_ConvertsToIntWhereMondayIsTheFirstDayOfWeek(DayOfWeek input, int expected)
        {
            var result = Calendar.DayOfWeekToInt(input);

            Assert.AreEqual(expected, result);
        }

    }
}