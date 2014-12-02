using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace Calendar
{
    class CalendarPageDataFactory : IPageDataFactory
    {
        private static readonly Color BackColor = Color.FromArgb(255, 242, 242, 242);
        private static readonly Color ForeColor = Color.FromArgb(255, 100, 100, 100);
        private static readonly Color OtherMonthDayColor = Color.FromArgb(255, 200, 200, 200);
        private static readonly Color SundayColor = Color.FromArgb(255, 255, 88, 88);
        private static readonly Color SelectedDateBackColor = Color.FromArgb(255, 185, 185, 245);

        public PageElement Create(DateTime date)
        {
            var page = new PageElement(PageElementType.Root);
            page.AddChild(BuildMonthAndYearHeader(date));
            page.AddChild(BuildDaysOfWeekHeader());
            page.AddChildRange(BuildWeeksGrid(date));
            return page;
        }

        private PageElement BuildMonthAndYearHeader(DateTime date)
        {
            return new PageElement(PageElementType.Block)
            {
                Text = String.Format("{0} {1}", DateTimeFormatInfo.InvariantInfo.GetMonthName(date.Month), date.Year),
                ForegroundColor = ForeColor
            };
        }

        private PageElement BuildDaysOfWeekHeader()
        {
            var header = new PageElement(PageElementType.Block);
            header.AddChildRange(Enumerable.Range(0, 7)
                    .Select(i => new PageElement(PageElementType.Inline)
                    {
                        Text = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedDayName((DayOfWeek)i),
                        ForegroundColor = ForeColor
                    }));
            return header;
        }

        private IEnumerable<PageElement> BuildWeeksGrid(DateTime date)
        {
            IEnumerable<Week> weeks = CalculateCalendarPageWeeks(date);
            return weeks
                .Select(BuildWeekRow);
        }

        private PageElement BuildWeekRow(Week week)
        {
            var weekRow = new PageElement(PageElementType.Block);
            weekRow.AddChildRange(week.WeekDays
                .Select(weekDay => new PageElement(PageElementType.Inline)
                    {
                        Text = weekDay.Number.ToString(CultureInfo.InvariantCulture),
                        ForegroundColor = GetColorForWeekDay(weekDay),
                        BackgroundColor = weekDay.IsSelected ? SelectedDateBackColor : BackColor
                    }));
            return weekRow;
        }

        private Color GetColorForWeekDay(WeekDay weekDay)
        {
            if (weekDay.BelongsToSelectedMonth) return OtherMonthDayColor;
            return weekDay.IsSunday ? SundayColor : ForeColor;
        }

        private IEnumerable<Week> CalculateCalendarPageWeeks(DateTime selectedDate)
        {
            var weeks = new List<Week>();
            DateTime firstDayOfWeek = selectedDate.FirstDayOfMonth().FirstDayOfWeek();
            do
            {
                weeks.Add(new Week(firstDayOfWeek, selectedDate));
                firstDayOfWeek = firstDayOfWeek.AddDays(7);
            } while (firstDayOfWeek.Month == selectedDate.Month);
            return weeks;
        }
    }

    class Week
    {
        public WeekDay[] WeekDays { get; private set; }

        public Week(DateTime firstDayOfWeek, DateTime selectedDate)
        {
            WeekDays = Enumerable.Range(0, 7)
                .Select(i => firstDayOfWeek.AddDays(i))
                .Select(date => new WeekDay(date, selectedDate))
                .ToArray();
        }
    }

    class WeekDay
    {
        public int Number { get; private set; }
        public bool BelongsToSelectedMonth { get; private set; }
        public bool IsSelected { get; private set; }
        public bool IsSunday { get; private set; }

        public WeekDay(DateTime date, DateTime selectedDate)
        {
            Number = date.Day;
            BelongsToSelectedMonth = date.Month != selectedDate.Month;
            IsSelected = date == selectedDate;
            IsSunday = date.DayOfWeek == DayOfWeek.Sunday;
        }
    }
}