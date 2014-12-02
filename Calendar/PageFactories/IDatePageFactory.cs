using System;

namespace Calendar.PageFactories
{
    interface IDatePageFactory
    {
        PageElement Create(DateTime date);
    }
}