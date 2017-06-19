using System;

namespace Snake.HighscoreManagment
{
    public class HighscoreItem : IComparable
    {
        private string _playerName;
        private int _score;
        private int _snakeLength;
        private string _level;

        public HighscoreItem(string playerName, int score, int snakeLength, string level)
        {
            _playerName = playerName;
            _score = score;
            _snakeLength = snakeLength;
            _level = level;
        }

        public string PlayerName
        {
            get { return _playerName; }
        }

        public int Score
        {
            get { return _score; }
        }

        public int SnakeLength
        {
            get { return _snakeLength; }
        }

        public string Level
        {
            get { return _level; }
        }
        

        public int CompareTo(object obj)
        {
            try
            {
                HighscoreItem c = obj as HighscoreItem;
                return _score.CompareTo(c.Score);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }

        public int CompareTo(HighscoreItem item, SortMethod sortMethod)
        {            
            switch (sortMethod)
            {
                case SortMethod.Ascending:
                    return _score.CompareTo(item.Score);
                break;
                case SortMethod.Descending:
                if (_score > item._score)
                    return -1;
                else if (_score < item._score)
                    return 1;
                else
                    return 0;
                break;
            }
            return 0;
        }
    }
}
