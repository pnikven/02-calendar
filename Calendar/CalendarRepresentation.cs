using System.Drawing;
using System.Windows.Forms;

namespace Calendar
{
    class CalendarRepresentation : Form
    {
        private readonly Calendar calendar;

        public CalendarRepresentation(Calendar calendar)
        {
            this.calendar = calendar;
            BackColor = Color.FromArgb(255, 242, 242, 242); 
            ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var calendarRender = new CalendarRender(calendar, e.Graphics, ClientSize);
            calendarRender.Draw();
        }
    }
}
