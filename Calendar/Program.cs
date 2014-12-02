using System;
using System.Drawing;
using System.Globalization;
using Calendar.PageFactories;
using Calendar.PageRenders;

namespace Calendar
{
    class Program
    {
        private readonly string inputDate;

        private IPageRender _pageRender;
        private IDatePageFactory _datePageFactory;

        private void SetPageRander(IPageRender pageRender)
        {
            _pageRender = pageRender;
        }

        private void SetPageFactory(IDatePageFactory datePageFactory)
        {
            _datePageFactory = datePageFactory;
        }

        static void Main(string[] args)
        {
            try
            {
                var program = new Program(args);
                program.SetPageRander(new PageRender());
                program.SetPageFactory(new CalendarPageFactory());
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

            PageElement page = _datePageFactory.Create(date);
            Bitmap pageImage = _pageRender.Draw(page);

            var outputPageImageFilename = String.Format("Calendar Page for {0}.{1}.{2}.bmp",
                date.Day, date.Month, date.Year);
            pageImage.Save(outputPageImageFilename);
        }
    }
}
