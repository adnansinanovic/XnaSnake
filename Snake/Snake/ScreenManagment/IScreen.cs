using Microsoft.Xna.Framework;

namespace Snake.ScreenManagment
{
    interface IScreen
    {
        void Start();
        void End();
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
    }
}
