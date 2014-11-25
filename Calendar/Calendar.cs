using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Calendar
{
    class Calendar
    {
        public const int DistributionByDayOfWeekMatrixWidth = 8;
        public DateTime Date;
        public int[][] DistributionByDaysOfTheWeek;
        public Tuple<int, int> DayLocation;

        public Calendar(string date)
        {
            var culture = new CultureInfo("ru-RU");
            Date = DateTime.Parse(date, culture);
            var distribution = DistributeByDaysOfTheWeek(Date);
            DistributionByDaysOfTheWeek = InitWeekNumbers(distribution, Date);
            DayLocation = GetDayLocation(Date.Day);
        }

        public static List<int[]> DistributeByDaysOfTheWeek(DateTime date)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var dayOfWeek = date.FirstDayOfMonth().DayOfWeek();
            var result = new List<int[]>();
            for (var dayOfMonth = 1; dayOfMonth <= daysInMonth; dayOfMonth++, dayOfWeek = dayOfWeek % 7 + 1)
            {
                if (result.Count == 0 || dayOfWeek == 1)
                    result.Add(new int[DistributionByDayOfWeekMatrixWidth]);
                result.Last()[dayOfWeek] = dayOfMonth;
            }
            return result;
        }

        public static int[][] InitWeekNumbers(List<int[]> arrayOfWeeks, DateTime date)
        {
            var weekOfYear = GetWeekOfYear(date.FirstDayOfMonth().DayOfYear);
            foreach (var week in arrayOfWeeks)
                week[0] = weekOfYear++;
            if (date.Month == 12 && new DateTime(date.Year, 12, 31).DayOfWeek != DayOfWeek.Sunday)
                arrayOfWeeks[arrayOfWeeks.Count - 1][0] = 1;
            return arrayOfWeeks.ToArray();
        }

        private Tuple<int, int> GetDayLocation(int day)
        {
            for (var i = 0; i < DistributionByDaysOfTheWeek.Length; i++)
                for (var j = 1; j < DistributionByDaysOfTheWeek[i].Length; j++)
                    if (DistributionByDaysOfTheWeek[i][j] == day)
                        return Tuple.Create(i, j);
            throw new Exception("Date not found in DistributionByDaysOfTheWeek Matrix");
        }

        public static int GetWeekOfYear(int dayOfYear)
        {
            return (dayOfYear - 1) / 7 + 1;
        }
    }
}