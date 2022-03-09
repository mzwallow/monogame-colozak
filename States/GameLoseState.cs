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
    public class GameLoseState : State
    {
        private List<Component> _Losecomponents;
        
        private Texture2D _bg;

        private SoundEffect _bgm;

        private SoundEffectInstance _bgmInstance;

        public GameLoseState(Colozak game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            //rectangle button
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            //font text
            var buttonFont = _content.Load<SpriteFont>("Fonts/Text");
            //bg lose
            _bg = _content.Load<Texture2D>("BG/lose");
            //song
            _bgm = _content.Load<SoundEffect>("Sound/BackgroundMusic");
            _bgmInstance = _bgm.CreateInstance();
            _bgmInstance.IsLooped = true;
            _bgmInstance.Play();
            _bgmInstance.Volume = 0.3f;


            ///position Menu button
            var MenuBotton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, Globals.SCREEN_HEIGHT / 2 - (buttonTexture.Height / 2 + 40)),
                Text = "Menu",
            };

            //call function Menubutton_click
            MenuBotton.Click += MenuButton_Click;

            //position quit game button
            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(Globals.SCREEN_WIDTH / 2 - buttonTexture.Width / 2, Globals.SCREEN_HEIGHT / 2 + (buttonTexture.Height / 2 )),
                Text = "Quit Game",
            };

            //call function quitgamebutton_Click
            quitGameButton.Click += QuitGameButton_Click;

            //keep object in list
            _Losecomponents = new List<Component>()
            {
              MenuBotton,
              quitGameButton
            };
        }

        

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_bg, Vector2.Zero, Color.White);
            foreach (var component in _Losecomponents)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        ///<summary>
        ///reset game and back to menu screen
        ///</summary>
        private void MenuButton_Click(object sender, EventArgs e)
        {
            Globals.IsShooting = false;
            Globals.Timer = 0f;
            Globals.CeilingCanDrop = false;
            Globals.BoardManager.Reset();
            Globals.CocoonManager.Reset();
            _bgmInstance.Stop();
            // Back To Menu
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
            
        }

        ///<summary>
        ///quit game
        ///</summary>
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _Losecomponents)
                component.Update(gameTime);
        }

    }
}
