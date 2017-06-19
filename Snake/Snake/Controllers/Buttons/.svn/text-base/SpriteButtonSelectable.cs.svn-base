using Common.MouseManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Controllers.Buttons;

namespace Snake.Buttons
{
    public class SpriteButtonSelectable : SpriteButton
    {
        #region Events
        public event SpriteButtonEventHandler TurnedOn;
        public event SpriteButtonEventHandler TurnedOff;
        
        #endregion
        private bool _selected;
        public SpriteButtonSelectable(Game game, SpriteBatch spriteBatch, string id, Sprite pressed, Sprite regular, Vector2 position, string caption, SpriteFont font) 
            : base(game, spriteBatch, id, pressed, regular, position, caption, font)
        {            
            _selected = false;
        }

        public bool Selected
        {
            get { return _selected; }
        }

        protected override void OnMouseUp(object sender, MouseControllerEventArgs args)
        {
            if (_enabled)
            {
                bool intersects = IntersectsWith(args.Position);

                if (intersects)
                {
                    RaiseMouseUp(this, new SpriteButtonEventArgs(args.Position, _id));

                    ToogleSelect(args);
                }
            }                     
        }

        private void ToogleSelect(MouseControllerEventArgs args)
        {
            if (_selected)
            {
                _selected = false;
                ChangeState(SpriteButtonState.Idle);

                if (TurnedOff != null)
                    TurnedOff(this, new SpriteButtonEventArgs(args.Position, _id));
            }
            else if (!_selected)
            {
                _selected = true;
                ChangeState(SpriteButtonState.Pressed);

                if (TurnedOn != null)
                    TurnedOn(this, new SpriteButtonEventArgs(args.Position, _id));
            }
        }

        protected override void OnMouseDown(object sender, MouseControllerEventArgs args)
        {
            if (_enabled)
            {
                bool intersects = IntersectsWith(args.Position);

                if (intersects)
                {
                    RaiseMouseDown(this, new SpriteButtonEventArgs(args.Position, _id));
                } 
            }
        }

        protected override void OnMouseMoveDown(object sender, MouseControllerEventArgs args)
        {
            if (_enabled)
            {
                bool intersects = IntersectsWith(args.Position);

                if (intersects)
                {                    
                    ToogleSelect(args);
                }
            }
        }

        public void Reset()
        {
            _selected = false;
            ChangeState(SpriteButtonState.Idle);            
        }

        public void SetSelected()
        {
            _selected = true;
            ChangeState(SpriteButtonState.Pressed);
        }
    }
}
