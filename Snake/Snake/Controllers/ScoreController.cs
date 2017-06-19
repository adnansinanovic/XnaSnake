namespace Snake
{
    public class ScoreController
    {
        private int _score;

        public int Score
        {
            get { return _score; }
        }

        public ScoreController()
        {
            _score = 0;
        }

        public void AddScore()
        {
            AddScore(1);
        }

        public void AddScore(int extraPoints)
        {
            _score += extraPoints;
        }

        public void SetScore(int score)
        {
            _score = score;
        }

        public void Reset()
        {
            _score = 0;
        }
    }
}
