using System.Drawing;

namespace Calendar.PageRenders
{
    interface IPageRender
    {
        Bitmap Draw(PageElement page);
    }
}