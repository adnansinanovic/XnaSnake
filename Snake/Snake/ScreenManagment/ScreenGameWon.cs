using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Buttons;
using Snake.Controllers.Buttons;

namespace Snake.ScreenManagment
{
    public class ScreenGameWon : ScreenGame
    {
        public ScreenGameWon(Game game, SpriteBatch spriteBatch, Screens screenType, Dictionary<LoadedFonts, SpriteFont> fonts) : base(game, spriteBatch, screenType, fonts)
        {
            Sprite normal = new Sprite(SpriteBatch, Game.Content, @"Buttons/Cloud_N");
            Sprite pressed = new Sprite(SpriteBatch, Game.Content, @"Buttons/Cloud_P");
            this.Buttons.Add(new SpriteButton(game, SpriteBatch, "NEXT", pressed, normal, new Vector2(220, 380), this.Game1.GetMessage("NEXT_LEVEL"), this.Fonts[LoadedFonts.KristenITC]));
        }

        public override void Draw(GameTime gameTime)
        {
            DrawHeader(gameTime);
            this.LevelController.Draw(gameTime);
            this.WinningBall.Draw();
            this.BonusController.Draw();
            this.Snake.Draw();            
            
            DrawOnCenterOfWindow(this.Game1.GetMessage("WIN") + " !!!", this.Fonts[LoadedFonts.KristenITC_fs24], Color.Black);
            
            foreach (SpriteButton button in this.Buttons)            
                button.Draw();
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        protected override void ButtonMouseUp(object sender, SpriteButtonEventArgs args)
        {
            switch(args.Name)
            {
                case "NEXT":
                    this.Game1.NextLevel();
                    break;
            }
        }

        public override void End()
        {
            base.End();
        }

        public override void Start()
        {            
            base.Start();
            this.Game1.StopSnake();
        }
    }
}
