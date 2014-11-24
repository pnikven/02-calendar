using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Calendar
{
    class Calendar
    {
        private const int DistributionByDayOfWeekMatrixWidth = 8;
        public DateTime Date;
        public int[][] DistributionByDaysOfTheWeek;

        public Calendar(string date)
        {
            var culture = new CultureInfo("ru-RU");
            Date = DateTime.Parse(date, culture);
            DistributionByDaysOfTheWeek = DistributeByDaysOfTheWeek(Date);
        }

        public static int[][] InitWeekNumbers(List<int[]> arrayOfWeeks, DateTime date)
        {
            var weekOfYear = GetWeekOfYear(date.DayOfYear);
            foreach (var week in arrayOfWeeks)
                week[0] = weekOfYear++;
            if (date.Month == 12 && new DateTime(date.Year, 12, 31).DayOfWeek != DayOfWeek.Sunday)
                arrayOfWeeks[arrayOfWeeks.Count - 1][0] = 1;
            return arrayOfWeeks.ToArray();
        }

        public static int[][] DistributeByDaysOfTheWeek(DateTime date)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var dayOfMonth = 1;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var dayOfWeek = DayOfWeekToInt(firstDayOfMonth.DayOfWeek);

            var result = new List<int[]>();
            var weekDays = new int[DistributionByDayOfWeekMatrixWidth];
            while (dayOfMonth <= daysInMonth)
            {
                weekDays[dayOfWeek] = dayOfMonth;
                dayOfMonth++;
                dayOfWeek++;
                if (dayOfWeek <= 7) 
                    continue;
                result.Add(weekDays);
                weekDays = new int[DistributionByDayOfWeekMatrixWidth];
                dayOfWeek = 1;
            }
            if (dayOfWeek > 1)
                result.Add(weekDays);
            return InitWeekNumbers(result, firstDayOfMonth);
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