using System;
using System.Drawing;
using System.Globalization;

namespace Calendar
{
    class Program
    {
        private readonly string inputDate;

        static void Main(string[] args)
        {
            try
            {
                var program = new Program(args);
                program.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private Program(string[] args)
        {
            if (args.Length == 0)
                throw new Exception("Using: Calendar.exe dd.mm.yyyy");
            inputDate = args[0];
        }

        private void Start()
        {
            var culture = new CultureInfo("ru-RU");
            var date = DateTime.Parse(inputDate, culture);

            var calendarPage = new CalendarPage(date);
            var calendarRender = new CalendarPageRender(calendarPage);
            Bitmap calendarPageImage = calendarRender.Draw();

            var outputCalendarPageImageFilename = String.Format("CalendarPage for {0}.{1}.{2}.bmp",
                date.Day, date.Month, date.Year);
            calendarPageImage.Save(outputCalendarPageImageFilename);
        }
    }
}
