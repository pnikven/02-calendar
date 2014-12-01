using System;

namespace Calendar
{
    interface IPageDataFactory
    {
        PageElement Create(DateTime datetime);
    }
}