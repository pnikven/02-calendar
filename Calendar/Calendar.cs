using System;
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
            var firstDayOfNextMonth = firstDayOfMonth.AddMonths(1);
            var lastDayOfMonth = firstDayOfNextMonth.AddDays(-1);
            var dayOfWeek = DayOfWeekToInt(firstDayOfMonth.DayOfWeek);

            throw new NotImplementedException();
        }

        public static int DayOfWeekToInt(DayOfWeek dayOfWeek)
        {
            throw new NotImplementedException();
        }
    }
}