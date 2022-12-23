using System;
using System.Numerics;
using SkiaSharp;

namespace Andromeda
{
    public class Frame : IViewWidget, IViewWrapper
    {
        public Vector2 Size => new Vector2(Child.Size.X + Thickness, Child.Size.Y + Thickness);
        public ILayoutMode Layout { get; } = new ControlledPosition();
        public IViewLeaf Parent { get; set; }
        public IViewWidget Child { get; set; }
        public SKColor Color;
        public float Thickness;

        public void Draw(ref SKCanvas canvas, ref SKPaint paint, Vector2 parentPos)
        {
            var halfThick = Thickness / 2;
            var borderPaint = new SKPaint() { StrokeWidth = Thickness, IsStroke = true, Color = Color};
            var derivedPosition = Layout.Apply(parentPos);
            canvas.DrawRect(derivedPosition.X, derivedPosition.Y, Child.Size.X+halfThick, Child.Size.Y+halfThick, borderPaint);
            Child.Draw(ref canvas, ref paint, derivedPosition + new Vector2(halfThick, halfThick));
        }

        public Frame(SKColor color, float thickness, IViewLeaf parent, IViewWidget child=null)
        {
            Color = color;
            Thickness = thickness;
            Parent = parent;
            if (child != null)
            {
                child.Parent = this;
            }
            Child = child;
        }
    }
}