using System;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace Snake.Buttons
{
    public class SpriteButtonEventArgs
    {
        private Vector2 _position;
        private string _name;

        public string Name
        {
            get { return _name; }
        }

        public SpriteButtonEventArgs(Vector2 position, string name)
        {
            _position = position;
            _name = name;
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
