using System.Numerics;

namespace Andromeda
{
    /// <summary>
    /// A simple interface for handling layout
    /// </summary>
    public interface ILayoutMode
    {
        public Vector2 Apply(Vector2 parentPos);
    }

    /// <summary>
    /// Position the widget absolutely without reference to its parent
    /// </summary>
    public struct AbsoluteLayout : ILayoutMode
    {
        public Vector2 TopLeft;

        public AbsoluteLayout(Vector2 topLeft)
        {
            TopLeft = topLeft;
        }


        public Vector2 Apply(Vector2 parentPos)
        {
            return TopLeft;
        }
    }

    /// <summary>
    /// A position relative to its parent
    /// </summary>
    public struct RelativePosition : ILayoutMode
    {
        public Vector2 Offset;

        public RelativePosition(Vector2 offset)
        {
            Offset = offset;
        }

        public Vector2 Apply(Vector2 parentPos)
        {
            return parentPos + Offset;
        }
    }

    /// <summary>
    /// A position which is FULLY controlled by the parent. It's the same thing as a relative position with an offset of zero.
    /// </summary>
    public struct ControlledPosition : ILayoutMode
    {
        public Vector2 Apply(Vector2 parentPos)
        {
            return parentPos;
        }
    }
}