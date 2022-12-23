using System.Numerics;

namespace Andromeda
{
    public struct Box
    {
        private Vector2 _size;
        public Vector2 Size => _size;
        private Vector2 _position;

        private Vector2 _topLeft;
        public Vector2 TopLeft => _topLeft;

        public float Width
        {
            get
            {
                return _size.X;
            }
            set
            {
                _size.X = value;
                SetTopLeft();
            }
        }
        
        public float Height
        {
            get
            {
                return _size.Y;
            }
            set
            {
                _size.Y = value;
                SetTopLeft();
            }
        }

        private void SetTopLeft()
        {
            _topLeft = new Vector2(_position.X - _size.X / 2, _position.Y - _size.Y / 2);
        }

        public Box(Vector2 position, Vector2 size)
        {
            _size = size;
            _position = position;
            _topLeft = new Vector2();
            SetTopLeft();
        }
    }
}