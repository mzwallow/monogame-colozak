using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Colozak.Controls;

namespace Colozak.States
{
    public class OptionState : State
    {
        SpriteFont _text;

        private List<Component> _components;
        //Button backButton;

        public OptionState(Colozak game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            _text = content.Load<SpriteFont>("Fonts/Font");

            var backButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - 80, Globals.SCREEN_HEIGHT / 2 - 20),
                Text = "Back To Menu",
            };
            //quitGameButton.Click += QuitGameButton_Click;
            backButton.Click += backButton_Click;

             _components = new List<Component>()
            {
              backButton
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.IndianRed);
            spriteBatch.Begin();


            spriteBatch.DrawString(_text, "Option WEI!!!!", new Vector2(Globals.SCREEN_WIDTH / 2 - 80, Globals.SCREEN_HEIGHT / 2 - 90), Color.Black);

             foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        private void backButton_Click(object sender, EventArgs e)
        {
            // Back To Menu
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
    }
}
