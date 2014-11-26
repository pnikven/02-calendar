using System;
using System.Drawing;
using System.Linq;

namespace Calendar
{
    class CalendarRender
    {
        private const float PointsPerInch = 72;
        private const string FontName = "Times";
        private const float TextPadding = 10f;
        private const int CalendarHeaderFieldsCount = 3;
        private const int MinTextWidthForCalendarValues = 2;

        private static readonly string[] MonthNames =
        {
            "", "Января", "Февраля", "Марта", "Апреля", "Мая", "Июня", 
            "Июля", "Августа", "Сентября", "Октября", "Ноября", "Декабря"
        };

        private static readonly string[] DaysOfTheWeekNames =
        {
            "ПН","ВТ","СР", "ЧТ","ПТ","СБ","ВС"
        };

        private static readonly StringFormat AlignCenter = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        private static readonly Color BackColor = Color.FromArgb(255, 242, 242, 242);
        private static readonly Color ForeColor = Color.FromArgb(255, 151, 151, 151);
        private static readonly Color WeekNumberColor = Color.FromArgb(255, 0, 149, 202);
        private static readonly Color SundayColor = Color.FromArgb(255, 255, 88, 88);
        private static readonly Color DateBackColor = Color.FromArgb(255, 185, 185, 245);

        private readonly Calendar calendar;
        private readonly Graphics graphics;
        private readonly SizeF size;
        private readonly SizeF cellSize;

        public CalendarRender(Calendar calendar, Graphics graphics, Size size)
        {
            this.calendar = calendar;
            this.graphics = graphics;
            this.size = size;
            cellSize = new SizeF(
                this.size.Width / Calendar.DistributionByDayOfWeekMatrixWidth,
                this.size.Height / (calendar.DistributionByDaysOfTheWeek.Length + CalendarHeaderFieldsCount));
        }

        public void Draw()
        {
            if (size.Width < 1 || size.Height < 1)
                return;
            DrawCalendarBackground(new RectangleF(new PointF(0, 0), size));
            DrawCalendarHeader(new PointF(0, 0));
            DrawCalendarContent(new PointF(0, cellSize.Height * CalendarHeaderFieldsCount));
        }

        private void DrawCalendarBackground(RectangleF calendarArea)
        {
            graphics.FillRectangle(new SolidBrush(BackColor), calendarArea);
        }

        private void DrawCalendarHeader(PointF origin)
        {
            var header = String.Format("{0} {1} {2} г.",
                calendar.Date.Day,MonthNames[calendar.Date.Month],calendar.Date.Year);
            DrawString(header, ForeColor, new RectangleF(origin, new SizeF(size.Width, cellSize.Height)));
            DrawString("#", ForeColor, new RectangleF(new PointF(origin.X, origin.Y + cellSize.Height * 2), cellSize));
            DrawDaysOfTheWeekHeader(new PointF(origin.X + cellSize.Width, origin.Y + cellSize.Height * 2));
        }

        private void DrawDaysOfTheWeekHeader(PointF origin)
        {
            var x = origin.X;
            for (var i = 0; i < DaysOfTheWeekNames.Length; i++, x += cellSize.Width)
                DrawString(DaysOfTheWeekNames[i], ForeColor, new RectangleF(x, origin.Y, cellSize.Width, cellSize.Height));
        }

        private void DrawCalendarContent(PointF origin)
        {
            DrawWeekNumbers(origin);
            graphics.FillEllipse(new SolidBrush(DateBackColor),
                new RectangleF(GetDateLocation(), cellSize));
            DrawDaysOfTheWeek(new PointF(origin.X + cellSize.Width, origin.Y));
        }

        private void DrawWeekNumbers(PointF origin)
        {
            var y = origin.Y;
            foreach (var weekNumber in calendar.DistributionByDaysOfTheWeek.Select(week => week[0].ToString()))
            {
                DrawString(weekNumber, MinTextWidthForCalendarValues, FontStyle.Italic,
                    WeekNumberColor, new RectangleF(new PointF(origin.X, y), cellSize));
                y += cellSize.Height;
            }
        }

        private PointF GetDateLocation()
        {
            return
                new PointF(calendar.DayLocation.Item2 * cellSize.Width,
                    (calendar.DayLocation.Item1 + CalendarHeaderFieldsCount) * cellSize.Height);
        }

        private void DrawDaysOfTheWeek(PointF origin)
        {
            var p = new PointF(origin.X, origin.Y);
            for (var i = 0; i < calendar.DistributionByDaysOfTheWeek.Length; i++, p.X = origin.X, p.Y += cellSize.Height)
                for (var j = 1; j < calendar.DistributionByDaysOfTheWeek[i].Length; j++, p.X += cellSize.Width)
                    DrawString(GetDayNumber(i, j), MinTextWidthForCalendarValues,
                        j % 7 == 0 ? SundayColor : ForeColor, new RectangleF(p, cellSize));
        }

        private string GetDayNumber(int week, int dayOfWeek)
        {
            var dayNumberValue = calendar.DistributionByDaysOfTheWeek[week][dayOfWeek];
            var dayNumber = dayNumberValue == 0 ? "" : dayNumberValue.ToString();
            return dayNumber;
        }

        private Font CreateFontThatFitToGivenSize(string text, SizeF sizeF, FontStyle fontStyle)
        {
            var font = new Font(FontName, PixelToPoint(graphics.DpiY, sizeF.Height), fontStyle);
            var actualTextWidth = graphics.MeasureString(text, font).Width + TextPadding;
            if (actualTextWidth <= sizeF.Width)
                return font;
            var scaleFactor = sizeF.Width / actualTextWidth;
            font = new Font(FontName, font.SizeInPoints * scaleFactor, fontStyle);
            return font;
        }

        private static float PixelToPoint(float resolution, float pixelCount)
        {
            var inchCount = pixelCount / resolution;
            var pointCount = inchCount * PointsPerInch;
            return pointCount;
        }

        private void DrawString(string text, Color color, RectangleF drawArea)
        {
            var font = CreateFontThatFitToGivenSize(text, drawArea.Size, FontStyle.Regular);
            graphics.DrawString(text, font, new SolidBrush(color), drawArea, AlignCenter);
        }

        private void DrawString(string text, int minTextWidth, Color color, RectangleF drawArea)
        {
            var font = CreateFontThatFitToGivenSize(text.PadLeft(minTextWidth, '0'), drawArea.Size, FontStyle.Regular);
            graphics.DrawString(text, font, new SolidBrush(color), drawArea, AlignCenter);
        }

        private void DrawString(string text, int minTextWidth, FontStyle style, Color color, RectangleF drawArea)
        {
            var font = CreateFontThatFitToGivenSize(text.PadLeft(minTextWidth, '0'), drawArea.Size, style);
            graphics.DrawString(text, font, new SolidBrush(color), drawArea, AlignCenter);
        }
    }
}
