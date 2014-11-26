using System;
using NUnit.Framework;

namespace Calendar
{
    [TestFixture]
    class ZodiacalSignTests
    {
        [Test]
        public void GetZodiacalSign_OnDate_ReturnsCorrectZodiacalSign()
        {
            var date = new DateTime(2013, 1, 1);
            var zodiacalSign = "Козерог";
            while (date.Year < 2015)
            {
                var result = ZodiacalSign.GetZodiacalSign(date);
                Assert.AreEqual(zodiacalSign, result);
                date = date.AddDays(1);
                foreach (var entry in ZodiacalSign.DateRangeToZodiacalSignMap)
                {
                    if (date == new DateTime(date.Year, entry.Item1 / 100, entry.Item1 % 100))
                        zodiacalSign = entry.Item3;
                }
            }
        }
    }
}