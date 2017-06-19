using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.ItemClasses;

namespace Snake
{
    public class BonusController
    {
        private const int CMinTime = 500;
        private const int CMaxTime = 3000;
        private Random _random = new Random((int) DateTime.UtcNow.Ticks);
        private Rectangle _playground;
        private readonly Dictionary<BonusItemType, BonusItem> _bonusItems;                    
        private SpriteFont _font;
        private Game1 _game;
        public Dictionary<BonusItemType, BonusItem> Items
        {
            get { return _bonusItems; }
        }

        public bool GhostMode
        {
            get { return _bonusItems[BonusItemType.Ghost].Active; }
        }

        public bool ReverseMode
        {
            get { return _bonusItems[BonusItemType.Reverse].Active; }
        }

        public BonusController(Game game, SpriteBatch spriteBatch, Rectangle playground, SpriteFont font)
        {
            _game = (Game1) game;
            _playground = playground;
            _font = font;
                    
            _bonusItems = new Dictionary<BonusItemType, BonusItem>();

            _bonusItems.Add(BonusItemType.Brake, new BonusItem(game, spriteBatch, BonusItemType.Brake, new Vector2(0, 0), new Sprite[] { new Sprite(spriteBatch, game.Content, "BonusItems/snail"), new Sprite(spriteBatch, game.Content, "BonusItems/snail_d") }, font));
            _bonusItems.Add(BonusItemType.SpeedUp, new BonusItem(game, spriteBatch, BonusItemType.SpeedUp, new Vector2(0, 0), new Sprite[] { new Sprite(spriteBatch, game.Content, @"BonusItems/fast"), new Sprite(spriteBatch, game.Content, @"BonusItems/fast_d") }, font));
            _bonusItems.Add(BonusItemType.Cut, new BonusItem(game, spriteBatch, BonusItemType.Cut, new Vector2(0, 0), new Sprite[] { new Sprite(spriteBatch, game.Content, @"BonusItems\saw"), new Sprite(spriteBatch, game.Content, @"BonusItems\saw_d") }, font));
            _bonusItems.Add(BonusItemType.MultiplierX2, new BonusItem(game, spriteBatch, BonusItemType.MultiplierX2, new Vector2(0, 0), new Sprite[] { new Sprite(spriteBatch, game.Content, @"BonusItems\x2"), new Sprite(spriteBatch, game.Content, @"BonusItems\x2_d") }, font));
            _bonusItems.Add(BonusItemType.MultiplierX3, new BonusItem(game, spriteBatch, BonusItemType.MultiplierX3, new Vector2(0, 0), new Sprite[] { new Sprite(spriteBatch, game.Content, @"BonusItems\x3"), new Sprite(spriteBatch, game.Content, @"BonusItems\x3_d") }, font));
            _bonusItems.Add(BonusItemType.ExtraPoints, new BonusItem(game, spriteBatch, BonusItemType.ExtraPoints, new Vector2(0, 0), new Sprite[] { new Sprite(spriteBatch, game.Content, @"BonusItems\chest"), new Sprite(spriteBatch, game.Content, @"BonusItems\chest_d"), }, font));
            _bonusItems.Add(BonusItemType.Reverse, new BonusItem(game, spriteBatch, BonusItemType.Reverse, new Vector2(0, 0), new Sprite[] { new Sprite(spriteBatch, game.Content, @"BonusItems\reverse"), new Sprite(spriteBatch, game.Content, @"BonusItems\reverse_d") }, font));
            _bonusItems.Add(BonusItemType.Ghost, new BonusItem(game, spriteBatch, BonusItemType.Ghost, new Vector2(0, 0), new Sprite[] { new Sprite(spriteBatch, game.Content, @"BonusItems\ghost"), new Sprite(spriteBatch, game.Content, @"BonusItems\ghost_d") }, font));

            int x = 250, y = 10, cntX = 1, cntY = 0, offsetY = 30, offsetX = 30;
            _bonusItems[BonusItemType.Brake].Initialize(new Vector2(x + cntX * offsetX, y + cntY * offsetY), 1, 10000, 15000, -5, 0, false, false, 0);
            cntX++;
            _bonusItems[BonusItemType.SpeedUp].Initialize(new Vector2(x+cntX*offsetX,y+cntY*offsetY), 1, 10000, 15000, 5, 0, false, false, 0);
            cntX++;
            _bonusItems[BonusItemType.Cut].Initialize(new Vector2(x + cntX * offsetX, y + cntY * offsetY), 1, 8000, 15000, 0, 0, false, false, 6);
            cntX++;
            _bonusItems[BonusItemType.MultiplierX2].Initialize(new Vector2(x + cntX * offsetX, y + cntY * offsetY), 2, 15000, 15000, 0, 0, false, false, 0);
            x = 250;
            cntX = 1;
            cntY++;
            _bonusItems[BonusItemType.MultiplierX3].Initialize(new Vector2(x + cntX * offsetX, y + cntY * offsetY), 3, 15000, 15000, 0, 0, false, false, 0);
            cntX++;
            _bonusItems[BonusItemType.ExtraPoints].Initialize(new Vector2(x + cntX * offsetX, y + cntY * offsetY), 1, 15000, 15000, 0, 10, false, false, 0);
            cntX++;
            _bonusItems[BonusItemType.Reverse].Initialize(new Vector2(x + cntX * offsetX, y + cntY * offsetY), 1, 10000, 15000, 0, 0, true, false, 0);
            cntX++;
            _bonusItems[BonusItemType.Ghost].Initialize(new Vector2(x + cntX * offsetX, y + cntY * offsetY), 0, 10000, 15000, 10, 0, false, true, 0);

            foreach (BonusItem item in _bonusItems.Values)
            {
                item.Activated += item_Activated;
                item.Deactivated += item_Deactivated;
            }            
        }

