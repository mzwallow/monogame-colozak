using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Colozak.Entities
{
    public class WallAndCeilingManager : IGameEntity
    {
        public Wall WallLeft, WallRight;
        public Ceiling Ceiling;

        public WallAndCeilingManager(Texture2D wallTexture, Texture2D ceilingTexture)
        {
            WallLeft = new WallLeft(wallTexture);
            WallRight = new WallRight(wallTexture);
            Ceiling = new Ceiling(ceilingTexture);
        }

        public void Update(GameTime gameTime)
        {
            WallLeft.Update(gameTime);
            WallRight.Update(gameTime);
            Ceiling.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            WallLeft.Draw(spriteBatch);
            WallRight.Draw(spriteBatch);
            Ceiling.Draw(spriteBatch);
        }
    }
}