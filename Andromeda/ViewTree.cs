using System;
using System.Collections.Generic;
using System.Numerics;
using SkiaSharp;

namespace Andromeda
{
    public class ViewTree
    {
        public IViewWidget Root;

        private SKCanvas _canvas;
        private SKPaint _paint;

        public ViewTree(SKCanvas canvas, SKPaint paint)
        {
            _canvas = canvas;
            _paint = paint;
        }

        //todo: Only call when needed
        public void Draw()
        {
            _canvas.Clear(SKColors.Black);
            
            Root.Draw(ref _canvas, ref _paint, Vector2.Zero);
            
            // this actually draws shit and without it there will be no drawing
            _canvas.Flush();
        }
    }

    public interface IDrawable
    {
        /// <summary>
        /// This method is responsible for drawing a given node. NOTHING is set back or changed outside of this function
        /// </summary>
        /// <param name="canvas">Reference to the current canvas</param>
        /// <param name="paint">Reference to the paint used by that canvas</param>
        /// <param name="parentPos">the position that the parent allows this widget to start from</param>
        public void Draw(ref SKCanvas canvas, ref SKPaint paint, Vector2 parentPos);

        public Vector2 Size { get; }
        public ILayoutMode Layout { get; }
    }

    /// <summary>
    /// Something which has no child nodes
    /// </summary>
    public interface IViewLeaf
    {
        IViewLeaf Parent { get; set;  }
    }

    /// <summary>
    /// Something with only one child
    /// </summary>
    public interface IViewWrapper : IViewLeaf
    {
        IViewWidget Child { get; }
    }

    /// <summary>
    /// Something with multiple children
    /// </summary>
    public interface IViewNode : IViewLeaf
    {
        List<IViewWidget> Children { get; }
    }

    /// <summary>
    /// This is the most basic thing that can be reasonably drawn
    /// </summary>
    public interface IViewWidget : IDrawable, IViewLeaf
    {
        
    }
    
    
}