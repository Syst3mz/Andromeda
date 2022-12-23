using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SkiaSharp;

namespace Andromeda
{
    public enum StackDirection
    {
        Vertical,
        Horizontal
    }

    public class Stack : IViewWidget, IViewNode
    {
        private Vector2 _size;
        public Vector2 Size => _size;
        public ILayoutMode Layout { get; }
        public IViewLeaf Parent { get; set; }
        public List<IViewWidget> Children { get; }
        public StackDirection StackDirection;

        public Stack(StackDirection stackDirection, ILayoutMode layout, IViewLeaf parent, List<IViewWidget> children = null)
        {
            StackDirection = stackDirection;
            Layout = layout;
            Parent = parent;

            if (children != null)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].Parent = this;
                }
            }
            
            Children = children;
            FindSize();
        }

        public void AddChild(IViewWidget w)
        {
            w.Parent = this;
            Children.Add(w);
            _size = FindSize();
        }

        private Vector2 FindSize()
        {
            Vector2 ret;
            if (StackDirection == StackDirection.Horizontal)
            {
                float maxHeight = Children.Select(x => x.Size.Y).Max();
                float width = Children.Select(x => x.Size.X).Sum();
                ret = new Vector2(width, maxHeight);
            }
            else
            {
                float maxWidth = Children.Select(x => x.Size.X).Max();
                float height = Children.Select(x => x.Size.Y).Sum();
                ret = new Vector2(maxWidth, height);
            }

            return ret;
        }

        private Vector2 FindStackDeltaPosition(IViewWidget w)
        {
            if (StackDirection == StackDirection.Horizontal)
            {
                return new Vector2(w.Size.X, 0);
            }
            else
            {
                return new Vector2(0, w.Size.Y);
            }
        }

        public void Draw(ref SKCanvas canvas, ref SKPaint paint, Vector2 parentPos)
        {
            Vector2 nextDraw = Vector2.Zero;
            foreach (var child in Children)
            {
                child.Draw(ref canvas, ref paint, nextDraw + parentPos);
                nextDraw += FindStackDeltaPosition(child);
            }
        }
    }
}