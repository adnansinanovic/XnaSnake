
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Common.KeyboardManager
{
    public delegate void KeyboardControllerEventHandler(object sender, KeyboardControllerEventArgs args);
    
    public class KeyboardController
    {
        public event KeyboardControllerEventHandler KeyPressed;

        private static KeyboardController _instance;

        public static KeyboardController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new KeyboardController();

                return _instance;
            }
        }

        private KeyboardController()
        {
            _currentState = Keyboard.GetState();
            _previousState = Keyboard.GetState();    
            _lastUpdate = 0;
        }

        private KeyboardState _currentState;
        private KeyboardState _previousState;
      
        private long _lastUpdate; //miliseconds
        private const int CSensitivity = 100;  //lower value, greater senistivity

        public void Update(GameTime gameTime)
        {
            _currentState = Keyboard.GetState();

            if (gameTime.TotalGameTime.TotalMilliseconds - _lastUpdate >= CSensitivity)
            {
                foreach (Keys pressedKey in _currentState.GetPressedKeys())
                {
                    if (!_previousState.IsKeyDown(pressedKey))
                    {
                        if (KeyPressed != null)
                            KeyPressed.Invoke(this,new KeyboardControllerEventArgs(pressedKey));
                    }
                }

                _previousState = _currentState;
                _lastUpdate = (long) gameTime.TotalGameTime.TotalMilliseconds;
            }            
        }        
    }
}
