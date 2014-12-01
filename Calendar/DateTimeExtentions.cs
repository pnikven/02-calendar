using System;

namespace Calendar
{
    static class DateTimeExtentions
    {
        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        // очень плохое название метода, поскольку у этого класса уже есть свойство с точно таким же названием
        // это может ввести в заблуждение читателя - своего рода обман, а это плохо
        // надо переименовать
        public static int DayOfWeek(this DateTime date)
        {
            var intRepresentation = (int)date.DayOfWeek;
            // тут уместно добавить комментарий, почему такая логика - что в перечислении DayOfWeek первым днем
            // является воскресенье
            return intRepresentation == 0 ? 7 : intRepresentation;
        }
    }
}
