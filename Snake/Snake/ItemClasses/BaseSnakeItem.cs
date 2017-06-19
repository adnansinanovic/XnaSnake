using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
    public class BaseSnakeItem : GameComponent
    {
        private Vector2 _position;
        private Sprite _texture;
        private SpriteBatch _spriteBatch;

        public BaseSnakeItem(Game game, SpriteBatch spriteBatch, Vector2 position, Sprite texture)
            : base(game)
        {
            _position = position;
            _texture = texture;
            _spriteBatch = spriteBatch;
        }


        public virtual void Draw()
        {
            _texture.Draw(_position);
        }

        public void Draw(int x, int y)
        {
            _texture.Draw(x, y);
        }

        public Vector2 Position
        {
            get { return _position; }
            protected set { _position = value; }

        }

        public float X
        {
            get { return _position.X; }
        }

        public float Y
        {
            get { return _position.Y; }
        }

        public int Width
        {
            get { return _texture.Width; }
        }

        public int Height
        {
            get { return _texture.Height; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)X, (int)Y, Width, Height); }
        }

        protected SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }
        }

        public void SetPosition(int x, int y)
        {
            _position.X = x;
            _position.Y = y;
        }

        public void SetPosition(Vector2 position)
        {
            SetPosition((int) position.X, (int) position.Y);
        }       
    }
}
