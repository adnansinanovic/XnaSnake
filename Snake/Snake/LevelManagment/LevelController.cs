using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.LevelManagment
{
    public class LevelController : GameComponent
    {
        private Game _game;
        private SpriteBatch _spriteBatch;
        private string _currentLevel;
        private readonly Dictionary<string, Level> _levels;
        private LevelIO _levelIO;
        private Rectangle _playground;        

        public LevelController(Game game, SpriteBatch spriteBatch, Rectangle playground, Sprite obstacleTexture) : base(game)
        {
            _game = game;
            _spriteBatch = spriteBatch;
            _playground = playground;

            _levels = new Dictionary<string, Level>();            

            _levelIO = new LevelIO(game, spriteBatch);
            _levels = _levelIO.Load(obstacleTexture);
                                 
            _levels.Add("DEFAULT", new Level(game, spriteBatch, "Default",0,0,new List<Obstacle>{}));            
            
            _currentLevel = "DEFAULT";
        }

        public List<Obstacle> Obstacles
        {
            get { return _levels[_currentLevel].Obstacles; }
        }

        public Dictionary<string, Level> Levels
        {
            get { return _levels; }            
        }

        public LevelObjective Objective
        {
            get { return _levels[_currentLevel].Objective; }
        }

        public string LevelName
        {
            get { return _levels[_currentLevel].Name; }
        }

        public void SaveLevel(List<Vector2> data, string levelName, string snakeLength, string score)
        {
            _levelIO.SaveLevel(data,levelName,snakeLength,score);
        }

        public void Draw(GameTime gameTime)
        {
            _levels[_currentLevel].Draw(gameTime);
        }
        

        public void ChangeLevel(string targetLevel)
        {
            if (_currentLevel != targetLevel && _levels.ContainsKey(targetLevel))
                _currentLevel = targetLevel;
        }

        public bool CheckWin(int score, int snakeLength)
        {
            return _levels[_currentLevel].CheckWin(score, snakeLength);
        }

        public void NextLevel()
        {                   
            bool nextLevelFound = false;

            foreach (KeyValuePair<string, Level> keyValuePair in _levels)
            {                                
                if (nextLevelFound)
                {
                    _currentLevel = keyValuePair.Key;
                    return;
                }
                
                if (_currentLevel == keyValuePair.Key)
                    nextLevelFound = true;
            }

            _currentLevel = "DEFAULT";
        }
    }
}
