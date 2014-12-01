using System;
using System.Linq;
using NUnit.Framework;

namespace Calendar
{
    [TestFixture]
    class CalendarTests
    {
        private static readonly object[] DistributeByDaysOfTheWeekCases =
        {
            new object[]
            {
                25, 12, 2013, new[]
                {
                    new[] {0, 0, 0, 0, 0, 0, 0, 1},
                    new[] {0, 2, 3, 4, 5, 6, 7, 8},
                    new[] {0, 9, 10, 11, 12, 13, 14, 15},
                    new[] {0, 16, 17, 18, 19, 20, 21, 22},
                    new[] {0, 23, 24, 25, 26, 27, 28, 29},
                    new[] {0, 30, 31, 0, 0, 0, 0, 0}
                }, new[]
                {
                    new[] {48, 0, 0, 0, 0, 0, 0, 1},
                    new[] {49, 2, 3, 4, 5, 6, 7, 8},
                    new[] {50, 9, 10, 11, 12, 13, 14, 15},
                    new[] {51, 16, 17, 18, 19, 20, 21, 22},
                    new[] {52, 23, 24, 25, 26, 27, 28, 29},
                    new[] {1, 30, 31, 0, 0, 0, 0, 0}
                }
            },
            new object[]
            {
                21, 12, 2017, new[]
                {
                    new[] {0, 0, 0, 0, 0, 1, 2, 3},
                    new[] {0, 4, 5, 6, 7, 8, 9, 10},
                    new[] {0, 11, 12, 13, 14, 15, 16, 17},
                    new[] {0, 18, 19, 20, 21, 22, 23, 24},
                    new[] {0, 25, 26, 27, 28, 29, 30, 31}
                }, new[]
                {
                    new[] {48, 0, 0, 0, 0, 1, 2, 3},
                    new[] {49, 4, 5, 6, 7, 8, 9, 10},
                    new[] {50, 11, 12, 13, 14, 15, 16, 17},
                    new[] {51, 18, 19, 20, 21, 22, 23, 24},
                    new[] {52, 25, 26, 27, 28, 29, 30, 31}
                }
            },
            new object[]
            {
                22, 12, 2016, new[]
                {
                    new[] {0, 0, 0, 0, 1, 2, 3, 4},
                    new[] {0, 5, 6, 7, 8, 9, 10, 11},
                    new[] {0, 12, 13, 14, 15, 16, 17, 18},
                    new[] {0, 19, 20, 21, 22, 23, 24, 25},
                    new[] {0, 26, 27, 28, 29, 30, 31, 0}
                },new[]
                {
                    new[] {48, 0, 0, 0, 1, 2, 3, 4},
                    new[] {49, 5, 6, 7, 8, 9, 10, 11},
                    new[] {50, 12, 13, 14, 15, 16, 17, 18},
                    new[] {51, 19, 20, 21, 22, 23, 24, 25},
                    new[] {1, 26, 27, 28, 29, 30, 31, 0}
                }
            },
            new object[]
            {
                14, 1, 2015, new[]
                {
                    new[] {0, 0, 0, 0, 1, 2, 3, 4},
                    new[] {0, 5, 6, 7, 8, 9, 10, 11},
                    new[] {0, 12, 13, 14, 15, 16, 17, 18},
                    new[] {0, 19, 20, 21, 22, 23, 24, 25},
                    new[] {0, 26, 27, 28, 29, 30, 31, 0}
                },new[]
                {
                    new[] {1, 0, 0, 0, 1, 2, 3, 4},
                    new[] {2, 5, 6, 7, 8, 9, 10, 11},
                    new[] {3, 12, 13, 14, 15, 16, 17, 18},
                    new[] {4, 19, 20, 21, 22, 23, 24, 25},
                    new[] {5, 26, 27, 28, 29, 30, 31, 0}
                }
            },
            new object[]
            {
                20, 12, 2014, new[]
                {
                    new[] {0, 1, 2, 3, 4, 5, 6, 7},
                    new[] {0, 8, 9, 10, 11, 12, 13, 14},
                    new[] {0, 15, 16, 17, 18, 19, 20, 21},
                    new[] {0, 22, 23, 24, 25, 26, 27, 28},
                    new[] {0, 29, 30, 31, 0, 0, 0, 0}
                },new[]
                {
                    new[] {48, 1, 2, 3, 4, 5, 6, 7},
                    new[] {49, 8, 9, 10, 11, 12, 13, 14},
                    new[] {50, 15, 16, 17, 18, 19, 20, 21},
                    new[] {51, 22, 23, 24, 25, 26, 27, 28},
                    new[] {1, 29, 30, 31, 0, 0, 0, 0}
                }
            }
        };

        [TestCaseSource("DistributeByDaysOfTheWeekCases")]
        public void DistributeByDaysOfTheWeek_OnDate_ReturnsMatrixOfDaysDistributedFromMondayToSunday(
            int day, int month, int year, int[][] expectedDistribution, int[][] notUsed)
        {
            var date = new DateTime(year, month, day);

            var result = Calendar.CalendarPage.DistributeByDaysOfTheWeek(date);

            Assert.AreEqual(expectedDistribution, result);
        }

        [TestCaseSource("DistributeByDaysOfTheWeekCases")]
        public void
            InitWeekNumbers_OnDateAndDistributionOfDaysOfTheWeek_ReturnsThisDistributionWithWeekNumbersInitialized(
                int day, int month, int year, int[][] inputDistribution, int[][] expected)
        {
            var result = Calendar.CalendarPage.InitWeekNumbers(inputDistribution.ToList(), new DateTime(year, month, day));

            Assert.AreEqual(expected,result);
        }

        [TestCase(1, 1)]
        [TestCase(7, 1)]
        [TestCase(8, 2)]
        [TestCase(14, 2)]
        [TestCase(15, 3)]
        public void GetWeekOfYear_DayOfYear_ReturnsWeekOfYear(int dayOfYear, int expected)
        {
            var result = Calendar.CalendarPage.GetWeekOfYear(dayOfYear);

            Assert.AreEqual(expected, result);
        }
    }
}