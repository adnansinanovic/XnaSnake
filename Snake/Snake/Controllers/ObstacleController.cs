using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
   

    public class ObstacleController1
    {
        private List<Obstacle> _obstacles;
        private Rectangle _playground;
        private Sprite _obstacleTexture;
        private SpriteBatch _spriteBatch;
        private Game _game;

        public ObstacleController1(Game game, SpriteBatch spriteBatch, Rectangle playground, Sprite obstacleTexture)
        {
            _game = game;
            _obstacles = new List<Obstacle>();
            _spriteBatch = spriteBatch;
            _obstacleTexture = obstacleTexture;
            _playground = playground;
        }

        public List<Obstacle> Obstacles
        {
            get { return _obstacles; }
        }

        public void AddObstacleRandom()
        {
            bool intersects = false;
            Obstacle temp;

            do
            {
                Vector2 position = RandomPosition.Next(_playground);
                temp = new Obstacle(_game, _spriteBatch, position, _obstacleTexture);

                foreach (Obstacle obstacle in _obstacles)
                    if (obstacle.Bounds.Intersects(temp.Bounds))
                        intersects = true;

            } while (intersects);

            AddObstacle(temp);
        }

        private void AddObstacle(Obstacle obstacle)
        {
            _obstacles.Add(obstacle);
        }

        public void AddObstacleLine(Vector2 startPosition, int count, Direction direction)
        {
            int offset = 0;
            float x = (int)startPosition.X;
            float y = (int)startPosition.Y;

            for (int i = 0; i < count; i++)
            {                
                AddObstacle(new Obstacle(_game, _spriteBatch, new Vector2(x, y), _obstacleTexture));

                switch (direction)
                {
                    case Direction.Left:
                        x = (int)(x - _obstacleTexture.Width - offset);
                        break;
                    case Direction.Right:
                        x = (int)(x + _obstacleTexture.Width + offset);
                        break;
                    case Direction.Down:
                        y = (int)(y + _obstacleTexture.Height + offset);
                        break;
                    case Direction.Up:
                        y = (int)(y - _obstacleTexture.Height - offset);
                        break;
                }
            }


        }

        public void Draw()
        {
            foreach (Obstacle obstacle in _obstacles)
                obstacle.Draw();
        }
    }
}
