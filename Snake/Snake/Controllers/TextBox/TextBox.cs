using Common.KeyboardManager;
using Common.MouseManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Buttons;

namespace Snake.Controllers.TextBox
{
    public class TextBox : GameComponent
    {
        private KeyboardController _keyboardController;
        private readonly MouseController _mouseController;
        
        private readonly Sprite _sprSelected;
        private readonly Sprite _sprRegular;
        
        protected string _id;
        private Vector2 _position;        
        private readonly SpriteFont _font;
        protected bool _enabled;
        private SpriteBatch _spriteBatch;
        private bool _selected;
        private string _text;
        private int _maxCharacters;
        private TextboxBlinker _blinker;
        private string _defaultCaption;
        
        public TextBox(Game game, SpriteBatch spriteBatch, string defaultCaption, int maxCharacters, Vector2 position, SpriteFont font) : base(game)
        {
            _spriteBatch = spriteBatch;
            _blinker = new TextboxBlinker(game, spriteBatch,new Sprite(spriteBatch,game.Content,"Blinker"));

            _sprRegular = new Sprite(spriteBatch,game.Content, "TextBox/bcg_r", position);
            _sprSelected= new Sprite(spriteBatch, game.Content, "TextBox/bcg_s", position);
            _font = font;

            _mouseController = MouseController.Instance;
            _mouseController.MouseUp += OnMouseUp;

            _keyboardController = KeyboardController.Instance;
            _keyboardController.KeyPressed +=OnKeyPressed;
            
            _maxCharacters = maxCharacters;
            _position = position;
            _defaultCaption = defaultCaption;
            _text = defaultCaption;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _blinker.Position = value.Length;
                _text = value;
            }
        }

        #region Event Methods
        private void OnKeyPressed(object sender, KeyboardControllerEventArgs args)
        {
            if (_enabled && _selected)
            {
                if (char.IsLetterOrDigit((char)args.Key))
                    OnPrintableCharacterPressed(args.Key);
                else
                {
                    switch (args.Key)
                    {
                        case Keys.Back:
                            OnBackspacePressed();
                            break;
                        case Keys.Delete:
                            OnDeletePressed();
                            break;                        
                    }
                }
            }
        }        

        private void OnPrintableCharacterPressed(Keys key)
        {
            if (_text.Length<_maxCharacters)
            {
                _text += key;
                _blinker.Position++;                
            }
        }

        private void OnDeletePressed()
        {
            if (_blinker.Position < _text.Length - 1)
            {
                _text.Remove(_blinker.Position + 1, 1);
            }
        }

        private void OnBackspacePressed()
        {
            if (_blinker.Position > 0 && _blinker.Position <= _text.Length)
            {
                _text = _text.Remove(_blinker.Position - 1, 1);
                _blinker.Position--;
            }
        }         

        private void OnMouseUp(object sender, MouseControllerEventArgs args)
        {
            if (_enabled)
            {
                bool intersects = IntersectsWith(args.Position);
                
                if (intersects)
                {
                    _text = "";
                    _selected = true;
                }
                else
                {
                    if (_text == "")
                        _text = _defaultCaption;

                    _selected = false;
                }
            }
        }
        #endregion

        protected bool IntersectsWith(Vector2 position)
        {
            return (position.X > _position.X && position.X < (_position.X + _sprSelected.Width) &&
                position.Y > _position.Y && position.Y < (_position.Y + _sprSelected.Height));
        }

        public void Draw(GameTime gameTime)
        {
            if (_selected)
            {
                _sprSelected.Draw(_position);
                DrawText();                                
                _blinker.Draw((int) ((int) _position.X + 10 + _font.MeasureString(_text).X),(int) (_position.Y + 25));
            }
            else
            {
                _sprRegular.Draw(_position);
                DrawText();
            }
        }

        private void DrawText()
        {
            _spriteBatch.DrawString(_font, _text, new Vector2(_position.X + 10, _position.Y + 20), Color.Black);
        }

        public void Enable()
        {
            _enabled = true;
        }

        public void Disable()
        {
            _enabled = true;
        }

        public override void Update(GameTime gameTime)
        {
            _blinker.Update(gameTime);
        }
    }
}
