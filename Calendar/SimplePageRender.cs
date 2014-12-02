using System;
using System.Collections.Generic;
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
            if (pageElement.GetType() == typeof(RootElement))
            {
                DrawPageBackground(new RectangleF(new PointF(0, 0), size),
                    ((RootElement)pageElement).BackgroundColor);
            }
            else if (pageElement.GetType() == typeof(TextElement))
            {
                DrawString(((TextElement)pageElement).Text, ((TextElement)pageElement).TextColor,
                    new RectangleF(origin, size));
            }
            else if (pageElement.GetType() == typeof(CellElement))
            {
                DrawString(((CellElement)pageElement).Text, MinTextWidthForCellElement,
                    ((CellElement)pageElement).TextColor,
                    new RectangleF(origin, size));
            }
            DrawChildren(pageElement, origin, size);
        }

        private void DrawChildren(PageElement pageElement, PointF origin, SizeF size)
        {
            var x = origin.X;
            var y = origin.Y;
            foreach (PageElement child in pageElement.GetChildren())
            {
                SizeF relativeSize = child.ConsumedSizeRelativeToParent();
                SizeF absoluteSize = new SizeF(size.Width*relativeSize.Width, size.Height*relativeSize.Height);
                DrawElement(child, new PointF(x, y), absoluteSize);
                if (child.GetPageElementType() == PageElementType.Block)
                    y += absoluteSize.Height;
                else if (child.GetPageElementType() == PageElementType.Inline)
                    x += absoluteSize.Width;
            }
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
