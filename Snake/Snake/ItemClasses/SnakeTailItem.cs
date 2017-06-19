using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
    public class SnakeTailItem : BaseSnakeItem
    {
        public SnakeTailItem(Game game, SpriteBatch spriteBatch, Vector2 position, Sprite texture) : base(game, spriteBatch, position, texture)
        {
        }        
    }
}
