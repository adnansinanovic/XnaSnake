using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.ItemClasses
{
    public class BonusItemEventArgs
    {
        
    }

    public delegate void BonusItemEventHandler(BonusItem sender, BonusItemEventArgs args);

    public class BonusItem : BaseSnakeItem
    {
        private readonly BonusItemType _itemType;
        private readonly SpriteFont _font;
        private Sprite _sprDeactivated;

        private int _timeActiveDuration;
        private int _timePlaygroundDuration;
        private int _timePlacedOnPlayground;
        private int _timeSettedAsActive;
        private int _timeCurrentTime;        

        private Vector2 _activePosition;
        
        private decimal _pointMultiplier;
        private int _speedIncrease;
        private int _extraPoints;
        private bool _reverse;
        private bool _ghost;        
        private bool _active;
        private bool _onPlayground;
        private int _cut;

        public event BonusItemEventHandler Activated;
        public event BonusItemEventHandler Deactivated;

        public BonusItemType ItemType
        {
            get { return _itemType; }
        }

        public int SpeedIncrease
        {
            get { return _speedIncrease; }
        }

        public int ExtraPoints
        {
            get { return _extraPoints; }
        }

        public bool Reverse
        {
            get { return _reverse; }
        }

        public bool Ghost
        {
            get { return _ghost; }
        }

        public int Cut
        {
            get { return _cut; }
        }

        public decimal PointMultiplier
        {
            get { return _pointMultiplier; }
        }


        public BonusItem(Game game, SpriteBatch spriteBatch, BonusItemType itemType,  Vector2 position, Sprite[] texture, SpriteFont font) : base(game, spriteBatch, position, texture[0])
        {
            _font = font;
            _itemType = itemType;
                       
            _active = false;
            _onPlayground = false;
            _sprDeactivated = texture[1];
        }

        public bool OnPlayground
        {
            get { return _onPlayground; }
        }

        public bool Active
        {
            get { return _active; }
        }

        private int CountDownActive
        {
            get
            {
                int result = ((_timeActiveDuration - (_timeCurrentTime - _timeSettedAsActive)) / 1000);
                return result > 0 ? result : 0;
            }
        }

        private int CountDownOnPlayground
        {
            get 
            {
                int result = ((_timePlaygroundDuration- (_timeCurrentTime - _timePlacedOnPlayground)) / 1000);
                return result > 0 ? result : 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pointMultiplier">Multiplies coin value.</param>
        /// <param name="activeDuration">When collected, how much it will be active (miliseconds)</param>
        /// <param name="durationOnPlayground">How much it will be visible on playground</param>
        /// <param name="speedIncrease">Increasing speed (pixels per second)</param>
        /// <param name="extraPoints">Gives extra points (once, when collected)</param>
        /// <param name="reverse">Tail becomes head</param>
        /// <param name="ghost">Snake will became ghost.</param>
        /// <param name="cut">It will make snake shorter</param>
        public void Initialize(Vector2 activePosition, decimal pointMultiplier, int activeDuration, int durationOnPlayground, int speedIncrease, int extraPoints, bool reverse, bool ghost, int cut)
        {
            _activePosition = activePosition;
            _pointMultiplier = pointMultiplier;
            _timeActiveDuration = activeDuration;
            _timePlaygroundDuration = durationOnPlayground;
            _speedIncrease = speedIncrease;            
            _extraPoints = extraPoints;
            _reverse = reverse;
            _ghost = ghost;
            _cut = cut;            
        }

        public void Activate()
        {
            if (!_active)
                _timeSettedAsActive = _timeCurrentTime;

            _active = true;

            if (Activated != null)
                Activated(this, new BonusItemEventArgs());
        }

        public void Deactivate()
        {
            _active = false;

            if (Deactivated != null)
                Deactivated(this, new BonusItemEventArgs());
        }

        public void DrawActive()
        {
            if(_active)
            {
                Draw((int)_activePosition.X, (int)_activePosition.Y);

                string seconds = CountDownActive.ToString();
                int x = (int)(_activePosition.X + (Width / 2) - (_font.MeasureString(seconds).X / 2));
                int y = (int)(_activePosition.Y + (Height));

                SpriteBatch.DrawString(_font, seconds.ToString(), new Vector2(x, y), Color.Red);                
            }
            else
                _sprDeactivated.Draw(_activePosition);
            
        }

        public override void Draw()
        {
            base.Draw();
            
            string seconds = CountDownOnPlayground.ToString();
            int x = (int) (X + (Width/2) - (_font.MeasureString(seconds).X/2));
            int y = (int) (Y + (Height));
            
            SpriteBatch.DrawString(_font, seconds.ToString(), new Vector2(x, y), Color.Red);            
        }

        public void AddDurationOnPlayground(int duration)
        {
            _timePlaygroundDuration += duration;
        }

        public void PlaceOnPlayground()
        {
            if (!_onPlayground)
                _timePlacedOnPlayground = _timeCurrentTime;

            _onPlayground = true;            
        }

        public void RemoveFromPlayground()
        {
            _onPlayground = false;
        }

        public override void Update(GameTime gameTime)
        {
            _timeCurrentTime = (int) gameTime.TotalGameTime.TotalMilliseconds;

            if (_onPlayground && CountDownOnPlayground==0)
                RemoveFromPlayground();

            if (_active && CountDownActive == 0)
                Deactivate();
        }

        public void Reset()
        {
            _active = false;
            _onPlayground = false;
        }
    }
}
