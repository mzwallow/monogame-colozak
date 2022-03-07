using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace Colozak.Entities
{
    public class Ceiling : IGameEntity
    {
        private const int CEILING_POS_X = 288;
        private const int CEILING_POS_Y = 73;

        private const int TEXTURE_WIDTH = 384;
        private const int TEXTURE_HEIGHT = 24;

        private Texture2D _texture;

        public Vector2 Position;

        public Rectangle CollisionBox
        {
            get
            {
                Rectangle collisionBox = new Rectangle(
                    (int)Math.Round(Position.X),
                    (int)Math.Round(Position.Y),
                    TEXTURE_WIDTH,
                    TEXTURE_HEIGHT);

                return collisionBox;
            }
        }

        public Ceiling(Texture2D texture)
        {
            _texture = texture;
            Position = new Vector2(CEILING_POS_X, CEILING_POS_Y);
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, CollisionBox, Color.White);
        }

        public void DropCeiling()
        {
            Position.Y +=  Globals.TILE_SIZE - 6;
        }
    }
}