
using Microsoft.Xna.Framework;

namespace Snake.LevelManagment
{
    public class LevelObjective : GameComponent
    {
        private int _snakeLength;
        private int _score;

        public int SnakeLength
        {
            get { return _snakeLength; }
        }

        public int Score
        {
            get { return _score; }
        }

        public LevelObjective(Game game, int snakeLength, int score) : base(game)
        {
            _snakeLength = snakeLength;
            _score = score;
        }

        public bool CheckWin(int score, int snakeLength)
        {
            if (_score == 0 && _snakeLength == 0)
                return false;

            if (_score == 0 && snakeLength >= _snakeLength)
                return true;

            if (_snakeLength == 0 && score >= _score)
                return true;

            if (score >= _score && snakeLength >= _snakeLength)
                return true;

            return false;
        }
    }
}
