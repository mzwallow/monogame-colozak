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
        private List<Component> _menuComponents;

        private List<Component> _optionComponents;

        private SoundEffect _bgm;
        private SoundEffectInstance _bgmInstance;

        private Texture2D _bg,_menu_bg;

        SpriteFont logo;

        private float VolumeAdjust;

        Boolean showOption;
        SpriteFont buttonFont;

        public MenuState(Colozak game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            /*var*/ buttonFont = _content.Load<SpriteFont>("Fonts/Text");
            logo = _content.Load<SpriteFont>("Fonts/Logo");
            _bg = _content.Load<Texture2D>("BG/bg");
            _menu_bg = _content.Load<Texture2D>("BG/menu_bg");
            _bgm = _content.Load<SoundEffect>("Sound/BackgroundMusic");
            _bgmInstance = _bgm.CreateInstance();
            _bgmInstance.IsLooped = true;
            VolumeAdjust = 0.01f;
            showOption = false;
            

            _bgmInstance.Play();
            _bgmInstance.Volume *= VolumeAdjust;

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width/2, Globals.SCREEN_HEIGHT / 2 - (buttonTexture.Height/2 + 40)),
                Text = "Normal mode",
            };

            newGameButton.Click += NewGameButton_Click;

            var optionButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width/2, Globals.SCREEN_HEIGHT / 2 + (buttonTexture.Height/2)),
                Text = "Option",
            };

            optionButton.Click += OptionButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH /2 - buttonTexture.Width/2, Globals.SCREEN_HEIGHT / 2 + (buttonTexture.Height/2 + 100)),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _menuComponents = new List<Component>()
            {
              newGameButton,
              optionButton,
              quitGameButton,
            };

            var backButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width/2, 540),
                Text = "Back To Menu",
            };

            backButton.Click += backButton_Click;

            _optionComponents = new List<Component>{
              backButton,
            };



        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            if (!showOption)
            {
                spriteBatch.Draw(_menu_bg, Vector2.Zero, Color.White);
                //spriteBatch.DrawString(logo, "COLOZAK", new Vector2(Globals.SCREEN_WIDTH / 2 - logo.MeasureString("COLOZAK").X / 2, logo.MeasureString("COLOZAK").Y / 2), Color.White);
                foreach (var component in _menuComponents)
                    component.Draw(gameTime, spriteBatch);

            }
            else
            {
                spriteBatch.Draw(_bg, Vector2.Zero, Color.White);
                spriteBatch.DrawString(logo, "OPTION", new Vector2(Globals.SCREEN_WIDTH / 2 - logo.MeasureString("OPTION").X / 2, logo.MeasureString("OPTION").Y / 2), Color.White);
                foreach (var component in _optionComponents)
                    component.Draw(gameTime, spriteBatch);

            }

            spriteBatch.End();

             //Console.WriteLine(buttonFont.MeasureString("NORMAL MODE").X);//497
             //Console.WriteLine(buttonFont.MeasureString("NORMAL MODE").Y);//153
        }


        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
            _bgmInstance.Stop();
        }
        private void OptionButton_Click(object sender, EventArgs e)
        {
            showOption = true;

            //Console.WriteLine("Test");
            //_game.ChangeState(new OptionState(_game, _graphicsDevice, _content));
            //_bgmInstance.Stop(); 
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            if (!showOption)
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


        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            // Back To Menu
            showOption = false;



        }
    }
}
