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
            var page = new RootElement(BackColor);
            page.AddChildElement(new TextElement(
                String.Format("{0} {1}", DateTimeFormatInfo.InvariantInfo.GetMonthName(date.Month), date.Year),
                ForeColor));
            page.AddChildElement(BuildCalendarPageDataGridHeader());
            IEnumerable<Week> weeks = CalculateCalendarPageWeeks(date);
            page.AddChildElement(BuildCalendarPageDataGrid(weeks));
            return page;
        }

        private PageElement BuildCalendarPageDataGridHeader()
        {
            var row=new RowElement();            
            foreach (var dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
            {
                row.AddChildElement(
                    new CellElement(DateTimeFormatInfo.InvariantInfo.GetAbbreviatedDayName((DayOfWeek)dayOfWeek), 
                        ForeColor, row, false));
            }
            var table=new TableElement();
            table.AddChildElement(row);
            return table;
        }

        private TableElement BuildCalendarPageDataGrid(IEnumerable<Week> weeks)
        {
            var tableElement = new TableElement();
            foreach (var week in weeks)
            {
                PageElement rowElement = new RowElement();
                foreach (var weekDay in week.WeekDays)
                {
                    rowElement.AddChildElement(new CellElement(weekDay.Number.ToString(),
                        GetColorForWeekDay(weekDay),
                        rowElement, weekDay.IsSelected));
                }
                tableElement.AddChildElement(rowElement);
            }
            return tableElement;
        }

        private Color GetColorForWeekDay(WeekDay weekDay)
        {
            if (weekDay.BelongsToSelectedMonth) return OtherMonthDayColor;
            if (weekDay.IsSunday) return SundayColor;
            return ForeColor;
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