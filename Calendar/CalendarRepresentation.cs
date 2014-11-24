using System.Drawing;
using System.Windows.Forms;

namespace Calendar
{
    class CalendarRepresentation : Form
    {
        private readonly CalendarRender calendarRender;

        public CalendarRepresentation(Calendar calendar)
        {
            calendarRender=new CalendarRender(calendar);
            ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            calendarRender.Draw(e.Graphics,ClientSize);
        }
    }
}
