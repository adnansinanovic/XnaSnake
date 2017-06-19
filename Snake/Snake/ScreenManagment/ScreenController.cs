using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.ScreenManagment
{
    internal class ScreenController : GameComponent
    {
        private SortedDictionary<Screens, IScreen> _screens;
        private SpriteBatch _spriteBatch;
        private Dictionary<LoadedFonts, SpriteFont> _fonts;
        private Game _game;
        private Screens _currentScreen;
        private Sprite _sprBackground;

        public ScreenController(Game game, SpriteBatch spriteBatch, Dictionary<LoadedFonts, SpriteFont> fonts) : base(game)
        {
            _screens = new SortedDictionary<Screens, IScreen>();
            
            _game = game;
            _spriteBatch = spriteBatch;
            _fonts = fonts;            
            
            _sprBackground = new Sprite(_spriteBatch, game.Content, "Background/Background_1", new Vector2(0, 0));

            InitializeScreens();            
        }

        private void InitializeScreens()
        {            
            _screens.Add(Screens.Menu, new ScreenMenu(_game, _spriteBatch, Screens.Menu, _fonts));
            _screens.Add(Screens.Game, new ScreenGame(_game, _spriteBatch, Screens.Game, _fonts));
            _screens.Add(Screens.GameOver, new ScreenGameOver(_game, _spriteBatch, Screens.GameOver, _fonts));
            _screens.Add(Screens.GameWin, new ScreenGameWon(_game, _spriteBatch, Screens.GameWin, _fonts));
            _screens.Add(Screens.LevelEditor, new ScreenLevelEditor(_game, _spriteBatch, Screens.LevelEditor, _fonts));
            _screens.Add(Screens.Highscores, new ScreenHighscore(_game, _spriteBatch, Screens.Highscores, _fonts));
                        
            ChangeScreen(Screens.Menu);
        }

        public void ChangeScreen(Screens screen)
        {
            _screens[_currentScreen].End();
            
            _currentScreen = screen;
            
            _screens[_currentScreen].Start();
        }

        public void Draw(GameTime gameTime)
        {
            _sprBackground.Draw();

            _screens[_currentScreen].Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            _screens[_currentScreen].Update(gameTime);            
        }       
    }    
}
