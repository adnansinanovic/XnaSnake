using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Buttons;
using Snake.Controllers.Buttons;
using Snake.LevelManagment;

namespace Snake.SelectorComponenet
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Selector: Microsoft.Xna.Framework.GameComponent
    {
        private SpriteBatch _spriteBatch;
        private int _index;
        private Dictionary<LoadedFonts, SpriteFont> _fonts;
        private List<SelectorItem> _items;
        private Vector2 _position;
        private SpriteButton _sprPlus;
        private SpriteButton _sprMinus;
        private string _caption;

        #region Constants
        private const int COffsetBetweenButtons = 5;
        private const int COffsetBetweenCaptionAndButtons = 5;
        private const int COffsetCaptionX = 50;
        #endregion
        public Selector(Game game, SpriteBatch spriteBatch, Dictionary<LoadedFonts, SpriteFont> fonts, Vector2 position, string caption)
            : base(game)
        {
            _spriteBatch = spriteBatch;            
            _fonts = fonts;
            _index = 0;
            _caption = caption;
            _position = position;
            _items = new List<SelectorItem>();
            Sprite normal50 = new Sprite(spriteBatch, Game.Content, @"Buttons/Cloud50_N");
            Sprite pressed50 = new Sprite(spriteBatch, Game.Content, @"Buttons/Cloud50_P");
            _sprPlus = new SpriteButton(game, spriteBatch, "PLUS", pressed50, normal50, new Vector2(position.X, position.Y+COffsetBetweenCaptionAndButtons), "+",fonts[LoadedFonts.KristenITC]);
            _sprMinus = new SpriteButton(game, spriteBatch, "MINUS", pressed50, normal50, new Vector2(position.X, position.Y + _sprPlus.Height + COffsetBetweenCaptionAndButtons+COffsetBetweenButtons), "-", fonts[LoadedFonts.KristenITC]);

            Enable();

            Dictionary<string, Level> levels = ((Game1) Game).GetLevels();
            
            foreach (KeyValuePair<string, Level> level in levels)            
                _items.Add(new SelectorItem(game,spriteBatch,fonts,level.Value.Name,level.Key));            
        }

        public string SelectedKeyValue
        {
            get { return _items[_index].Key; }            
        }

        void Buttons_MouseUp(object sender, SpriteButtonEventArgs args)
        {
            switch(args.Name)
            {
                case "PLUS":
                    IncreaseIndex();
                    break;
                case "MINUS":
                    DecreaseIndex();
                    break;
            }
        }

        private void DecreaseIndex()
        {
            if (_index - 1 < 0)
                _index = _items.Count - 1;
            else
                _index--;
        }

        private void IncreaseIndex()
        {
            if (_index + 1 >= _items.Count)
                _index = 0;
            else
                _index++;
        }

        public void Draw(GameTime gameTime)
        {
            Vector2 captionPosition = _position;
            captionPosition.X += COffsetCaptionX;
            _spriteBatch.DrawString(_fonts[LoadedFonts.KristenITC_fs10],((Game1)this.Game).GetMessage(_caption),captionPosition,Color.Black);
            
            _sprPlus.Draw();
            _sprMinus.Draw();

            Vector2 displayValuePosition = Vector2.Zero;
            displayValuePosition.X = _position.X + _sprPlus.Width + 10;
            displayValuePosition.Y = _position.Y + ((_sprPlus.Height * 2 + COffsetBetweenButtons + COffsetBetweenCaptionAndButtons)/2 - _fonts[LoadedFonts.KristenITC_fs18].MeasureString("Z").Y/2);
            
            if (_items.Count > 0)
                _items[_index].Draw(gameTime, displayValuePosition, Color.Black);
        }

        public void Enable()
        {
            _sprPlus.MouseUp += (Buttons_MouseUp);
            _sprMinus.MouseUp += (Buttons_MouseUp);
        }

        public void Disable()
        {
            _sprPlus.MouseUp -= (Buttons_MouseUp);
            _sprMinus.MouseUp -= (Buttons_MouseUp);
        }
    }
}
