using System;

namespace Calendar
{
    static class DateTimeExtentions
    {
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime FirstDayOfWeek(this DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Monday)
                date = date.AddDays(-1);
            return date;
        }
    }
}
