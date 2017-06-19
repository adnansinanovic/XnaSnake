using Common.MouseManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Buttons;

namespace Snake.Controllers.Buttons
{  
    public delegate void SpriteButtonEventHandler(object sender, SpriteButtonEventArgs args);

    public class SpriteButton : GameComponent
    {
        protected enum SpriteButtonState
        {
            Idle,
            Pressed
        }        

        #region Events
        public event SpriteButtonEventHandler MouseUp;
        public event SpriteButtonEventHandler MouseDown; 
        #endregion

        #region Fields
        private readonly MouseController _mouseController;
        private readonly Sprite _sprPressed;
        private readonly Sprite _regular;
        protected SpriteButtonState _state;
        protected string _id;
        private Vector2 _position;
        private readonly string _caption;
        private readonly SpriteFont _font;
        protected bool _enabled;
        private SpriteBatch _spriteBatch;
        private Game _game;
        #endregion

        #region Properties
        public Vector2 Position
        {
            get { return _position; }
        }

        public float Height
        {
            get { return _sprPressed.Height; }
        }

        public float Width
        {
            get { return _sprPressed.Width; }
        }

        #endregion

        #region Constructors
        public SpriteButton(Game game, SpriteBatch spriteBatch, string id, Sprite pressed, Sprite regular, Vector2 position, string caption, SpriteFont font) : base(game)
        {
            _spriteBatch = spriteBatch;
            _game = game;

            _mouseController = MouseController.Instance;
            _mouseController.MouseUp += OnMouseUp;
            _mouseController.MouseDown += OnMouseDown;
            _mouseController.MouseMoveDown +=OnMouseMoveDown;

            _sprPressed = pressed;
            _regular = regular;
            _position = position;
            _state = SpriteButtonState.Idle;
            _caption = caption;
            _font = font;
            _id = id.ToUpper();
            _enabled = true;
        }

        protected virtual void OnMouseMoveDown(object sender, MouseControllerEventArgs args)
        {
            
        }
        
        #endregion

        #region Draw Methods
        private void DrawCaption()
        {
            Vector2 captionSize = _font.MeasureString(GetMessage(_caption));
            Vector2 captionPosition = Vector2.Zero;
            captionPosition.X = _position.X + _sprPressed.Width / 2f - captionSize.X/2;
            captionPosition.Y = _position.Y + _sprPressed.Height / 2f - captionSize.Y / 2;

            if (_state == SpriteButtonState.Pressed)
                captionPosition.Y += 2;

            _spriteBatch.DrawString(_font,GetMessage(_caption) , captionPosition, Color.Black);
        }

        private string GetMessage(string key)
        {
            return ((Game1)this.Game).GetMessage(key);
        }

        private void DrawGraphic()
        {
            switch (_state)
            {
                case SpriteButtonState.Idle:
                    _regular.Draw(_position);
                    break;
                case SpriteButtonState.Pressed:
                    _sprPressed.Draw(_position);
                    break;
            }
        } 
        #endregion

        protected void ChangeState(SpriteButtonState state)
        {
            _state = state;
        }

        protected bool IntersectsWith(Vector2 position)
        {
            return  (position.X > _position.X && position.X < (_position.X + _sprPressed.Width) &&
                position.Y > _position.Y && position.Y < (_position.Y + _sprPressed.Height));
        }

        protected void RaiseMouseUp(SpriteButton spriteButton, SpriteButtonEventArgs args)
        {          
            if (MouseUp != null)
                MouseUp(this, args);         
        }

        protected void RaiseMouseDown(SpriteButton spriteButton, SpriteButtonEventArgs args)
        {
            if (MouseDown != null)
                MouseDown(this, args);
        }
        
        protected virtual void OnMouseUp(object sender, MouseControllerEventArgs args)
        {
            if (_enabled)
            {
                if (_state == SpriteButtonState.Pressed)
                {
                    ChangeState(SpriteButtonState.Idle);

                    RaiseMouseUp(this, new SpriteButtonEventArgs(args.Position, _id));
                } 
            }
        }
        
        protected virtual void OnMouseDown(object sender, MouseControllerEventArgs args)
        {
            if (_enabled)
            {
                bool intersects = IntersectsWith(args.Position);

                if (intersects)
                    ChangeState(SpriteButtonState.Pressed);   
            }
        }

        public void Draw()
        {
            Enable();
            DrawGraphic();
            DrawCaption();
        }

        public void Disable()
        {
            _enabled = false;
        }

        public void Enable()
        {
            _enabled = true;
        }
        
    }
}
