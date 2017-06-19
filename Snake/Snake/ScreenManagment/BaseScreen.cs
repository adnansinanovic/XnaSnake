using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Buttons;
using Snake.Controllers.Buttons;

namespace Snake.ScreenManagment
{
    public class BaseScreen : GameComponent, IScreen
    {
        #region Fields
        private List<SpriteButton> _buttons;
        private bool _started;
        private SpriteBatch _spriteBatch;
        private Game _game;
        private Screens _screenType;
        private Dictionary<LoadedFonts, SpriteFont> _fonts;
        #endregion

        #region Properties
        protected List<SpriteButton> Buttons
        {
            get { return _buttons; }
            set { _buttons = value; }
        }

        protected SpriteBatch SpriteBatch
        {
            get { return _spriteBatch; }
        }

        public bool Started
        {
            get { return _started; }
            set { _started = value; }
        }

        public Screens ScreenType
        {
            get { return _screenType; }
        }

        protected Dictionary<LoadedFonts, SpriteFont> Fonts
        {
            get { return _fonts; }
        }

        public Game1 Game1
        {
            get { return (Game1)_game; }
        }

        #endregion

        public BaseScreen(Game game, SpriteBatch spriteBatch, Screens screenType, Dictionary<LoadedFonts, SpriteFont> fonts ) : base(game)
        {
            _game = game;
            _fonts = fonts;
            _spriteBatch = spriteBatch;
            _screenType = screenType;
            _buttons = new List<SpriteButton>();            
        }               

        protected virtual void DisableButtons()
        {
            foreach (SpriteButton button in _buttons)
                button.MouseUp -= ButtonMouseUp;
        }

        protected virtual void EnableButtons()
        {
            foreach (SpriteButton button in _buttons)
                button.MouseUp += ButtonMouseUp;
        }

        protected virtual void ButtonMouseUp(object sender, SpriteButtonEventArgs args)
        {
          
        }

        public virtual void Start()
        {
            EnableButtons();
            _started = true;
        }

        public virtual void End()
        {
            DisableButtons();
            _started = false;
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (SpriteButton button in _buttons)
                button.Draw();
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
