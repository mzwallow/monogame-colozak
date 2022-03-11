using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Colozak.Controls;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace Colozak.States
{
    public class MenuState : State
    {
        private List<Component> _menuComponents, _optionComponents;
        private Texture2D _bg, _menu_bg, _option_bg, _volumeRect;

        SpriteFont _buttonFont, _textFont;

        private bool _showOption;

        private SoundEffect _bgm, _soundFX;
        private SoundEffectInstance _bgmInstance, _soundFXInstance;
        private Rectangle _volumeBar;
        private float _volumeAdjust;

        public MenuState(Colozak game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            //rectangle button
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            //buttonFont text
            _buttonFont = _content.Load<SpriteFont>("Fonts/Text");
            //textFont
            _textFont = _content.Load<SpriteFont>("Fonts/Logo");
            //background
            _bg = _content.Load<Texture2D>("BG/bg");
            //background menu
            _menu_bg = _content.Load<Texture2D>("BG/menu_bg");
            //background option
            _option_bg = _content.Load<Texture2D>("BG/option_bg2");
            //song background for menu and option
            _bgm = _content.Load<SoundEffect>("Sound/BackgroundMusic");
            _bgmInstance = _bgm.CreateInstance();
            _bgmInstance.IsLooped = true;
            _bgmInstance.Volume = Globals.MusicVolume;
            _bgmInstance.Play();

            //SoundEffect
            _soundFX = content.Load<SoundEffect>("Sound/Click");
            _soundFXInstance = _soundFX.CreateInstance();
            _soundFXInstance.Volume = Globals.MusicVolume;

            //Volume option Rectangle
            _volumeRect = new Texture2D(graphicsDevice, 1, 1);
            _volumeRect.SetData(new[] { Color.White });
            _volumeBar = new Rectangle(Globals.SCREEN_WIDTH / 2 - 480 / 2, Globals.SCREEN_HEIGHT / 2 - 20, 480, 10);

            //Show Option
            _showOption = false;

            //position start button
            var newGameButton = new Button(buttonTexture, _buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, Globals.SCREEN_HEIGHT / 2 - (buttonTexture.Height / 2 + 40)),
                Text = "Start",
            };

            newGameButton.Click += NewGameButton_Click;

            //position option button
            var OptionButton = new Button(buttonTexture, _buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, Globals.SCREEN_HEIGHT / 2 + buttonTexture.Height / 2),
                Text = "Option",
            };

            OptionButton.Click += OptionButton_Click;

            //position quit game button
            var quitGameButton = new Button(buttonTexture, _buttonFont)
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
            var backButton = new Button(buttonTexture, _buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, Globals.SCREEN_HEIGHT / 2 + 180),
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

                foreach (var component in _optionComponents)
                    component.Draw(gameTime, spriteBatch);
                //Music Option
                spriteBatch.DrawString(_textFont, "MUSIC", new Vector2((Globals.SCREEN_WIDTH / 2) - _textFont.MeasureString("MUSIC").X / 2, 240), Color.DarkGray);
                spriteBatch.Draw(_volumeRect, new Rectangle(Globals.SCREEN_WIDTH / 2 - 480 / 2, Globals.SCREEN_HEIGHT / 2 - 20, 480, 10), Color.White);
                spriteBatch.Draw(_volumeRect, new Rectangle(Globals.MusicX, Globals.SCREEN_HEIGHT / 2 - (15 + 13), 20, 26), Color.Silver);
                //SoundRffect Option
                spriteBatch.DrawString(_textFont, "SOUNDTRACK", new Vector2((Globals.SCREEN_WIDTH / 2) - _textFont.MeasureString("SOUNDTRACK").X / 2, Globals.SCREEN_HEIGHT / 2 + 30), Color.DarkGray);
                spriteBatch.Draw(_volumeRect, new Rectangle(Globals.SCREEN_WIDTH / 2 - 480 / 2, Globals.SCREEN_HEIGHT / 2 + 130, 480, 10), Color.White);
                spriteBatch.Draw(_volumeRect, new Rectangle(Globals.SoundX, Globals.SCREEN_HEIGHT / 2 + (110 + 13), 20, 26), Color.Silver);

            }
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
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
                if (Globals.CurrentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (Globals.CurrentMouseState.X > Globals.SCREEN_WIDTH / 2 - ((_volumeBar.Width / 2) + 5) &&
                    Globals.CurrentMouseState.X < Globals.SCREEN_WIDTH / 2 + _volumeBar.Width / 2 && Globals.CurrentMouseState.Y < 370 && Globals.CurrentMouseState.Y > 310)
                    {
                        //Adjust Music Volume
                        Globals.MusicX = Globals.CurrentMouseState.X;
                        _volumeAdjust = ((Globals.CurrentMouseState.X / 48f) - 5) * 0.1f;
                        if (_volumeAdjust < 0.0001f) Globals.MusicVolume = 0f;
                        else
                            Globals.MusicVolume = _bgmInstance.Volume = _volumeAdjust;

                    }
                    else if (Globals.CurrentMouseState.X > Globals.SCREEN_WIDTH / 2 - ((_volumeBar.Width / 2) + 5) &&
                    Globals.CurrentMouseState.X < Globals.SCREEN_WIDTH / 2 + _volumeBar.Width / 2 && Globals.CurrentMouseState.Y < 520 && Globals.CurrentMouseState.Y > 460)
                    {
                        //Adjust SoundFX Volume
                        Globals.SoundX = Globals.CurrentMouseState.X;
                        _volumeAdjust = ((Globals.CurrentMouseState.X / 48f) - 5) * 0.1f;
                        if (_volumeAdjust < 0.0001f) Globals.SoundVolume = 0f;
                        else
                            Globals.SoundVolume = _soundFXInstance.Volume = _volumeAdjust;
                        _soundFXInstance.Play();



                    }
                }
            }
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
            _soundFXInstance.Play();
            _bgmInstance.Stop();
        }
        private void OptionButton_Click(object sender, EventArgs e)
        {
            _soundFXInstance.Play();
            _showOption = true;
        }


        private void BackButton_Click(object sender, EventArgs e)
        {
            _soundFXInstance.Play();
            _showOption = false;


        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _soundFXInstance.Play();
            _game.Exit();
        }
    }
}
