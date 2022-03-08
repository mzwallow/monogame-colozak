using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Colozak.Entities
{
    public class WallRight : Wall
    {
        private const int WALL_RIGHT_POS_X = 672;
        private const int WALL_RIGHT_POS_Y = 97;

        private const int TEXTURE_WIDTH = 24;
        private const int TEXTURE_HEIGHT = 575;

        private Texture2D _texture;

        public override Rectangle CollisionBox
        {
            get
            {
                Rectangle collisionBox = new Rectangle(
                    (int)Math.Round(Position.X), 
                    (int)Math.Round(Position.Y),
                    TEXTURE_WIDTH,
                    TEXTURE_HEIGHT
                );

                return collisionBox;
            }
        }

        public WallRight(Texture2D texture)
        {
            _texture = texture;
            Position = new Vector2(WALL_RIGHT_POS_X, WALL_RIGHT_POS_Y);
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, CollisionBox, Color.White);
        }
    }
}