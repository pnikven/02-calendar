using System.Drawing;
using System.Globalization;
using System.Linq;

namespace Calendar.PageFactories
{
    class WeekPageFactory : IWeekPageFactory
    {
        private static readonly Color BackColor = Color.FromArgb(255, 242, 242, 242);
        private static readonly Color ForeColor = Color.FromArgb(255, 100, 100, 100);
        private static readonly Color OtherMonthDayColor = Color.FromArgb(255, 200, 200, 200);
        private static readonly Color SundayColor = Color.FromArgb(255, 255, 88, 88);
        private static readonly Color SelectedDateBackColor = Color.FromArgb(255, 185, 185, 245);

        public PageElement Create(Week week)
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
            if (!weekDay.BelongsToSelectedMonth) return OtherMonthDayColor;
            return weekDay.IsSunday ? SundayColor : ForeColor;
        }
    }
}