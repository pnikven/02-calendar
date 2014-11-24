using System;
using System.Collections.Generic;

namespace Calendar
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = GetFirstParameter(args);
        }

        private static string GetFirstParameter(IList<string> consoleArguments)
        {
            if (consoleArguments.Count != 0) return consoleArguments[0];
            throw new Exception("Date in format dd.mm.yyyy must be provided");
        }
    }
}
