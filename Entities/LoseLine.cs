using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Colozak.Entities
{
    public class LoseLine : Wall
    {
        private const int LOSE_LINE_POS_X = 288;
        private const int LOSE_LINE_POS_Y = 570;

        private const int TEXTURE_WIDTH = 384;
        private const int TEXTURE_HEIGHT = 6;

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

        public LoseLine(Texture2D texture) : base()
        {
            _texture = texture;
            Position = new Vector2(LOSE_LINE_POS_X, LOSE_LINE_POS_Y);
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, CollisionBox, Color.White);
        }
    }
}