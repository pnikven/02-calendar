using System;

namespace Calendar
{
    class WeekDay
    {
        public int Number { get; private set; }
        public bool BelongsToSelectedMonth { get; private set; }
        public bool IsSelected { get; private set; }
        public bool IsSunday { get; private set; }

        public WeekDay(int dayNumber, bool isSunday, bool isSelected, bool belongsToSelectedMonth)
        {
            Number = dayNumber;
            IsSunday = isSunday;
            IsSelected = isSelected;
            BelongsToSelectedMonth = belongsToSelectedMonth;
        }

        public override bool Equals(object obj)
        {
            var other = (WeekDay) obj;
            return
                Number == other.Number &&
                BelongsToSelectedMonth.Equals(other.BelongsToSelectedMonth) &&
                IsSelected.Equals(other.IsSelected) &&
                IsSunday.Equals(other.IsSunday);
        }

        public override int GetHashCode()
        {
            return
                Number*1000 +
                BelongsToSelectedMonth.GetHashCode()*100 +
                IsSelected.GetHashCode()*10 +
                IsSunday.GetHashCode();
        }
    }
}