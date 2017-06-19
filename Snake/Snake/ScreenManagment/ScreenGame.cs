using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.ItemClasses;
using Snake.LevelManagment;

namespace Snake.ScreenManagment
{
    public class ScreenGame : BaseScreen
    {
        private Sprite _sprHeader;

        #region Constants
        private int C2ndColumnX = 105;
        #endregion

        #region Properties
        protected LevelController LevelController
        {
            get { return this.Game1.LevelController; }
        }

        protected WinningBall WinningBall
        {
            get { return this.Game1.WinningBall; }
        }

        protected BonusController BonusController
        {
            get { return this.Game1.BonusController; }
        }

        protected ScoreController ScoreController
        {
            get { return this.Game1.ScoreController; }
        }

        protected Snake Snake
        {
            get { return this.Game1.Snake; }
        }

        protected LevelObjective LevelObjective
        {
            get { return this.Game1.LevelObjective; }
        }
        #endregion

        public ScreenGame(Game game, SpriteBatch spriteBatch, Screens screenType, Dictionary<LoadedFonts, SpriteFont> fonts)
            : base(game, spriteBatch, screenType, fonts)
        {
            _sprHeader = new Sprite(spriteBatch, game.Content, "Background/Header", new Vector2(0, 0));
        }
        
        public override void Draw(GameTime gameTime)
        {
            DrawHeader(gameTime);
            this.LevelController.Draw(gameTime);
            this.WinningBall.Draw();
            this.BonusController.Draw();
            this.Game1.Snake.Draw();            
        }

        protected void DrawHeader(GameTime gameTime)
        {
            _sprHeader.Draw();
            string score = string.Format("{0}/{1}", this.ScoreController.Score, this.LevelObjective.Score);
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], this.Game1.GetMessage("SCORE"), new Vector2(10, 10), Color.IndianRed);
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], score, new Vector2(C2ndColumnX, 10), Color.IndianRed);

            string snakeLentgh = string.Format("{0}/{1}", this.Snake.SnakeLength, this.LevelObjective.SnakeLength);
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], this.Game1.GetMessage("SNAKE_LENGTH"), new Vector2(10, 30), Color.IndianRed);
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], snakeLentgh, new Vector2(C2ndColumnX, 30), Color.IndianRed);
                        
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], this.Game1.GetMessage("SPEED"), new Vector2(10, 50), Color.IndianRed);
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], this.Snake.Speed.ToString("0.0", CultureInfo.InvariantCulture), new Vector2(C2ndColumnX, 50), Color.IndianRed);
                        
            int currentTime = (int)gameTime.TotalGameTime.TotalMilliseconds;
            int countDownBonus = this.Game1.TimeBonusItemUpdateFrequency - (currentTime - this.Game1.TimeBonusItemLastUpdate);
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], this.Game1.GetMessage("BONUS"), new Vector2(10, 70), Color.IndianRed);
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], (countDownBonus / 1000).ToString(), new Vector2(C2ndColumnX, 70), Color.IndianRed);
        }

        public override void Update(GameTime gameTime)
        {
            CheckWin();
            CheckCollisions();
            CheckBonusTimer(gameTime);
            CheckReverseMode();
        }

        private void CheckWin()
        {
            this.Game1.CheckWin();
        }

        private void CheckReverseMode()
        {
            this.Game1.SetSnakeReverseMode();
        }

        private void CheckBonusTimer(GameTime gameTime)
        {
            this.Game1.CheckBonusTimer(gameTime);
        }

        public void CheckCollisions()
        {
            if (!this.BonusController.GhostMode && (this.Snake.CheckCollisionWithItSelf() || this.Game1.CheckCollision_SnakeVsObstacles()))
            {
                this.Game1.ChangeScreen(Screens.GameOver);                
            }
            else if (this.Game1.CheckCollision_SnakeVsWinningBall())
            {               
                this.Snake.AddTail();                
                
                this.Game1.AddScore();

                this.Game1.ChangeWinnBallPosition();                
            }
            else
            {
                BonusItem bonusItem = this.Game1.CheckCollision_SnakeVsBonusItems();
                if (bonusItem != null)
                    this.Game1.BonusWin(bonusItem);
            }
        }

        protected void DrawOnCenterOfWindow(string text, SpriteFont font, Color color)
        {
            int x = this.Game1.GraphicsDevice.PresentationParameters.BackBufferWidth / 2;
            int y = this.Game1.GraphicsDevice.PresentationParameters.BackBufferHeight / 2;

            x -= Convert.ToInt32(font.MeasureString(text).X / 2);
            y -= Convert.ToInt32(font.MeasureString(text).Y / 2);

            this.SpriteBatch.DrawString(font, text, new Vector2(x, y), color);
        }

        public override void Start()
        {
            this.Game1.RunSnake();
            base.Start();
        }

        public override void End()
        {
            this.Game1.StopSnake();
            base.End();
        }
    }
}
