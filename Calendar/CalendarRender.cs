using System.Drawing;

namespace Calendar
{
    class CalendarRender
    {
        private Calendar calendar;

        public CalendarRender(Calendar calendar)
        {
            this.calendar = calendar;
        }

        public void Draw(Graphics g, Size size)
        {
            var pen = new Pen(Color.Black);
            g.DrawLine(pen, 0, 0, size.Width - 1, size.Height - 1);
            g.DrawLine(pen, 0, size.Height - 1, size.Width - 1, 0);            
        }
    }
}
