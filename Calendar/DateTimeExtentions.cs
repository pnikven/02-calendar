using System;

namespace Calendar
{
    static class DateTimeExtentions
    {
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static int DayOfWeek(this DateTime date)
        {
            var intRepresentation = (int)date.DayOfWeek;
            return intRepresentation == 0 ? 7 : intRepresentation;
        }
    }
}
