using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Common.MouseManager
{
    public delegate void MouseClickHandler(object sender, MouseControllerEventArgs args);

    public class MouseController
    {
        public event MouseClickHandler MouseDown;
        public event MouseClickHandler MouseUp;
        public event MouseClickHandler MouseMoveDown;

        #region Singleton
        private static MouseController _instance;
        private MouseController()
        { }

        public static MouseController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MouseController();
                return _instance;
            }
        }
        #endregion

        private long _lastUpdate; //miliseconds
        private const int CSensitivity = 110;  //lower value, greater senistivity

        private Vector2 _previousCursorPosition = Vector2.Zero;
        private Vector2 _currentCursosPosition = Vector2.Zero;
        private ButtonState _previousStateLeftBtn;
        private ButtonState _currentStateLeftButton;

        #region Update
        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - _lastUpdate >= CSensitivity)
            {
                _lastUpdate = (long) gameTime.TotalGameTime.TotalMilliseconds;
                UpdateLeftButtonState();    
            }
            
        }

        private void UpdateLeftButtonState()
        {
            _previousCursorPosition = _currentCursosPosition;               

            _previousStateLeftBtn = _currentStateLeftButton;

            _currentStateLeftButton = Mouse.GetState().LeftButton;

            _currentCursosPosition.X = Mouse.GetState().X;
            _currentCursosPosition.Y = Mouse.GetState().Y;

            if (_previousStateLeftBtn != _currentStateLeftButton && _currentStateLeftButton == ButtonState.Pressed)
                OnMouseDown();
            else if (_previousStateLeftBtn != _currentStateLeftButton && _currentStateLeftButton == ButtonState.Released)
                OnMouseUp();
            else if (Mouse.GetState().LeftButton == ButtonState.Pressed && ((int)_previousCursorPosition.X !=  Mouse.GetState().X || (int)_previousCursorPosition.Y !=Mouse.GetState().Y))
                OnMouseMoveDown();
        }

        #endregion

        private void OnMouseDown()
        {
            MouseControllerEventArgs args = new MouseControllerEventArgs(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

            if (MouseDown != null)
                MouseDown(this, args);
        }

        private void OnMouseUp()
        {
            MouseControllerEventArgs args = new MouseControllerEventArgs(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

            if (MouseUp != null)
                MouseUp(this, args);
        }

        private void OnMouseMoveDown()
        {
            MouseControllerEventArgs args = new MouseControllerEventArgs(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

            if (MouseMoveDown != null)
                MouseMoveDown(this, args);
        }
    }    
}
