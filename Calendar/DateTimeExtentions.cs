using System;

namespace Calendar
{
    static class DateTimeExtentions
    {
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
    }
}
