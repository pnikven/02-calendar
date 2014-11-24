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
            throw new NotImplementedException();
        }
    }
}