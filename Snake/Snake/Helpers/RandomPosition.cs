using System;
using Microsoft.Xna.Framework;

namespace Snake
{    
    static class RandomPosition
    {
        static Random _random = new Random((int)DateTime.UtcNow.Ticks);

        public static Vector2 Next(Rectangle surface)
        {
            int x = _random.Next(surface.X, surface.Right);
            int y = _random.Next(surface.Y, surface.Bottom);
            return new Vector2(x,y);
        }
    }
}
