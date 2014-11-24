using System.Drawing;

namespace Calendar
{
    class CalendarRender
    {
        private Calendar calendar;
        private float width;
        private float height;

        public CalendarRender(Calendar calendar)
        {
            this.calendar = calendar;
        }

        public void Draw(Graphics g, Size size)
        {
            width = size.Width;
            height = size.Height;
            var cellWidth = (float)width / (calendar.DistributionByDaysOfTheWeek.Length + 2);
            var cellHeight = (float)height / calendar.DistributionByDaysOfTheWeek[0].Length;

            var pen = new Pen(Color.Gray);
            for (var x = cellWidth; x < width; x += cellWidth)
            {
                g.DrawLine(pen, x, 0, x, height);
            }
            for (var y = cellHeight; y < height; y += cellHeight)
            {
                g.DrawLine(pen, 0, y, width, y);
            }

            DrawCalendarHeader(g, cellHeight);
        }

        private void DrawCalendarHeader(Graphics g, float cellHeight)
        {
            var stringFormat=new StringFormat {Alignment = StringAlignment.Center};
            g.DrawString(calendar.Date.ToShortDateString(),new Font("Arial", cellHeight/2),
                new SolidBrush(Color.Black), new RectangleF(0,0,width,cellHeight),stringFormat);
        }
    }
}
