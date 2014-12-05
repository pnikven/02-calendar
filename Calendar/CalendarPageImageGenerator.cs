using System;
using System.Drawing;
using Calendar.PageFactories;
using Calendar.PageRenders;

namespace Calendar
{
    class CalendarPageImageGenerator
    {
        private readonly IPageRender _pageRender;
        private readonly IDatePageFactory _datePageFactory;

        public CalendarPageImageGenerator(IDatePageFactory datePageFactory, IPageRender pageRender)
        {
            _datePageFactory = datePageFactory;
            _pageRender = pageRender;
        }

        public Bitmap GenerateCalendarPageImageByDate(DateTime date)
        {
            PageElement page = _datePageFactory.Create(date);
            Bitmap pageImage = _pageRender.Draw(page);
            return pageImage;
        }
    }
}
