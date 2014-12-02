using System;
using System.Drawing;
using System.Globalization;

namespace Calendar.PageFactories
{
    class MonthAndYearHeaderPageFactory : IDatePageFactory
    {
        private static readonly Color HeaderForeColor = Color.FromArgb(255, 0, 102, 204);

        public PageElement Create(DateTime date)
        {
            return new PageElement(PageElementType.Block)
            {
                Text = String.Format("{0} {1}", 
                    DateTimeFormatInfo.InvariantInfo.GetMonthName(date.Month), date.Year),
                ForegroundColor = HeaderForeColor
            };
        }
    }
}