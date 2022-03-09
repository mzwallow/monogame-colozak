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

        private Texture2D _bg, _menu_bg, _option_bg, _barVolume,_volumeButton;
        Point _volumeAdjust;


        
        private Boolean _showOption;

        public MenuState(Colozak game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            //rectangle button
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            //font text
            var buttonFont = _content.Load<SpriteFont>("Fonts/Text");
            //background
            _bg = _content.Load<Texture2D>("BG/bg");
            //background menu
            _menu_bg = _content.Load<Texture2D>("BG/menu_bg");
            //background option
            _option_bg = _content.Load<Texture2D>("BG/option_bg2");
            //song background for menu and option
            _bgm = _content.Load<SoundEffect>("Sound/BackgroundMusic");
            //song
            _bgmInstance = _bgm.CreateInstance();
            _bgmInstance.IsLooped = true;
            _showOption = false;
            _bgmInstance.Play();
            _bgmInstance.Volume = 0.3f;
            _barVolume = _content.Load<Texture2D>("Controls/Rectangle");
            _volumeButton = new Texture2D(graphicsDevice,1,1);
            _volumeButton.SetData(new[] { Color.Chocolate });
            _volumeAdjust = Globals.CurrentMouseState.Position;

            //position start button
            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, Globals.SCREEN_HEIGHT / 2 - (buttonTexture.Height / 2 + 40)),
                Text = "Start",
            };

            newGameButton.Click += NewGameButton_Click;

            //position option button
            var OptionButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, Globals.SCREEN_HEIGHT / 2 + buttonTexture.Height / 2),
                Text = "Option",
            };

            OptionButton.Click += OptionButton_Click;

            //position quit game button
            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, Globals.SCREEN_HEIGHT / 2 + (buttonTexture.Height / 2 + 100)),
                Text = "Quit Game",
            };

            //call function quitgamebutton_Click
            quitGameButton.Click += QuitGameButton_Click;

            //keep object in list
            _menuComponents = new List<Component>()
            {
              newGameButton,
              OptionButton,
              quitGameButton,
            };

            //position back to menu button
            var backButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, Globals.SCREEN_HEIGHT/2 +180),
                Text = "Back To Menu",
            };

            //call function backbutton_click
            backButton.Click += BackButton_Click;

            //keep object in list
            _optionComponents = new List<Component>()
            {
              backButton,
            };

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            //if _showOption is false show object in _menuComponents
            if (!_showOption)
            {
                spriteBatch.Draw(_menu_bg, Vector2.Zero, Color.White);

                foreach (var component in _menuComponents)
                    component.Draw(gameTime, spriteBatch);

            }
            //if _showOption is true show object in _menuComponents
            else
            {
                spriteBatch.Draw(_option_bg, Vector2.Zero, Color.White);
                //spriteBatch.Draw(_volumeButton, new Rectangle(Globals.SCREEN_WIDTH/2-25, Globals.SCREEN_HEIGHT/2+50, 50, 50),Color.Chocolate);
                foreach (var component in _optionComponents)
                    component.Draw(gameTime, spriteBatch);
                //spriteBatch.Draw(_volumeButton,new Vector2(Globals.SCREEN_WIDTH/2-25,Globals.SCREEN_HEIGHT/2+50),Color.White);
                //spriteBatch.Draw(_barVolume, new Vector2(Globals.SCREEN_WIDTH/2-25, Globals.SCREEN_HEIGHT/2+50),Color.Chocolate);
                spriteBatch.Draw(_volumeButton, new Rectangle(Globals.SCREEN_WIDTH/2, Globals.SCREEN_HEIGHT/2, 100, 10),Color.Yellow);
                spriteBatch.Draw(_volumeButton, new Rectangle(Globals.SCREEN_WIDTH/2, Globals.SCREEN_HEIGHT/2, 20, 30),Color.Chocolate);
                
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
                //Globals.CurrentMouseState.X 
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
