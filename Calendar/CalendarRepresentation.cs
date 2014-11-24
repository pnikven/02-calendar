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
            BackColor = Color.FromArgb(255, 242, 242, 242); 
            ResizeRedraw = true;
        }

        public override sealed Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            calendarRender.Draw(e.Graphics,ClientSize);
        }
    }
}
