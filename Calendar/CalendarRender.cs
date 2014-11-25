using System;
using System.Collections.Generic;
using System.Drawing;

namespace Calendar
{
    class CalendarRender
    {
        private const float PointsPerInch = 72;
        private const string FontName = "Arial";
        private const float TextPadding = 10f;

        private readonly Calendar calendar;
        private readonly Graphics graphics;
        private readonly float width;
        private readonly float height;

        private static readonly string[] MonthNames =
        {
            "", "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };

        private static readonly string[] DaysOfTheWeekNames =
        {
            "ПН","ВТ","СР", "ЧТ","ПТ","СБ","ВС",
        };

        private readonly StringFormat stringFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        private static readonly Color ForeColor = Color.FromArgb(255, 169, 169, 169);

        public CalendarRender(Calendar calendar, Graphics graphics, Size size)
        {
            this.calendar = calendar;
            this.graphics = graphics;
            width = size.Width;
            height = size.Height;
        }

        public void Draw()
        {
            if (height < 1)
                return;
            var cellWidth = width / (calendar.DistributionByDaysOfTheWeek.Length + 2);
            var cellHeight = height / calendar.DistributionByDaysOfTheWeek[0].Length;
            var cellSize = new SizeF(cellWidth, cellHeight);
            DrawGrid(cellSize);
            DrawCalendarHeader(new SizeF(width, cellHeight));
            DrawDaysOfTheWeekHeader(new PointF(cellWidth, cellHeight), cellSize);
        }

        private void DrawDaysOfTheWeekHeader(PointF origin, SizeF cellSize)
        {
            var x = origin.X;
            var y = origin.Y;
            foreach (var day in DaysOfTheWeekNames)
            {
                var font = CreateFont(cellSize, day);
                var brush = new SolidBrush(ForeColor);
                graphics.DrawString(day, font, brush, new RectangleF(x, y, cellSize.Width, cellSize.Height), stringFormat);
                x += cellSize.Width;
            }
        }

        private void DrawGrid(SizeF sizeF)
        {
            var pen = new Pen(Color.Gray);
            for (var x = sizeF.Width; x < width; x += sizeF.Width)
                graphics.DrawLine(pen, x, 0, x, height);
            for (var y = sizeF.Height; y < height; y += sizeF.Height)
                graphics.DrawLine(pen, 0, y, width, y);
        }

        private void DrawCalendarHeader(SizeF headerSizeF)
        {
            var header = CreateCalendarHeader(calendar.Date);
            var font = CreateFont(headerSizeF, header);
            var brush = new SolidBrush(ForeColor);
            graphics.DrawString(header, font, brush, new RectangleF(0, 0, width, headerSizeF.Height), stringFormat);
        }

        private Font CreateFont(SizeF sizeF, string text)
        {
            var font = new Font(FontName, PixelToPoint(graphics.DpiY, sizeF.Height));
            var actualTextWidth = graphics.MeasureString(text, font).Width + TextPadding;
            if (actualTextWidth <= sizeF.Width)
                return font;
            var scaleFactor = sizeF.Width / actualTextWidth;
            font = new Font(FontName, font.SizeInPoints * scaleFactor);
            return font;
        }

        private static string CreateCalendarHeader(DateTime date)
        {
            return MonthNames[date.Month] + " " + date.Year;
        }

        private static float PixelToPoint(float resolution, float pixelCount)
        {
            var inchCount = pixelCount / resolution;
            var pointCount = inchCount * PointsPerInch;
            return pointCount;
        }
    }
}
