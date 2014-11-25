using System;
using System.Collections.Generic;
using System.Drawing;

namespace Calendar
{
    class CalendarRender
    {
        private const float PointsPerInch = 72;

        private readonly Calendar calendar;
        private float width;
        private float height;

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

        public CalendarRender(Calendar calendar)
        {
            this.calendar = calendar;
        }

        public void Draw(Graphics g, Size size)
        {
            width = size.Width;
            height = size.Height;
            if (height < 1) return;

            var cellWidth = (float)width / (calendar.DistributionByDaysOfTheWeek.Length + 2);
            var cellHeight = (float)height / calendar.DistributionByDaysOfTheWeek[0].Length;

            DrawGrid(g, cellWidth, cellHeight);
            DrawCalendarHeader(g, cellHeight);
        }

        private void DrawGrid(Graphics g, float cellWidth, float cellHeight)
        {
            var pen = new Pen(Color.Gray);
            for (var x = cellWidth; x < width; x += cellWidth)
            {
                g.DrawLine(pen, x, 0, x, height);
            }
            for (var y = cellHeight; y < height; y += cellHeight)
            {
                g.DrawLine(pen, 0, y, width, y);
            }
        }

        private void DrawCalendarHeader(Graphics g, float cellHeight)
        {
            var stringFormat = new StringFormat { Alignment = StringAlignment.Center };
            var font = new Font("Arial", PixelToPoint(g.DpiY, cellHeight));
            var header = CreateCalendarHeader(calendar.Date);
            var brush = new SolidBrush(Color.Black);
            g.DrawString(header, font, brush, new RectangleF(0, 0, width, cellHeight), stringFormat);
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
