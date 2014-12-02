using System;
using System.Collections.Generic;

namespace Calendar
{
    class CalendarPageDataProvider
    {
        public IEnumerable<Week> Weeks { get { return CalculateCalendarPageWeeks(); } }

        private DateTime _selectedDate;

        public CalendarPageDataProvider(DateTime selectedDate)
        {
            _selectedDate = selectedDate;
        }

        private Week[] CalculateCalendarPageWeeks()
        {
            var weeks = new List<Week>();
            DateTime firstDayOfWeek = _selectedDate.FirstDayOfMonth().FirstDayOfWeek();
            do
            {
                weeks.Add(new Week(firstDayOfWeek, _selectedDate));
                firstDayOfWeek = firstDayOfWeek.AddDays(7);
            } while (firstDayOfWeek.Month == _selectedDate.Month);
            return weeks.ToArray();
        }
    }
}