using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.LevelManagment
{
    public class Level : GameComponent
    {
        private SpriteBatch _spriteBatch;
        private Game _game;
        private string _name;
        private List<Obstacle> _obstacles;
        private LevelObjective _objective;        

        public Level(Game game, SpriteBatch spriteBatch, string name, int snakeLengthGoal, int scoreGoal, List<Obstacle> obstacles) : base(game)
        {
            _spriteBatch = spriteBatch;
            _game = game;

            _name = name;
            _objective = new LevelObjective(game, snakeLengthGoal, scoreGoal);
            _obstacles = obstacles;
        }

        public List<Obstacle> Obstacles
        {
            get { return _obstacles; }            
        }

        public string Name
        {
            get { return _name; }            
        }

        public LevelObjective Objective
        {
            get { return _objective; }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Obstacle obstacle in _obstacles)            
                obstacle.Draw();            
        }

        public bool CheckWin(int score, int snakeLength)
        {            
            return _objective.CheckWin(score, snakeLength);
        }
    }
}
