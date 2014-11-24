using System.Drawing;
using System.Windows.Forms;

namespace Calendar
{
    class CalendarRepresentation : Form
    {
        public CalendarRepresentation()
        {
            ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var pen = new Pen(Color.Black);
            graphics.DrawLine(pen, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
            graphics.DrawLine(pen, 0, ClientSize.Height - 1, ClientSize.Width - 1, 0);
        }
    }
}
