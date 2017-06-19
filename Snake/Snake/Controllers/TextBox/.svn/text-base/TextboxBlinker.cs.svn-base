using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.Controllers.TextBox
{
    class TextboxBlinker : GameComponent
    {
        private SpriteBatch _spriteBatch;        
        private int _blinkerPosition;
        private const int CTimeBlinkDuration = 200;
        private int _timeLastBlink;
        private bool _showBlinker;
        private Sprite _blinker;

        public TextboxBlinker(Game game, SpriteBatch spriteBatch, Sprite blinker) : base(game)
        {
            _spriteBatch = spriteBatch;
            _timeLastBlink = 0;
            _blinkerPosition = 0;
            _showBlinker = false;
            _blinker = blinker;
        }

        public int Position
        {
            get { return _blinkerPosition; }
            set { _blinkerPosition = value; }
        }

        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - _timeLastBlink >= CTimeBlinkDuration)
            {
                _timeLastBlink = (int) gameTime.TotalGameTime.TotalMilliseconds;
                _showBlinker = !_showBlinker;
            }
        }

        public void Draw(int x, int y)
        {
            if (_showBlinker)
                _blinker.Draw(x,y);            
        }

    }
}
