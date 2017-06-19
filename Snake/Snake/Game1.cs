using System;
using System.Collections.Generic;
using System.IO;
using Common.KeyboardManager;
using Common.MouseManager;
using Common.TranslationManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Controllers.TextBox;
using Snake.ItemClasses;
using Snake.LevelManagment;
using Snake.ScreenManagment;
using Snake.SelectorComponenet;

namespace Snake
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Fields

        private GraphicsDeviceManager _graphics;
        private GameComponentCollection _components;
        private SpriteBatch _spriteBatch;
        public Dictionary<LoadedFonts, SpriteFont> _fonts;
        private TranslationController _translationController;
        private KeyboardController _keyboardController;
        private MouseController _mouseController;
        private LevelController _levelController;
        private ScoreController _scoreController;
        private WinningBall _winningBall;
        private BonusController _bonusController;
        private ScreenController _screenController;

        
        private Sprite _sprBlueBall;
        private Sprite _sprRedBall;
        private Sprite _sprYellowCoin;
        

        private Snake _snake;
        private Rectangle _playground;
        private const int CTitleBarOffset = 10;
        private const int CTitleBarHeight = 100;

        private int _timeBonusItemLastUpdate = 0;
        private int _timeBonusItemUpdateFrequency = 15000;

        #endregion

        #region Properties

        public int TimeBonusItemUpdateFrequency
        {
            get { return _timeBonusItemUpdateFrequency; }
            set { _timeBonusItemUpdateFrequency = value; }
        }

        public int TimeBonusItemLastUpdate
        {
            get { return _timeBonusItemLastUpdate; }
            set { _timeBonusItemLastUpdate = value; }
        }

        public Snake Snake
        {
            get { return _snake; }
            set { _snake = value; }
        }

        public ScoreController ScoreController
        {
            get { return _scoreController; }
        }

        public BonusController BonusController
        {
            get { return _bonusController; }
        }

        public LevelController LevelController
        {
            get { return _levelController; }
        }

        public WinningBall WinningBall
        {
            get { return _winningBall; }
        }


        public Vector2 CrashSitePosition
        {
            get { return _snake.Tail[0].Position; }
        }

        public LevelObjective LevelObjective
        {
            get { return _levelController.Objective; }            
        }

        #endregion



        public Game1()
        {
            debugtext("GAME CREATE start");
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.PreferredBackBufferWidth = 400;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _mouseController = MouseController.Instance;
            _keyboardController = KeyboardController.Instance;
            _keyboardController.KeyPressed += new KeyboardControllerEventHandler(KeyboardController_OnKeyPressed);

            _timeBonusItemLastUpdate = _timeBonusItemUpdateFrequency;
            debugtext("GAME CREATED");
        }

        public void debugtext(string t)
        {
            File.AppendAllText("ERROR.txt", t + Environment.NewLine);
        }

        void KeyboardController_OnKeyPressed(object sender, KeyboardControllerEventArgs args)
        {
            switch (args.Key)
            {
#if DEBUG
                case Keys.PageDown:
                    _snake.RemoveTail();
                    break;
                case Keys.PageUp:
                    _snake.AddTail();
                    break;
                case Keys.A:
                    _bonusController.AddItemToPlayground();
                    break;
                case Keys.R:
                    _snake.Reverse();
                    break;
                case Keys.S:
                    _snake.IncreaseSpeed(1);
                    break;
#endif
            }
        }

        public static string _exception;
        protected override void LoadContent()
        {            
            try
            {
                _spriteBatch = new SpriteBatch(GraphicsDevice);
                LoadFonts();
                _exception += "FONTS LOADED" + Environment.NewLine;
                _sprBlueBall = new Sprite(_spriteBatch, Content, "blue");
                _sprRedBall = new Sprite(_spriteBatch, Content, "red");
                _sprYellowCoin = new Sprite(_spriteBatch, Content, "coin");

                int windowHeight = _spriteBatch.GraphicsDevice.PresentationParameters.BackBufferHeight;
                int windowWidth = _spriteBatch.GraphicsDevice.PresentationParameters.BackBufferWidth;
                _playground = new Rectangle(0, CTitleBarHeight, windowWidth - 10, windowHeight - CTitleBarHeight - CTitleBarOffset);


                _translationController = TranslationController.Instance;
                _translationController.SetLanguage("BA");

                _exception += "TRANSLATIONS LOADED" + Environment.NewLine;
                _snake = new Snake(this, _sprBlueBall, _spriteBatch, _playground);
                _bonusController = new BonusController(this, _spriteBatch, _playground, _fonts[LoadedFonts.CourierNew_fs8]);
                _levelController = new LevelController(this, _spriteBatch, _playground, _sprRedBall);
                _scoreController = new ScoreController();
                _winningBall = new WinningBall(this, _spriteBatch, _sprYellowCoin, new Vector2(0, 0), _playground);
                _screenController = new ScreenController(this, _spriteBatch, _fonts);
                _exception += "CONTROLLERS LOADED" + Environment.NewLine;


                InitializeComponents();
                ChangeWinnBallPosition();
                _exception += "COMPONENETS" + Environment.NewLine;

                HighscoreManagment.HighscoreController.Load();
                _exception += "HIGHSCORES LOADED" + Environment.NewLine;
            }
            catch (Exception ex)
            {
                File.AppendAllText("ERROR.txt",_exception);
                throw ex;
                Environment.Exit(0);
            }

            File.AppendAllText("ERROR.txt", _exception);
            
        }    
      
        

        public string GetMessage(string key)
        {
            return _translationController.GetMessage(key);
        }

        private void InitializeComponents()
        {
            _components = new GameComponentCollection();
            _components.Add(_snake);
            _components.Add(_screenController);

            foreach (BonusItem bonusItem in _bonusController.Items.Values)
                _components.Add(bonusItem);
        }

        private void LoadFonts()
        {
            _fonts = new Dictionary<LoadedFonts, SpriteFont>();

            _fonts.Add(LoadedFonts.SegoeUIMono, Content.Load<SpriteFont>("Fonts/SegoeUIMono"));
            _fonts.Add(LoadedFonts.KristenITC, Content.Load<SpriteFont>("Fonts/KristenITC"));
            _fonts.Add(LoadedFonts.CourierNew_fs8, Content.Load<SpriteFont>("Fonts/CourierNew_fs8"));
            _fonts.Add(LoadedFonts.KristenITC_fs24, Content.Load<SpriteFont>("Fonts/KristenITC_fs24"));
            _fonts.Add(LoadedFonts.KristenITC_fs18, Content.Load<SpriteFont>("Fonts/KristenITC_fs18"));
            _fonts.Add(LoadedFonts.KristenITC_fs10, Content.Load<SpriteFont>("Fonts/KristenITC_fs10"));
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            _mouseController.Update(gameTime);
            _keyboardController.Update(gameTime);

            foreach (GameComponent gameComponent in _components)
                gameComponent.Update(gameTime);           

            base.Update(gameTime);
        }        

        public void BonusWin(BonusItem bonusItem)
        {
            bonusItem.RemoveFromPlayground();
            bonusItem.Activate();
        }

        #region Check Collision
        public BonusItem CheckCollision_SnakeVsBonusItems()
        {
            foreach (BonusItem bonusItem in _bonusController.Items.Values)
            {
                if (bonusItem.OnPlayground)
                {
                    if (_snake.Tail[0].Bounds.Intersects(bonusItem.Bounds))
                        return bonusItem;
                }
            }

            return null;
        }

        public bool CheckCollision_SnakeVsWinningBall()
        {
            return (_winningBall.Bounds.Intersects(_snake.Tail[0].Bounds));
        }

        public bool CheckCollision_SnakeVsObstacles()
        {
            foreach (Obstacle obstacle in _levelController.Obstacles)
            {
                if (_snake.Tail[0].Bounds.Intersects(obstacle.Bounds))
                    return true;
            }

            return false;
        }
        
        #endregion
        public void ChangeScreen(Screens targetScreen)
        {
            _screenController.ChangeScreen(targetScreen);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _screenController.Draw(gameTime);

            DrawDebugInfo(gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawDebugInfo(GameTime time)
        {
            return;
            try
            {
                int x = 10, y = 10, offset = 25;

                string fps = decimal.Round((decimal)(1 / time.ElapsedGameTime.TotalSeconds), 2).ToString();

                _spriteBatch.DrawString(_fonts[LoadedFonts.SegoeUIMono], fps, new Vector2(x, y), Color.Red);
                y += offset;

                _spriteBatch.DrawString(_fonts[LoadedFonts.SegoeUIMono], Mouse.GetState().X + " x " + Mouse.GetState().Y, new Vector2(x, y), Color.Red);
                y += offset;

                //_spriteBatch.DrawString(_fonts[LoadedFonts.SegoeUIMono], "Screen: " + _currentScreen, new Vector2(x, y), Color.Red);
                //y += offset;

                //foreach (SnakeTailItem tail in _snake.Tail)
                //{
                //    _spriteBatch.DrawString(_fonts[LoadedFonts.SegoeUIMono], (tail.X + ", " + tail.Y), new Vector2(x, y), Color.DarkGreen);
                //    y += offset;
                //}
                _spriteBatch.DrawString(_fonts[LoadedFonts.SegoeUIMono], "Real speed: " + _snake.Speed.ToString(), new Vector2(x, y), Color.DarkGreen);
                y += offset;
                //pixels per seconds
                decimal speed = (1 / ((decimal)_snake.Speed / (decimal)1000)) * 10;
                _spriteBatch.DrawString(_fonts[LoadedFonts.KristenITC], "Speed (px/sec): " + speed.ToString(), new Vector2(x, y), Color.DarkGreen);
                y += offset;

                _spriteBatch.DrawString(_fonts[LoadedFonts.SegoeUIMono], "Score: " + _scoreController.Score.ToString(), new Vector2(x, y), Color.DarkGreen);

            }
            catch (Exception)
            {

            }

        }

        public void ChangeWinnBallPosition()
        {
            _winningBall.ChangePosition(this.Snake, this.LevelController.Obstacles);
        }

        public void AddScore()
        {
            int pointsWon = this.WinningBall.Points;

            Dictionary<BonusItemType, BonusItem> activeBonusItems = GetActiveBonusItems();

            foreach (BonusItem item in activeBonusItems.Values)
            {
                pointsWon = (int)(pointsWon * item.PointMultiplier);
                pointsWon += item.ExtraPoints;

                RemoveTail(item.Cut);

                //IncreseSpeedSnake(item.SpeedIncrease);                
            }

            _scoreController.AddScore(pointsWon);
        }

        public void RemoveTail(int cut)
        {
            this.Snake.RemoveTail(cut);
        }

        public void IncreseSpeedSnake(int speedIncrease)
        {
            _snake.IncreaseSpeed(speedIncrease);
        }

        public void ReverseSnake()
        {            
            _snake.Reverse();
        }

        public Dictionary<BonusItemType, BonusItem> GetActiveBonusItems()
        {
           return this.BonusController.GeActiveBonusItems();
        }

        public void CheckBonusTimer(GameTime gameTime)
        {
            int currentTime = (int)gameTime.TotalGameTime.TotalMilliseconds;

            if (currentTime - _timeBonusItemLastUpdate >= _timeBonusItemUpdateFrequency)
            {
                _bonusController.AddItemToPlayground();
                _timeBonusItemLastUpdate = currentTime;
            }  
        }

        public void PlayAgain()
        {
            SaveGameResult();
            this.ChangeScreen(Screens.Menu);
            ResetGameVariables();
        }

        private void ResetGameVariables()
        {            
            _scoreController.Reset();
            _bonusController.Reset();
            _snake.Reset();
        }

        public void RunSnake()
        {
            _snake.Run();
        }

        public void StopSnake()
        {
            _snake.Stop();
        }

        private bool _snakeReverseMode = false;
        private string _playerName;

        public void SetSnakeReverseMode()
        {
            if (_snakeReverseMode != _bonusController.ReverseMode)
            {
                _snakeReverseMode = _bonusController.ReverseMode;
                _snake.Reverse();
            }
        }

        public void SaveLevelData(List<Vector2> data, string levelName, int snakeLength, int score)
        {
            _levelController.SaveLevel(data, levelName, snakeLength.ToString(), score.ToString());            
        }

        public void ChangeLevel(string targetLevel)
        {            
            _levelController.ChangeLevel(targetLevel);
        }

        public Dictionary<string, Level> GetLevels()
        {
            return _levelController.Levels;
        }

        public void CheckWin()
        {
            bool win =_levelController.CheckWin(_scoreController.Score, _snake.SnakeLength);
            if (win)
                ChangeScreen(Screens.GameWin);
        }

        public void NextLevel()
        {        
            SaveGameResult();
            ResetGameVariables();
            if (_levelController.Levels.Count > 1)
            {
                _levelController.NextLevel();
                ChangeScreen(Screens.Game);
            }
            else
                ChangeScreen(Screens.Menu);
            
        }

        public void SaveGameResult()
        {
            HighscoreManagment.HighscoreController.Save(_playerName,_levelController.LevelName, _snake.SnakeLength, _scoreController.Score);
        }

        public void ExitGame()
        {
            SaveGameResult();
            this.Exit();
        }

        public void ChangeLanguage(string language)
        {
            _translationController.SetLanguage(language);
        }

        public void SetPlayerName(string text)
        {
            _playerName = text;
        }
    }
}

