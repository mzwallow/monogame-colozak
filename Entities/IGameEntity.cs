using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Colozak.Entities
{
    public interface IGameEntity
    {
        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}