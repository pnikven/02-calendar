using System;
using System.Drawing;

namespace Calendar
{
    class SimplePageRender : IPageRender
    {
        private const float PointsPerInch = 72;
        private const string FontName = "Times";
        private const int PageWidth = 300;
        private const int PageHeight = 300;

        private readonly StringFormat _alignCenter = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        private readonly Size pageSize = new Size(PageWidth, PageHeight);

        private Graphics graphics;

        public Bitmap Draw(PageElement page)
        {
            var pageImage = new Bitmap(pageSize.Width, pageSize.Height);
            graphics = Graphics.FromImage(pageImage);
            DrawElement(page, new PointF(0, 0), pageSize);
            return pageImage;
        }

        private void DrawElement(PageElement pageElement, PointF origin, SizeF size)
        {
            DrawPageBackground(new RectangleF(origin, size), pageElement.BackgroundColor);
            if (pageElement.HasText())
                DrawString(pageElement.Text, pageElement.ForegroundColor, new RectangleF(origin, size));
            DrawChildren(pageElement, origin, size);
        }

        private void DrawChildren(PageElement pageElement, PointF origin, SizeF parentSize)
        {
            var x = origin.X;
            var y = origin.Y;
            foreach (PageElement child in pageElement.GetChildren())
            {
                SizeF relativeSize = child.ConsumedSizeRelativeToParent();
                var absoluteSize = new SizeF(
                    parentSize.Width * relativeSize.Width, 
                    parentSize.Height * relativeSize.Height);
                DrawElement(child, new PointF(x, y), absoluteSize);
                if (child.ElementType == PageElementType.Block)
                    y += absoluteSize.Height;
                else if (child.ElementType == PageElementType.Inline)
                    x += absoluteSize.Width;
            }
        }

        private void DrawPageBackground(RectangleF pageArea, Color color)
        {
            graphics.FillRectangle(new SolidBrush(color), pageArea);
        }

        private Font CreateFontThatFitToGivenSize(string text, SizeF sizeF)
        {
            var font = new Font(FontName, PixelToPoint(graphics.DpiY, sizeF.Height));
            var actualTextSize = graphics.MeasureString(text, font);
            if (actualTextSize.Width < sizeF.Width && actualTextSize.Height <= sizeF.Height)
                return font;
            var scaleFactor = Math.Min(sizeF.Width / actualTextSize.Width, sizeF.Height / actualTextSize.Height);
            font = new Font(FontName, font.SizeInPoints * scaleFactor);
            return font;
        }

        private float PixelToPoint(float resolution, float pixelCount)
        {
            var inchCount = pixelCount / resolution;
            var pointCount = inchCount * PointsPerInch;
            return pointCount;
        }

        private void DrawString(string text, Color color, RectangleF drawArea)
        {
            var font = CreateFontThatFitToGivenSize(text, drawArea.Size);
            graphics.DrawString(text, font, new SolidBrush(color), drawArea, _alignCenter);
        }
    }
}
