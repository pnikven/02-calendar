using System;
using System.Drawing;

namespace Calendar
{
    class CalendarRender
    {
        private const float PointsPerInch = 72;
        private const string FontName = "Times";
        private const float TextPadding = 10f;
        private const int CalendarHeaderAdditionalFieldsCount = 2;

        private static readonly string[] MonthNames =
        {
            "", "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };

        private static readonly string[] DaysOfTheWeekNames =
        {
            "ПН","ВТ","СР", "ЧТ","ПТ","СБ","ВС",
        };

        private static readonly StringFormat AlignCenter = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        private static readonly Color ForeColor = Color.FromArgb(255, 169, 169, 169);
        private static readonly Brush StandardBrush = new SolidBrush(ForeColor);

        private readonly Calendar calendar;
        private readonly Graphics graphics;
        private readonly float width;
        private readonly float height;
        private readonly SizeF cellSize;

        public CalendarRender(Calendar calendar, Graphics graphics, Size size)
        {
            this.calendar = calendar;
            this.graphics = graphics;
            width = size.Width;
            height = size.Height;
            cellSize = new SizeF(
                width / (calendar.DistributionByDaysOfTheWeek.Length + CalendarHeaderAdditionalFieldsCount),
                height / Calendar.DistributionByDayOfWeekMatrixWidth);
        }

        public void Draw()
        {
            if (height < 1)
                return;
            DrawGrid();
            DrawCalendarHeader();
        }

        private void DrawCalendarHeader()
        {
            DrawCalendarCaption(new PointF(0, 0), new SizeF(width, cellSize.Height));
            DrawWeekNumberHeader(new PointF(0, cellSize.Height));
            DrawDaysOfTheWeekHeader(new PointF(cellSize.Width, cellSize.Height));
        }

        private void DrawWeekNumberHeader(PointF origin)
        {
            const string header = "#";
            var font = CreateFont(header, cellSize);
            graphics.DrawString(header, font, StandardBrush, new RectangleF(origin, cellSize), AlignCenter);
        }

        private void DrawDaysOfTheWeekHeader(PointF origin)
        {
            var x = origin.X;
            foreach (var day in DaysOfTheWeekNames)
            {
                var font = CreateFont(day, cellSize);
                graphics.DrawString(day, font, StandardBrush, 
                    new RectangleF(x, origin.Y, cellSize.Width, cellSize.Height), AlignCenter);
                x += cellSize.Width;
            }
        }

        private void DrawGrid()
        {
            var pen = new Pen(Color.Gray);
            for (var x = cellSize.Width; x < width; x += cellSize.Width)
                graphics.DrawLine(pen, x, 0, x, height);
            for (var y = cellSize.Height; y < height; y += cellSize.Height)
                graphics.DrawLine(pen, 0, y, width, y);
        }

        private void DrawCalendarCaption(PointF origin, SizeF headerSize)
        {
            var header = CreateCalendarCaption(calendar.Date);
            var font = CreateFont(header, headerSize);
            graphics.DrawString(header, font, StandardBrush,
                new RectangleF(origin.X, origin.Y, headerSize.Width, headerSize.Height), AlignCenter);
        }

        private Font CreateFont(string text, SizeF sizeF)
        {
            var font = new Font(FontName, PixelToPoint(graphics.DpiY, sizeF.Height));
            var actualTextWidth = graphics.MeasureString(text, font).Width + TextPadding;
            if (actualTextWidth <= sizeF.Width)
                return font;
            var scaleFactor = sizeF.Width / actualTextWidth;
            font = new Font(FontName, font.SizeInPoints * scaleFactor);
            return font;
        }

        private static string CreateCalendarCaption(DateTime date)
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
