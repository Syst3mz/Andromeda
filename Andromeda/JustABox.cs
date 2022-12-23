using System.Drawing;
using System.Numerics;
using SkiaSharp;

namespace Andromeda
{
    public class JustABox : IViewWidget
    {
        public IViewLeaf Parent { get; set; }
        public SKColor Color;
        public Vector2 Size { get; set; }
        public ILayoutMode Layout { get; } = new ControlledPosition();
        public void Draw(ref SKCanvas canvas, ref SKPaint paint, Vector2 parentPos)
        {
            var derivedPosition = Layout.Apply(parentPos);
            canvas.DrawRect(derivedPosition.X, derivedPosition.Y, Size.X, Size.Y, new SKPaint() {Color = Color});
        }
        
        public JustABox(SKColor color, IViewLeaf parent, Vector2 size)
        {
            Color = color;
            Parent = parent;
            Size = size;
        }
    }
}

