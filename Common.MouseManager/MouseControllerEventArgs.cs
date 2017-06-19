using System;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace Common.MouseManager
{
    public class MouseControllerEventArgs
    {
        private Vector2 _position;

        public MouseControllerEventArgs(Vector2 position)
        {
            _position = position;
        }

        public Vector2 Position
        {
            get { return _position; }
        }

        public int X
        {
            get { return Convert.ToInt32(_position.X, CultureInfo.InvariantCulture); }
        }

        public int Y
        {
            get { return Convert.ToInt32(_position.Y, CultureInfo.InvariantCulture); }
        }
    }
}
