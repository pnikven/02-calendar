using System;
using System.Collections.Generic;
using System.Drawing;

namespace Calendar
{
    enum PageElementType
    {
        Root,
        Block,
        Inline
    }

    class PageElement
    {
        public PageElement Parent { get; private set; }
        public PageElementType ElementType { get; private set; }
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }
        public string Text { get; set; }

        private readonly Color _defaultBackgroundColor = Color.White;
        private readonly Color _defaultForegroundColor = Color.Black;
        private readonly List<PageElement> children = new List<PageElement>();

        public PageElement(PageElementType elementType)
        {
            ElementType = elementType;
            BackgroundColor = _defaultBackgroundColor;
            ForegroundColor = _defaultForegroundColor;
        }

        public virtual void AddChild(PageElement child)
        {
            if (!ChildIsSupported(child))
                throw new Exception(String.Format("Page element of type {0} can't contain children of type {1}",
                   ElementType, child.ElementType));
            children.Add(child);
            child.Parent = this;
        }

        public void AddChildRange(IEnumerable<PageElement> pageElements)
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
            switch (ElementType)
            {
                case PageElementType.Block:
                    return new SizeF(1, 1f / Parent.GetChildren().Count);
                case PageElementType.Inline:
                    return new SizeF(1f / Parent.GetChildren().Count, 1);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool ChildIsSupported(PageElement child)
        {
            switch (ElementType)
            {
                case PageElementType.Root:
                    return child.ElementType == PageElementType.Block;
                case PageElementType.Block:
                    return child.ElementType == PageElementType.Inline;
                case PageElementType.Inline:
                    return false;
                default:
                    throw new Exception("ElementType is not recognized");
            }
        }

        public bool HasText()
        {
            return Text != null;
        }
    }
}
