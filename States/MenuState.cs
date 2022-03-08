using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Colozak.Controls;
using Microsoft.Xna. Framework.Audio;
using Microsoft.Xna. Framework.Media;


namespace Colozak.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        private SoundEffect _bgm;
        private SoundEffectInstance _bgmInstance;

        private Texture2D _bg;

        public MenuState(Colozak game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            _bg = _content.Load<Texture2D>("BG/bg");
            _bgm = _content.Load<SoundEffect>("Sound/BackgroundMusic");
            _bgmInstance = _bgm.CreateInstance();
            _bgmInstance.IsLooped =true;


            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - 80, Globals.SCREEN_HEIGHT / 2 - 90),
                Text = "Start",
            };

            newGameButton.Click += NewGameButton_Click;

            var OptionButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - 80, Globals.SCREEN_HEIGHT / 2 - 20),
                Text = "Option",
            };

            OptionButton.Click += OptionButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - 80, Globals.SCREEN_HEIGHT / 2 + 50),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
              newGameButton,
              OptionButton,
              quitGameButton,
            };

            _bgmInstance.Play();
            _bgmInstance.Volume = 0.3f;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_bg, Vector2.Zero, Color.White);
            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);



            spriteBatch.End();
        }


        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }
        private void OptionButton_Click(object sender, EventArgs e)
        {
            // Console.WriteLine("Test");
            _game.ChangeState(new OptionState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
     

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
