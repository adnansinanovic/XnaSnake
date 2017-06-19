using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Buttons;
using Snake.Controllers.Buttons;
using Snake.HighscoreManagment;

namespace Snake.ScreenManagment
{
    class ScreenHighscore : BaseScreen
    {
        public ScreenHighscore(Game game, SpriteBatch spriteBatch, Screens screenType, Dictionary<LoadedFonts, SpriteFont> fonts) : base(game, spriteBatch, screenType, fonts)
        {
            Sprite regularNormal = new Sprite(SpriteBatch, Game.Content, @"Buttons/Regular_N");
            Sprite regularPressed = new Sprite(SpriteBatch, Game.Content, @"Buttons/Regular_P");

            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "BACK", regularPressed, regularNormal, new Vector2(10, 5), "BACK", this.Fonts[LoadedFonts.KristenITC]));
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (SpriteButton button in this.Buttons)            
                button.Draw();


            int startY = 70;
            int rowOffset = 30;

            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], "#", new Vector2(20, startY), Color.Black);

            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], this.Game1.GetMessage("PLAYER"), new Vector2(50, startY), Color.Black);

            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], this.Game1.GetMessage("LEVEL"), new Vector2(150, startY), Color.Black);

            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], this.Game1.GetMessage("SNAKE_LENGTH"), new Vector2(220, startY), Color.Black);

            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.CourierNew_fs8], this.Game1.GetMessage("SCORE"), new Vector2(320, startY), Color.Black);

            List<HighscoreItem> highscoreItems = HighscoreController.Highscores;

            for (int i=0; i<highscoreItems.Count && i<10; i++)
            {
                this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.KristenITC_fs10], (i + 1).ToString() + ".", new Vector2(20, startY + (i + 1) * rowOffset), Color.Black);

                this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.KristenITC_fs10], highscoreItems[i].PlayerName, new Vector2(50, startY + (i + 1) * rowOffset), Color.Black);

                this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.KristenITC_fs10], highscoreItems[i].Level, new Vector2(150, startY + (i + 1) * rowOffset), Color.Black);

                this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.KristenITC_fs10], highscoreItems[i].SnakeLength.ToString(), new Vector2(250, startY + (i + 1) * rowOffset), Color.Black);

                this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.KristenITC_fs10], highscoreItems[i].Score.ToString(), new Vector2(350, startY + (i + 1) * rowOffset), Color.Black);
            }
        }

        protected override void ButtonMouseUp(object sender, SpriteButtonEventArgs args)
        {
            switch (args.Name)
            {
                case "BACK":
                    this.Game1.ChangeScreen(Screens.Menu);
                    break;
            }
        }        
    }
}
