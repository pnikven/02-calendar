using System;
using System.Collections.Generic;
using System.Drawing;

namespace Calendar
{
    enum PageElementType
    {
        Block,
        Inline
    }

    abstract class PageElement
    {
        public PageElement Parent { get; private set; }

        private readonly List<PageElement> children = new List<PageElement>();

        public virtual void AddChild(PageElement pageElement)
        {
            if (!ChildIsSupported(pageElement.GetType()))
                throw new Exception(String.Format("Page element of type {0} can't contain children of type {1}",
                    GetType(), pageElement.GetType()));
            children.Add(pageElement);
            pageElement.Parent = this;
        }

        public void AddChildRange(PageElement[] pageElements)
        {
            foreach (var pageElement in pageElements)
            {
                AddChild(pageElement);
            }
        }

        public List<PageElement> GetChildren()
        {
            return children;
        }

        public SizeF ConsumedSizeRelativeToParent()
        {
            switch (GetPageElementType())
            {
                case PageElementType.Block:
                    return new SizeF(1, 1f / Parent.GetChildren().Count);
                case PageElementType.Inline:
                    return new SizeF(1f / Parent.GetChildren().Count, 1);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public abstract PageElementType GetPageElementType();
        protected abstract bool ChildIsSupported(Type typeOfChild);
    }

    class RootElement : PageElement
    {
        public Color BackgroundColor { get; private set; }

        public RootElement(Color backgroundColor)
        {
            BackgroundColor = backgroundColor;
        }

        public override PageElementType GetPageElementType()
        {
            return PageElementType.Block;
        }

        protected override bool ChildIsSupported(Type typeOfChild)
        {
            return typeOfChild == typeof(TextElement) ||
                   typeOfChild == typeof(RowElement);
        }
    }

    class TextElement : PageElement
    {
        public Color TextColor { get; set; }
        public string Text { get; private set; }

        public TextElement(string text, Color color)
        {
            Text = text;
            TextColor = color;
        }

        public override PageElementType GetPageElementType()
        {
            return PageElementType.Block;
        }

        protected override bool ChildIsSupported(Type typeOfChild)
        {
            return false;
        }
    }

    class CellElement : TextElement
    {
        public bool IsMarked { get; private set; }

        public CellElement(string text, Color color, bool isMarked)
            : base(text, color)
        {
            IsMarked = isMarked;
        }

        public override PageElementType GetPageElementType()
        {
            return PageElementType.Inline;
        }
    }

    class RowElement : PageElement
    {
        public override PageElementType GetPageElementType()
        {
            return PageElementType.Block;
        }

        protected override bool ChildIsSupported(Type typeOfChild)
        {
            return typeOfChild == typeof(CellElement);
        }
    }
}
