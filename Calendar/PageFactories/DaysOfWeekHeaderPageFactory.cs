using System;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace Calendar.PageFactories
{
    class DaysOfWeekHeaderPageFactory : IPageFactory
    {
        private static readonly Color ForeColor = Color.FromArgb(255, 0, 0, 0);

        public PageElement Create()
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
    }
}