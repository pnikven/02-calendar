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

        public Calendar(string date)
        {
            var culture = new CultureInfo("ru-RU");
            Date = DateTime.Parse(date, culture);
            var distribution = DistributeByDaysOfTheWeek(Date);
            DistributionByDaysOfTheWeek = InitWeekNumbers(distribution, Date);
        }

        public static List<int[]> DistributeByDaysOfTheWeek(DateTime date)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var dayOfWeek = DayOfWeekToInt(date.FirstDayOfMonth().DayOfWeek);
            var result = new List<int[]>();
            for (var dayOfMonth = 1; dayOfMonth <= daysInMonth; dayOfMonth++, dayOfWeek = dayOfWeek % 7 + 1)
            {
                if (result.Count==0 || dayOfWeek == 1)
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

        public static int GetWeekOfYear(int dayOfYear)
        {
            return (dayOfYear - 1) / 7 + 1;
        }

        public static int DayOfWeekToInt(DayOfWeek dayOfWeek)
        {
            var intRepresentation = (int)dayOfWeek;
            return intRepresentation == 0 ? 7 : intRepresentation;
        }
    }
}