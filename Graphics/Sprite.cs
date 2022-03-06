using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Colozak.Graphics
{
    public class Sprite
    {
        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public Color TintColor { get; set; } = Color.White;

        public Sprite(Texture2D texture, Vector2 position, int width, int height)
        {
            Texture = texture;
            Position = position;
            Width = width;
            Height = height;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // spriteBatch.Draw(Texture, position, new Rectangle(X, Y, Width, Height), TintColor);
        }

    }
}