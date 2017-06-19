using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Buttons;

namespace Snake.Controllers
{
    public class LanguageController
    {
        private Game _game;
        private SpriteBatch _spriteBatch;
        private Dictionary<string, SpriteButtonSelectable> _buttons;
        private SpriteFont _font;

        public LanguageController(Game game, SpriteBatch spriteBatch, SpriteFont font)
        {
            _game = game;
            _spriteBatch = spriteBatch;
            _font = font;

            Sprite baPressed = new Sprite(spriteBatch,game.Content,"Flags/FlagBosnianPressed");
            Sprite baNormal = new Sprite(spriteBatch,game.Content,"Flags/FlagBosnianNormal");
            Sprite enPressed = new Sprite(spriteBatch,game.Content,"Flags/FlagUKPressed");
            Sprite enNormal = new Sprite(spriteBatch,game.Content,"Flags/FlagUKNormal");

            _buttons = new Dictionary<string, SpriteButtonSelectable>();
            _buttons.Add("BA", new SpriteButtonSelectable(_game, spriteBatch, "BA", baPressed, baNormal, new Vector2(50, 470), "", _font));
            _buttons.Add("EN", new SpriteButtonSelectable(_game,spriteBatch, "EN", enPressed, enNormal, new Vector2(80, 470), "", _font));
            _buttons["BA"].SetSelected();
        }

        public void Enable ()
        {
            foreach (SpriteButtonSelectable button in _buttons.Values)
                button.MouseUp += MouseUp;
        }

        public void Disable()
        {
            foreach (SpriteButtonSelectable button in _buttons.Values)
                button.MouseUp -= MouseUp;
        }

        void MouseUp(object sender, SpriteButtonEventArgs args)
        {
            switch (args.Name)
            {
                case "BA":    
                    _buttons["EN"].Reset();
                    ((Game1) _game).ChangeLanguage("BA");
                    break;
                case "EN":
                    _buttons["BA"].Reset();
                    ((Game1) _game).ChangeLanguage("EN");
                    break;
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (SpriteButtonSelectable buttonSelectable in _buttons.Values)            
                buttonSelectable.Draw();            
        }
    }
}
