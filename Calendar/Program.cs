using System;
using System.Collections.Generic;
using System.Drawing;

namespace Calendar
{
    class Program
    {
        private static readonly Size DefaultSize = new Size(300, 300);

        static void Main(string[] args)
        {
            var date = GetFirstParameter(args);
            var size = InitSize(args);
            var calendar = new Calendar(date);
            var outputCalendarImageFilename = String.Format("Calendar for {0}.bmp", calendar.Date.ToShortDateString());
            CalendarGenerator.GenerateCalendarImage(calendar, size, outputCalendarImageFilename);
        }

        private static string GetFirstParameter(IList<string> consoleArguments)
        {
            if (consoleArguments.Count != 0) return consoleArguments[0];
            throw new Exception("Date in format dd.mm.yyyy must be provided");
        }

        private static Size InitSize(string[] consoleArguments)
        {
            if (consoleArguments.Length > 1)
                try
                {
                    return new Size(int.Parse(consoleArguments[1]), int.Parse(consoleArguments[2]));
                }
                catch
                {
                    Console.WriteLine("Using: Calendar.exe dd.mm.yyyy [width height]");
                }
            return DefaultSize;
        }
    }
}
