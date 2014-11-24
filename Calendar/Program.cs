using System;

namespace Calendar
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = GetFirstParameter(args);
        }

        private static string GetFirstParameter(string[] consoleArguments)
        {
            if (consoleArguments.Length != 0) return consoleArguments[0];
            throw new Exception("Date in format dd.mm.yyyy must be provided");
        }
    }
}
