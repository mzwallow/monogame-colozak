using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Colozak.States
{
    public abstract class State
    {
        protected ContentManager _content;

        protected GraphicsDevice _graphicsDevice;

        protected Colozak _game;

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public State(Colozak game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;

            _graphicsDevice = graphicsDevice;

            _content = content;
        }

        public abstract void Update(GameTime gameTime);
    }
}

