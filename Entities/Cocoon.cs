using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace Colozak.Entities
{
    public class Cocoon
    {
        public const int COCOON_WIDTH = 48;
        public const int COCOON_HEIGHT = 48;

        // private readonly Random _random;
        
        private Texture2D _texture;
        
        public Vector2 Position;

        public Cocoon(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Logging
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _texture, 
                Position, 
                null, 
                Color.White, 
                0f, 
                new Vector2(COCOON_WIDTH/2, COCOON_HEIGHT/2),
                Vector2.One, 
                SpriteEffects.None, 
                0f
            );
        }
    }
}