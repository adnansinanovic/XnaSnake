using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Buttons;
using Snake.Controllers.Buttons;

namespace Snake.ScreenManagment
{
    public class ScreenGameOver : ScreenGame
    {
        private Sprite _sprCrash;
        public ScreenGameOver(Game game, SpriteBatch spriteBatch, Screens screenType, Dictionary<LoadedFonts, SpriteFont> fonts) : base(game, spriteBatch, screenType, fonts)
        {
            _sprCrash = new Sprite(spriteBatch, game.Content, "crash");
            InitializeButtons();
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        private void InitializeButtons()
        {
            Sprite normal = new Sprite(SpriteBatch, Game.Content, @"Buttons/Cloud_N");
            Sprite pressed = new Sprite(SpriteBatch, Game.Content, @"Buttons/Cloud_P");
            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "EXIT", pressed, normal, new Vector2(220, 380), "EXIT", this.Fonts[LoadedFonts.KristenITC]));
            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "PLAYAGAIN", pressed, normal, new Vector2(20, 380), "PLAY_AGAIN", this.Fonts[LoadedFonts.KristenITC]));                                          
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHeader(gameTime);
            this.LevelController.Draw(gameTime);
            this.WinningBall.Draw();
            this.BonusController.Draw();
            this.Snake.Draw();
            _sprCrash.Draw(this.Game1.CrashSitePosition);
            DrawOnCenterOfWindow(this.Game1.GetMessage("GAME_OVER"), this.Fonts[LoadedFonts.KristenITC_fs24], Color.Black);
            DrawButtons(gameTime);
        }

        private void DrawButtons(GameTime gameTime)
        {
            foreach (SpriteButton spriteButton in this.Buttons)
            {
                spriteButton.Draw();
            }
        }
     

        protected override void ButtonMouseUp(object sender, SpriteButtonEventArgs args)
        {
            switch (args.Name)
            {
                case "EXIT":
                    this.Game1.ExitGame();
                    break;
                case "PLAYAGAIN":
                    this.Game1.PlayAgain();
                    break;              
            }
        }

        public override void Start()
        {
            EnableButtons();
            this.Started= true;
        }

        public override void End()
        {
            DisableButtons();
            this.Started = false;
        }
    }

    
}
