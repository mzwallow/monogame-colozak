using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Colozak.Entities
{
    public abstract class Wall : IGameEntity
    {
        public abstract Rectangle CollisionBox { get; }

        public Vector2 Position { get; set; }

        protected Wall() { }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}