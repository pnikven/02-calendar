using System;
using System.Collections.Generic;
using System.Globalization;

namespace Calendar
{
    class Calendar
    {
        private const int ValuesMatrixWidth = 8;
        public DateTime Date;
        public int[][] Values;

        public Calendar(string date)
        {
            var culture = new CultureInfo("ru-RU");
            Date = DateTime.Parse(date, culture);
            Values = InitializeValuesMatrix(Date);
        }

        public static int[][] InitializeValuesMatrix(DateTime date)
        {
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var monthDayCount = DateTime.DaysInMonth(date.Year, date.Month);            

            var currentDayOfMonth = 1;
            var dayOfWeek = DayOfWeekToInt(firstDayOfMonth.DayOfWeek);
            var weekNumber = GetWeekNumber(firstDayOfMonth.DayOfYear);            

            var result = new List<int[]>();
            var weekDays = new int[ValuesMatrixWidth];
            weekDays[0] = weekNumber;
            while (currentDayOfMonth<=monthDayCount)
            {
                weekDays[dayOfWeek] = currentDayOfMonth;
                currentDayOfMonth++;
                dayOfWeek++;
                if (dayOfWeek <= 7) continue;
                result.Add(weekDays);
                weekDays = new int[ValuesMatrixWidth];
                weekNumber++;
                weekDays[0] = weekNumber;
                dayOfWeek = 1;
            }
            result.Add(weekDays);

            return result.ToArray();
        }

        public static int GetWeekNumber(int dayOfYear)
        {
            throw new NotImplementedException();
        }

        public static int DayOfWeekToInt(DayOfWeek dayOfWeek)
        {
            throw new NotImplementedException();
        }
    }
}