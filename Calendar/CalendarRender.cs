using System.Drawing;

namespace Calendar
{
    class CalendarRender
    {
        private Calendar calendar;
        private float CellHeight;
        private float CellWidth;

        public CalendarRender(Calendar calendar)
        {
            this.calendar = calendar;
            CellHeight = 1f / (calendar.DistributionByDaysOfTheWeek.Length + 2);
            CellWidth = 1f / calendar.DistributionByDaysOfTheWeek[0].Length;
        }

        public void Draw(Graphics g, Size size)
        {
            var Width = size.Width;
            var Height = size.Height;
            var pen = new Pen(Color.Black);
            for (var x = CellWidth; x < 1; x += CellWidth)
            {
                g.DrawLine(pen, x * Width, 0, x * Width, Height);
            }
            for (var y = CellHeight; y < 1; y += CellHeight)
            {
                g.DrawLine(pen, 0, y * Height, Width, y * Height);
            }
        }
    }
}
