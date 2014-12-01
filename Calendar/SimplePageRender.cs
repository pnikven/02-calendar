using System;
using System.Drawing;

namespace Calendar
{
    class SimplePageRender : IPageRender
    {
        private const float PointsPerInch = 72;
        private const string FontName = "Times";
        private const float TextPadding = 10f;
        private const int MinTextWidthForCellElement = 2;
        private const int PageWidth = 300;
        private const int PageHeight = 300;

        private readonly StringFormat _alignCenter = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        private readonly Size size = new Size(PageWidth, PageHeight);

        private SizeF rowSize;
        private Graphics graphics;

        private int GetPageElementRowCount(PageElement pageElement)
        {
            var result = 0;
            foreach (var child in pageElement.GetChildren())
            {
                if (child.GetType() == typeof(TableElement))
                    result += child.GetChildren().Count;
                else
                    result++;
            }
            return result;
        }

        public Bitmap Draw(PageElement page)
        {
            rowSize = new SizeF(size.Width, (float)size.Height / GetPageElementRowCount(page));
            var pageImage = new Bitmap(size.Width, size.Height);
            graphics = Graphics.FromImage(pageImage);
            DrawElement(page, new PointF(0, 0));
            return pageImage;
        }

        private void DrawElement(PageElement pageElement, PointF origin)
        {
            if (pageElement.GetType() == typeof (RootElement))
            {
                DrawPageBackground(new RectangleF(new PointF(0, 0), size),
                    ((RootElement)pageElement).BackgroundColor);
            }
            if (pageElement.GetType() == typeof(TextElement))
            {
                DrawString(((TextElement)pageElement).Text, ((TextElement)pageElement).TextColor,
                    new RectangleF(origin, rowSize));
            }
            else if (pageElement.GetType() == typeof(CellElement))
            {
                DrawString(((CellElement)pageElement).Text, MinTextWidthForCellElement, 
                    ((CellElement)pageElement).TextColor,
                    new RectangleF(origin, new SizeF(
                        (float)size.Width / ((CellElement)pageElement).Parent.GetChildren().Count, rowSize.Height)));
            }
            else
            {
                var x = origin.X;
                var y = origin.Y;
                foreach (PageElement child in pageElement.GetChildren())
                {
                    DrawElement(child, new PointF(x, y));
                    x += GetElementSize(child).Width;
                    y += GetElementSize(child).Height;
                }
            }
        }

        private SizeF GetElementSize(PageElement pageElement)
        {
            if (pageElement.GetType() == typeof(RowElement))
            {
                return new SizeF(0, rowSize.Height);
            }
            if (pageElement.GetType() == typeof(TextElement))
            {
                return new SizeF(0, rowSize.Height);
            }
            if (pageElement.GetType() == typeof(CellElement))
            {
                return new SizeF((float)size.Width / ((CellElement)pageElement).Parent.GetChildren().Count, 0);
            }
            if (pageElement.GetType() == typeof(TableElement))
            {
                return new SizeF(0, rowSize.Height * pageElement.GetChildren().Count);
            }
            throw new Exception("PageElement type " + pageElement.GetType() + " is not recognized");
        }

        private void DrawPageBackground(RectangleF pageArea, Color color)
        {
            graphics.FillRectangle(new SolidBrush(color), pageArea);
        }

        private Font CreateFontThatFitToGivenSize(string text, SizeF sizeF, FontStyle fontStyle)
        {
            var font = new Font(FontName, PixelToPoint(graphics.DpiY, sizeF.Height), fontStyle);
            var actualTextWidth = graphics.MeasureString(text, font).Width + TextPadding;
            if (actualTextWidth <= sizeF.Width)
                return font;
            var scaleFactor = sizeF.Width / actualTextWidth;
            font = new Font(FontName, font.SizeInPoints * scaleFactor, fontStyle);
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
            var font = CreateFontThatFitToGivenSize(text, drawArea.Size, FontStyle.Regular);
            graphics.DrawString(text, font, new SolidBrush(color), drawArea, _alignCenter);
        }

        private void DrawString(string text, int minTextWidth, Color color, RectangleF drawArea)
        {
            var font = CreateFontThatFitToGivenSize(text.PadLeft(minTextWidth, '0'), drawArea.Size, FontStyle.Regular);
            graphics.DrawString(text, font, new SolidBrush(color), drawArea, _alignCenter);
        }
    }
}
