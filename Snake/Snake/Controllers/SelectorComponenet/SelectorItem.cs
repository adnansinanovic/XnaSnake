using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.SelectorComponenet
{
    class SelectorItem : GameComponent
    {
        private SpriteBatch _spriteBatch;
        private string _displayDisplayValue;
        private string _key;
        private Dictionary<LoadedFonts, SpriteFont> _fonts;

        public SelectorItem(Game game, SpriteBatch spriteBatch, Dictionary<LoadedFonts, SpriteFont> fonts, string displayValue, string key) : base(game)
        {
            _spriteBatch = spriteBatch;
            _fonts = fonts;
            _displayDisplayValue = displayValue;
            _key = key;
        }

        public string Key
        {
            get { return _key; }            
        }

        public void Draw(GameTime gameTime, Vector2 position, Color color)
        {
            _spriteBatch.DrawString(_fonts[LoadedFonts.KristenITC_fs18],_displayDisplayValue, position,color);
        }
    }
}
