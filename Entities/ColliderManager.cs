using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Colozak.Entities
{
    public class ColliderManager : IGameEntity
    {
        public Wall WallLeft, WallRight;
        public Ceiling Ceiling;
        public LoseLine LoseLine;

        public ColliderManager(Texture2D wallTexture, Texture2D ceilingTexture, Texture2D loseLineTexture)
        {
            WallLeft = new WallLeft(wallTexture);
            WallRight = new WallRight(wallTexture);
            Ceiling = new Ceiling(ceilingTexture);
            LoseLine = new LoseLine(loseLineTexture);
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            WallLeft.Draw(spriteBatch);
            WallRight.Draw(spriteBatch);
            Ceiling.Draw(spriteBatch);
            LoseLine.Draw(spriteBatch);
        }
    }
}