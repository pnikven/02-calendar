using System;
using System.Globalization;

namespace Calendar
{
    class Calendar
    {
        public DateTime Date;

        public Calendar(string date)
        {
            var culture = new CultureInfo("ru-RU");
            Date = DateTime.Parse(date, culture);
        }
    }
}