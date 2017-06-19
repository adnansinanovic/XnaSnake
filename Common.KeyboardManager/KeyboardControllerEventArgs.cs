using Microsoft.Xna.Framework.Input;

namespace Common.KeyboardManager
{
    public class KeyboardControllerEventArgs
    {
        private Keys _key;

        public KeyboardControllerEventArgs(Keys key)
        {
            _key = key;
        }

        public Keys Key
        {
            get { return _key; }
        }
    }
}
