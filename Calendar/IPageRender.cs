using System.Drawing;

namespace Calendar
{
    interface IPageRender
    {
        Bitmap Draw(PageElement page);
    }
}