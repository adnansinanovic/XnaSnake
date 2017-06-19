using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Snake
{   
    public class WinningBall : BaseSnakeItem
    {
        private int _points;
        private Rectangle _playground;

        public WinningBall(Game game, SpriteBatch spriteBatch, Sprite sprite, Vector2 position, Rectangle playground) : base(game, spriteBatch,position, sprite)
        {
            _points = 1;
            _playground = playground;
        }

        public int Points
        {
            get { return _points; }
        }

        public void ChangePosition()
        {
            Vector2 position = RandomPosition.Next(_playground);
            
        }

        public void ChangePosition(Snake snake, List<Obstacle> obstacles)
        {
            bool success = false;
            while (!success)
            {
                Vector2 position = RandomPosition.Next(_playground);
                Rectangle winningBallRectangle = new Rectangle((int) position.X, (int) position.Y, Width, Height);
                
                bool snakeColision = CheckColisionWithSnake(snake, winningBallRectangle);
                bool obstacleColision = CheckColisionWithObstacles(obstacles, winningBallRectangle);

                if (!snakeColision && !obstacleColision)
                {
                    success = true;
                    Position = position;
                }
            }
        }

        private bool CheckColisionWithObstacles(List<Obstacle> obstacles, Rectangle winningBallRectangle)
        {
            foreach (Obstacle obstacle in obstacles)
            {
                if (winningBallRectangle.Intersects(obstacle.Bounds))
                    return true;
            }

            return false;
        }

        private bool CheckColisionWithSnake(Snake snake, Rectangle winningBallRectangle)
        {
            foreach (SnakeTailItem tail in snake.Tail)
            {                
                if (tail.Bounds.Intersects(winningBallRectangle))
                    return true;
            }

            return false;
        }
    }
}
