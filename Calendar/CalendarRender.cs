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
        private static readonly Dictionary<int, string> MonthNames = new Dictionary<int, string>()
        {
            {1, "Январь"},
            {2, "Февраль"},
            {3, "Март"},
            {4, "Апрель"},
            {5, "Май"},
            {6, "Июнь"},
            {7, "Июль"},
            {8, "Август"},
            {9, "Сентябрь"},
            {10, "Октябрь"},
            {11, "Ноябрь"},
            {12, "Декабрь"}
        };
        private static readonly Color TextColor = Color.FromArgb(255, 169, 169, 169);

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
            DrawGrid(graphics, cellWidth, cellHeight);
            DrawCalendarHeader(graphics, new SizeF(width, cellHeight));
        }

        private void DrawGrid(Graphics g, float cellWidth, float cellHeight)
        {
            var pen = new Pen(Color.Gray);
            for (var x = cellWidth; x < width; x += cellWidth)
                g.DrawLine(pen, x, 0, x, height);
            for (var y = cellHeight; y < height; y += cellHeight)
                g.DrawLine(pen, 0, y, width, y);
        }

        private void DrawCalendarHeader(Graphics g, SizeF headerSizeF)
        {
            var header = CreateCalendarHeader(calendar.Date);
            var font = CreateFont(g, headerSizeF, header);
            var brush = new SolidBrush(TextColor);
            var stringFormat = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(header, font, brush, new RectangleF(0, 0, width, headerSizeF.Height), stringFormat);
        }

        private static Font CreateFont(Graphics g, SizeF sizeF, string text)
        {
            var font = new Font(FontName, PixelToPoint(g.DpiY, sizeF.Height));
            var actualTextWidth = g.MeasureString(text, font).Width + TextPadding;
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
