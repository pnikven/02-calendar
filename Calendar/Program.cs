using System;
using System.Collections.Generic;
using System.Drawing;

namespace Calendar
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = GetFirstParameter(args);
            var calendar = new Calendar(date);
            var outputCalendarImageFilename = String.Format("Calendar for {0}.bmp",calendar.Date.ToShortDateString());
            CalendarGenerator.GenerateCalendarImage(calendar, new Size(300, 300), outputCalendarImageFilename);
        }

        private static string GetFirstParameter(IList<string> consoleArguments)
        {
            if (consoleArguments.Count != 0) return consoleArguments[0];
            throw new Exception("Date in format dd.mm.yyyy must be provided");
        }
    }
}
