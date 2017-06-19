using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Buttons;
using Snake.Controllers.Buttons;
using Snake.Controllers.TextBox;

namespace Snake.ScreenManagment
{
    class ScreenLevelEditor : BaseScreen
    {
        private Dictionary<int, SpriteButtonSelectable> _buttonSet;
        private int _scoreGoal;
        private int _snakeLengthGoal;
        private TextBox _txtLevelName;

        public ScreenLevelEditor(Game game, SpriteBatch spriteBatch, Screens screenType, Dictionary<LoadedFonts, SpriteFont> fonts) : base(game, spriteBatch, screenType, fonts)
        {
            InitializeButtons();      
            _txtLevelName = new TextBox(game,spriteBatch,"LEVEL_NAME",15,new Vector2(5,47),this.Fonts[LoadedFonts.KristenITC_fs10]);
        }

        private void Reset()
        {
            _scoreGoal = 250;
            _snakeLengthGoal = 50;
            foreach (SpriteButtonSelectable btn in _buttonSet.Values)            
                btn.Reset();
            
        }

        private void InitializeButtons()
        {
            Sprite regularNormal = new Sprite(SpriteBatch, Game.Content, @"Buttons/Regular_N");
            Sprite regularPressed = new Sprite(SpriteBatch, Game.Content, @"Buttons/Regular_P");

            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "BACK", regularPressed, regularNormal, new Vector2(5, 0), this.Game1.GetMessage("BACK"), this.Fonts[LoadedFonts.KristenITC]));
            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "SAVE", regularPressed, regularNormal, new Vector2(145, 0), this.Game1.GetMessage("SAVE"), this.Fonts[LoadedFonts.KristenITC]));

            Sprite squareN = new Sprite(SpriteBatch, Game.Content, @"Buttons/SquareSmall_N");
            Sprite squareP = new Sprite(SpriteBatch, Game.Content, @"Buttons/SquareSmall_P");
            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "SCOREPLUS", squareP, squareN, new Vector2(340, 0), this.Game1.GetMessage("+"), this.Fonts[LoadedFonts.KristenITC]));
            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "SCOREMINUS", squareP, squareN, new Vector2(310, 0), this.Game1.GetMessage("-"), this.Fonts[LoadedFonts.KristenITC]));

            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "SNAKEPLUS", squareP, squareN, new Vector2(340, 50), this.Game1.GetMessage("+"), this.Fonts[LoadedFonts.KristenITC]));
            this.Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "SNAKEMINUS", squareP, squareN, new Vector2(310, 50), this.Game1.GetMessage("-"), this.Fonts[LoadedFonts.KristenITC]));

            _buttonSet = new Dictionary<int, SpriteButtonSelectable>();

            Sprite normal = new Sprite(SpriteBatch, Game.Content, @"bcg");
            Sprite pressed = new Sprite(SpriteBatch, Game.Content, @"red");

            int x = 0, y = 100, cnt = 0;
            for (int i = 0; i < 40; i++)
            {
                for (int j=0; j<40; j++)
                {
                    _buttonSet.Add(cnt, new SpriteButtonSelectable(this.Game, this.SpriteBatch, cnt.ToString(), pressed, normal, new Vector2(x, y), "", this.Fonts[LoadedFonts.SegoeUIMono]));
                    cnt++;
                    x += 10;
                }
                y += 10;
                x = 0;
            }
            Buttons.Add(new SpriteButton(this.Game, SpriteBatch, "START", pressed, normal, new Vector2(220, 380), "", this.Fonts[LoadedFonts.KristenITC]));
            DisableButtons();
        }

        public override void Draw(GameTime gameTime)
        {
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.KristenITC_fs10], this.Game1.GetMessage("GOAL").ToUpper(), new Vector2(20, 55), Color.Black);

            string msg = string.Format("{0} {1}", this.Game1.GetMessage("SCORE"), _scoreGoal);
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.KristenITC_fs10],msg,new Vector2(310,30),Color.Black );

            msg = string.Format("{0} {1}", this.Game1.GetMessage("SNAKE_LENGTH"), _snakeLengthGoal);
            this.SpriteBatch.DrawString(this.Fonts[LoadedFonts.KristenITC_fs10], msg, new Vector2(280, 80), Color.Black);

            foreach (SpriteButtonSelectable btn in _buttonSet.Values)            
                btn.Draw();

            foreach (SpriteButton button in this.Buttons)            
                button.Draw();            
            
            _txtLevelName.Draw(gameTime);
        }

        public override void Start()
        {
            Reset();
            this.Started = true;            
            EnableButtons();            
            _txtLevelName.Enable();
        }

        public override void End()
        {
            this.Started = false;
            DisableButtons();
            _txtLevelName.Disable();
        }

        protected override void EnableButtons()
        {
            foreach (SpriteButtonSelectable btn in _buttonSet.Values)            
                btn.Enable();

            foreach (SpriteButton button in this.Buttons)
            {
                button.Enable();
                button.MouseUp += (ButtonMouseUp);
            } 
        }

        protected override void DisableButtons()
        {
            foreach (SpriteButtonSelectable btn in _buttonSet.Values)            
                btn.Disable();                
            

            foreach (SpriteButton button in this.Buttons)
            {
                button.Disable();
                button.MouseUp -= (ButtonMouseUp);
            }
        }

        protected override void ButtonMouseUp(object sender, SpriteButtonEventArgs args)
        {
            switch (args.Name)
            {
                case "BACK":
                    this.Game1.ChangeScreen(Screens.Menu);
                    break;
                case "SAVE":
                    SaveData();
                    break;
                case "SCOREPLUS":
                    ChangeScoreObjective(1);
                    break;
                case "SCOREMINUS":
                    ChangeScoreObjective(-1);
                    break;
                case "SNAKEPLUS":
                    ChangeSnakeLength(1);
                    break;
                case "SNAKEMINUS":
                    ChangeSnakeLength(-1);
                    break;                    
            }
        }

        private void ChangeSnakeLength(int amount)
        {
            if (_snakeLengthGoal + amount >= 0)
                _snakeLengthGoal += amount;
        }

        private void ChangeScoreObjective(int amount)
        {
            if (_scoreGoal + amount >= 0)
                _scoreGoal += amount;
        }

        private void SaveData()
        {
            List<Vector2> data = GetData();            

            this.Game1.SaveLevelData(data, _txtLevelName.Text, _snakeLengthGoal,_scoreGoal);
        }

        private List<Vector2> GetData()
        {       
            List<Vector2> positions = new List<Vector2>();
            foreach (SpriteButtonSelectable btn in _buttonSet.Values)
            {
                if (btn.Selected)
                    positions.Add(btn.Position);
            }

            return positions;
        }
    }
}
