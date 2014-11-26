using System.Drawing;

namespace Calendar
{
    class CalendarGenerator
    {
        public static void GenerateCalendarImage(Calendar calendar, Size imageSize, string filename)
        {
            var calendarImage = new Bitmap(imageSize.Width, imageSize.Height);
            var calendarRender = new CalendarRender(calendar, Graphics.FromImage(calendarImage), imageSize);
            calendarRender.Draw();
            calendarImage.Save(filename);
        }
    }
}
