using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Buttons;
using Snake.Controllers;
using Snake.Controllers.Buttons;
using Snake.Controllers.TextBox;
using Snake.SelectorComponenet;

namespace Snake.ScreenManagment
{
    class ScreenMenu : BaseScreen
    {
        private Selector _levelSelector;
        private TextBox _txtPlayerName;
        private LanguageController _languageController;

        public ScreenMenu(Game game, SpriteBatch spriteBatch, Screens screenType, Dictionary<LoadedFonts, SpriteFont> fonts)
            : base(game, spriteBatch, screenType, fonts)
        {
            _levelSelector = new Selector(game, spriteBatch, fonts, new Vector2(15, 250),"SELECT_LEVEL");
            InitializeButtons();
            _txtPlayerName = new TextBox(game, spriteBatch, this.Game1.GetMessage("PLAYER_NAME"), 10, new Vector2(240, 200), fonts[LoadedFonts.KristenITC_fs10]);
            _languageController= new LanguageController(game,spriteBatch,fonts[LoadedFonts.KristenITC]);
        }

        private void InitializeButtons()
        {
            Sprite normal = new Sprite(SpriteBatch, Game.Content, @"Buttons/Cloud_N");
            Sprite pressed = new Sprite(SpriteBatch, Game.Content, @"Buttons/Cloud_P");
            Sprite normal50 = new Sprite(SpriteBatch, Game.Content, @"Buttons/Cloud50_N");
            Sprite pressed50 = new Sprite(SpriteBatch, Game.Content, @"Buttons/Cloud50_P");

            Sprite squareSmallN = new Sprite(SpriteBatch, Game.Content, @"Buttons/SquareSmall_N");
            Sprite squareSmallP = new Sprite(SpriteBatch, Game.Content, @"Buttons/SquareSmall_P");

            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "START", pressed, normal, new Vector2(220, 380), "START", this.Fonts[LoadedFonts.KristenITC]));
            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "LVLEDITOR", pressed, normal, new Vector2(20, 380), "LEVEL_EDITOR", this.Fonts[LoadedFonts.KristenITC]));
            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "HIGHSCORE", pressed, normal, new Vector2(220, 10), "HIGHSCORES", this.Fonts[LoadedFonts.KristenITC]));

            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "PLUS_SPEED", pressed50, normal50, new Vector2(15, 10), "+", this.Fonts[LoadedFonts.KristenITC]));
            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "MINUS_SPEED", pressed50, normal50, new Vector2(15, 45), "-", this.Fonts[LoadedFonts.KristenITC]));

            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "PLUS_BF", pressed50, normal50, new Vector2(15, 100), "+", this.Fonts[LoadedFonts.KristenITC]));
            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "MINUS_BF", pressed50, normal50, new Vector2(15, 135), "-", this.Fonts[LoadedFonts.KristenITC]));                                         
        }

        protected override void ButtonMouseUp(object sender, SpriteButtonEventArgs args)
        {
            switch (args.Name)
            {
                case "START":
                    this.Game1.ChangeScreen(Screens.Game);
                    break;
                case "LVLEDITOR":
                    this.Game1.ChangeScreen(Screens.LevelEditor);
                    break;
                    
                case "PLUS_SPEED":
                    this.Game1.IncreseSpeedSnake(1);
                    break;
                case "MINUS_SPEED":
                    this.Game1.IncreseSpeedSnake(-1);
                    break;
                case "PLUS_BF":
                    this.Game1.TimeBonusItemUpdateFrequency += 1000;
                    break;
                case "MINUS_BF":
                    if (this.Game1.TimeBonusItemUpdateFrequency - 1000 >= 5000)
                        this.Game1.TimeBonusItemUpdateFrequency -= 1000;
                    break;               
                case "HIGHSCORE":
                    this.Game1.ChangeScreen(Screens.Highscores);
                    break;                    
            }
        }
     
        public override void Draw(GameTime gameTime)
        {
            foreach (SpriteButton spriteButton in this.Buttons)
                spriteButton.Draw();

            _levelSelector.Draw(gameTime);

            string speed = string.Format("{0}: {1}", this.Game1.GetMessage("SPEED"), this.Game1.Snake.Speed.ToString("0.0"));
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.KristenITC_fs18], speed, new Vector2(60, 30), Color.Black);

            string bonus = string.Format("{0}: {1} {2}", this.Game1.GetMessage("BONUS_FREQEUNCY"), this.Game1.TimeBonusItemUpdateFrequency / 1000, this.Game1.GetMessage("SEC_ABR"));
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.KristenITC_fs18], bonus, new Vector2(60, 120), Color.Black);


            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.KristenITC_fs10], "version: pre-alpha", new Vector2(250, 480), Color.Black);                                       

            _txtPlayerName.Draw(gameTime);

            _languageController.Draw(gameTime);
        }

        public override void Start()
        {
            _levelSelector.Enable();
            _txtPlayerName.Enable();
            _languageController.Enable();  
            base.Start();
        }

        public override void End()
        {
            _levelSelector.Disable();
            _txtPlayerName.Disable();
            _languageController.Disable();
            base.End();
        }

        
        public override void Update(GameTime gameTime)
        {            
            this.Game1.ChangeLevel(_levelSelector.SelectedKeyValue);
            this.Game1.SetPlayerName(_txtPlayerName.Text);
            _txtPlayerName.Update(gameTime);
        }
    }
}