        private void item_Deactivated(BonusItem sender, BonusItemEventArgs args)
        {
            switch (sender.ItemType)
            {
                case BonusItemType.Brake:
                    _game.IncreseSpeedSnake(-sender.SpeedIncrease);
                    break;
                case BonusItemType.SpeedUp:
                    _game.IncreseSpeedSnake(-sender.SpeedIncrease);
                    break;
            }

        }

        void item_Activated(BonusItem sender, BonusItemEventArgs args)
        {
           switch (sender.ItemType)
           {
               case BonusItemType.Brake:
                   _game.IncreseSpeedSnake(sender.SpeedIncrease);
                   break;
               case BonusItemType.SpeedUp:
                   _game.IncreseSpeedSnake(sender.SpeedIncrease);
                   break;
           }
        }

        public void AddItemToPlayground()
        {
            Vector2 itemPosition = RandomPosition.Next(_playground);
            
            BonusItemType itemType = GetRandomItemType();
                     
            BonusItem bonusItem = _bonusItems[itemType];
                        
            bonusItem.SetPosition((int) itemPosition.X, (int) itemPosition.Y);          

            bonusItem.PlaceOnPlayground();
        }

        private BonusItemType GetRandomItemType()
        {
            return (BonusItemType)_random.Next(0, (int) BonusItemType.Count);
        }

        public void DrawInARow(int x, int y)
        {
            foreach (BonusItem item in _bonusItems.Values)
            {
                item.Draw(x,y);
                
                x += item.Width + 10;
            }
        }

        public void Draw()
        {
            foreach (BonusItem item in _bonusItems.Values)            
            {
                if (item.OnPlayground)
                    item.Draw();
                
                item.DrawActive();

            }
        } 
      
        public Dictionary<BonusItemType, BonusItem> GeActiveBonusItems()
        {
            Dictionary<BonusItemType, BonusItem> activeItems = new Dictionary<BonusItemType, BonusItem>();
            foreach (KeyValuePair<BonusItemType, BonusItem> item in _bonusItems)
            {
                if (item.Value.Active)
                    activeItems.Add(item.Key,item.Value);
            }
            return activeItems;
        }

        public void Reset()
        {
            foreach (BonusItem item in _bonusItems.Values)            
                item.Reset();            
        }
    }
}

