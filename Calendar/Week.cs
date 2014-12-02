using System;
using System.Linq;

namespace Calendar
{
    class Week
    {
        public WeekDay[] WeekDays { get; private set; }

        public Week(DateTime firstDayOfWeek, DateTime selectedDate)
        {
            WeekDays = Enumerable.Range(0, 7)
                .Select(i => firstDayOfWeek.AddDays(i))
                .Select(date => new WeekDay(
                    date.Day, 
                    date.DayOfWeek == DayOfWeek.Sunday,
                    date == selectedDate,
                    date.Month == selectedDate.Month))
                .ToArray();
        }

        public override bool Equals(object obj)
        {
            var other = (Week) obj;
            return !WeekDays.Where((t, i) => !t.Equals(other.WeekDays[i])).Any();
        }

        public override int GetHashCode()
        {
            return (WeekDays != null ? WeekDays.GetHashCode() : 0);
        }
    }
}