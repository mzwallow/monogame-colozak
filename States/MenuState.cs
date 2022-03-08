using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Colozak.Controls;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace Colozak.States
{
    public class MenuState : State
    {
        private List<Component> _menuComponents, _optionComponents;

        private SoundEffect _bgm;
        private SoundEffectInstance _bgmInstance;

        private Texture2D _bg, _menu_bg, _option_bg;
        
        private Boolean _showOption;

        public MenuState(Colozak game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Text");
            _bg = _content.Load<Texture2D>("BG/bg");
            _menu_bg = _content.Load<Texture2D>("BG/menu_bg");
            _option_bg = _content.Load<Texture2D>("BG/option_bg2");
            _bgm = _content.Load<SoundEffect>("Sound/BackgroundMusic");
            _bgmInstance = _bgm.CreateInstance();
            _bgmInstance.IsLooped = true;
            _showOption = false;
            _bgmInstance.Play();
            _bgmInstance.Volume = 0.3f;

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, Globals.SCREEN_HEIGHT / 2 - (buttonTexture.Height / 2 + 40)),
                Text = "Start",
            };

            newGameButton.Click += NewGameButton_Click;

            var OptionButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, Globals.SCREEN_HEIGHT / 2 + buttonTexture.Height / 2),
                Text = "Option",
            };

            OptionButton.Click += OptionButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, Globals.SCREEN_HEIGHT / 2 + (buttonTexture.Height / 2 + 100)),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _menuComponents = new List<Component>()
            {
              newGameButton,
              OptionButton,
              quitGameButton,
            };

            var backButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, 540),
                Text = "Back To Menu",
            };

            backButton.Click += BackButton_Click;

            _optionComponents = new List<Component>()
            {
              backButton,

            };


        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (!_showOption)
            {
                spriteBatch.Draw(_menu_bg, Vector2.Zero, Color.White);

                foreach (var component in _menuComponents)
                    component.Draw(gameTime, spriteBatch);

            }
            else
            {
                spriteBatch.Draw(_option_bg, Vector2.Zero, Color.White);

                foreach (var component in _optionComponents)
                    component.Draw(gameTime, spriteBatch);

                
            }
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            if (!_showOption)
            {
                foreach (var component in _menuComponents)
                    component.Update(gameTime);

            }
            else
            {
                foreach (var component in _optionComponents)
                    component.Update(gameTime);
            }
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
            _bgmInstance.Stop();
        }
        private void OptionButton_Click(object sender, EventArgs e)
        {
           _showOption = true;
        }


        private void BackButton_Click(object sender, EventArgs e)
        {
            _showOption = false;
          
            
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
