using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendar.PageFactories
{
    class CalendarPageFactory : IDatePageFactory
    {
        private readonly IDatePageFactory _monthAndYearHeaderFactory;
        private readonly IPageFactory _daysOfWeekHeaderPageFactory;
        private readonly IWeekPageFactory _weekPageFactory;

        public CalendarPageFactory()
        {
            _monthAndYearHeaderFactory=new MonthAndYearHeaderPageFactory();
            _daysOfWeekHeaderPageFactory = new DaysOfWeekHeaderPageFactory();
            _weekPageFactory = new WeekPageFactory();
        }

        public PageElement Create(DateTime date)
        {
            var page = new PageElement(PageElementType.Root);
            page.AddChild(_monthAndYearHeaderFactory.Create(date));
            page.AddChild(_daysOfWeekHeaderPageFactory.Create());

            var dataProvider = new CalendarPageDataProvider(date);
            IEnumerable<Week> weeks = dataProvider.Weeks;

            page.AddChildRange(weeks
                .Select(week => _weekPageFactory.Create(week)));
            return page;
        }
    }
}