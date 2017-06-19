using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
    public class Sprite
    {
        private Vector2 _position = new Vector2(0, 0);
        private string _assetName;
        private Texture2D _texture;
        private SpriteBatch _spriteBatch;

        public Sprite(SpriteBatch spriteBatch, ContentManager content, string theAssetName, Vector2 position)
        {
            _spriteBatch = spriteBatch;
            LoadContent(content, theAssetName);
            _position = position;
            _assetName = theAssetName;            
        }

        public Sprite(SpriteBatch spriteBatch, ContentManager content, string theAssetName)
        {
            _spriteBatch = spriteBatch;
            LoadContent(content, theAssetName);
            _assetName = theAssetName;
        }

        public int Height
        {
            get { return _texture.Height; }
        }

        public int Width
        {
            get { return _texture.Width; }
        }

        public string AssetName
        {
            get { return _assetName; }
        }

        public void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            _texture = theContentManager.Load<Texture2D>(theAssetName);
        }

        public void Draw()
        {                        
            _spriteBatch.Draw(_texture, _position, Color.White);
        }

        public void Draw(int x, int y)
        {
            _position.X = x;
            _position.Y = y;
            Draw();
        }

        public void Draw(Vector2 position)
        {
            _position = position;
            Draw();
        }

        public Rectangle Bounds
        {
            get { return _texture.Bounds; }
        }        
    }

}
