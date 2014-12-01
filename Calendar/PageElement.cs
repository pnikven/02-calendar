using System;
using System.Collections.Generic;
using System.Drawing;

namespace Calendar
{
    abstract class PageElement
    {
        private readonly List<PageElement> children = new List<PageElement>();

        public virtual void AddChildElement(PageElement pageElement)
        {
            children.Add(pageElement);
        }

        public List<PageElement> GetChildren()
        {
            return children;
        }
    }

    class RootElement : PageElement
    {
        public RootElement(Color backgroundColor)
        {
            BackgroundColor = backgroundColor;
        }

        public Color BackgroundColor { get; private set; }
    }

    class TextElement : PageElement
    {
        public Color TextColor { get; set; }
        public string Text { get; private set; }

        public TextElement(string headerText, Color color)
        {
            Text = headerText;
            TextColor = color;
        }

        public override void AddChildElement(PageElement pageElement)
        {
            throw new Exception(GetType() + " can not contain child elements");
        }
    }

    class HeaderElement : TextElement
    {
        public HeaderElement(string headerText, Color color)
            : base(headerText, color)
        {
        }
    }

    class CellElement : TextElement
    {
        public PageElement Parent { get; private set; }
        public bool IsMarked { get; private set; }

        public CellElement(string text, Color color, PageElement parent, bool isMarked)
            : base(text, color)
        {
            Parent = parent;
            IsMarked = isMarked;
        }
    }

    class TableElement : PageElement
    {
        public override void AddChildElement(PageElement pageElement)
        {
            if (pageElement.GetType() != typeof(RowElement))
                throw new Exception("Table element can contain only row elements");
            base.AddChildElement(pageElement);
        }
    }

    class RowElement : PageElement
    {
        public override void AddChildElement(PageElement pageElement)
        {
            if (pageElement.GetType() != typeof(CellElement))
                throw new Exception("Row element can contain only cell elements");
            base.AddChildElement(pageElement);
        }
    }
}
