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
        private static readonly Color ForeColor = Color.FromArgb(255, 151, 151, 151);
        private static readonly Color WeekNumberColor = Color.FromArgb(255, 0, 149, 202);
        private static readonly Color SundayColor = Color.FromArgb(255, 255, 88, 88);
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
            DrawCalendarHeader(new PointF(0, 0));
            DrawCalendarContent(new PointF(0, cellSize.Height * CalendarHeaderAdditionalFieldsCount));
        }

        private void DrawCalendarContent(PointF origin)
        {
            DrawWeekNumbers(origin);
            DrawDaysOfTheWeek(new PointF(origin.X + cellSize.Width, origin.Y));
        }

        private void DrawDaysOfTheWeek(PointF origin)
        {
            var p = new PointF(origin.X, origin.Y);
            for (var i = 0; i < calendar.DistributionByDaysOfTheWeek.Length; i++, p.X = origin.X, p.Y += cellSize.Height)
                for (var j = 1; j < calendar.DistributionByDaysOfTheWeek[i].Length; j++, p.X += cellSize.Width)
                {
                    var dayNumber = GetDayNumber(i, j);
                    graphics.DrawString(dayNumber, CreateFont(dayNumber.PadLeft(2, '0'), cellSize),
                        new SolidBrush(j % 7 == 0 ? SundayColor : ForeColor), new RectangleF(p, cellSize), AlignCenter);
                }
        }

        private string GetDayNumber(int week, int dayOfWeek)
        {
            var dayNumberValue = calendar.DistributionByDaysOfTheWeek[week][dayOfWeek];
            var dayNumber = dayNumberValue == 0 ? "" : dayNumberValue.ToString();
            return dayNumber;
        }

        private void DrawWeekNumbers(PointF origin)
        {
            var y = origin.Y;
            foreach (var weekNumber in calendar.DistributionByDaysOfTheWeek.Select(week => week[0].ToString()))
            {
                graphics.DrawString(weekNumber, CreateFont(weekNumber.PadLeft(2, '0'), cellSize), 
                    new SolidBrush(WeekNumberColor),
                    new RectangleF(new PointF(origin.X, y), cellSize), AlignCenter);
                y += cellSize.Height;
            }
        }

        private void DrawCalendarHeader(PointF origin)
        {
            DrawCalendarCaption(new PointF(origin.X, origin.Y), new SizeF(width, cellSize.Height));
            DrawWeekNumbersHeader(new PointF(origin.X, origin.Y + cellSize.Height));
            DrawDaysOfTheWeekHeader(new PointF(origin.X + cellSize.Width, origin.Y + cellSize.Height));
        }

        private void DrawWeekNumbersHeader(PointF origin)
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
