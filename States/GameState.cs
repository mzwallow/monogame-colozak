using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Colozak.Entities;

namespace Colozak.States
{
    public class GameState : State
    {
        private Gun _gun;

        private SoundEffect _bkgs, _shootFX, _popFX,_winFX, _loseFX;
        private SoundEffectInstance _bkgsInstance, _shootInstance, _popInstance, _winInstance,_loseInstance;

        private bool _showLoseMenu, _showWinMenu;



        private SpriteFont _font;
        private Texture2D _gunTexture, _wallTexture, _ceilingTexture, _frameTexture, _bg, _loseLineTexture;

        private int[] _map
        {
            get
            {
                // return new int[90]
                // {
                //    7, 7, 7, 7, 7, 7, 7, 7,
                //     7, 7, 7, 7, 7, 7, 7,
                //     7, 7, 7, 7, 7, 7, 7, 7,
                //     7, 7, 7, 7, 7, 7, 7,
                //     9, 9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9
                // };

                return new int[90]
                {
                    0, 0, 1, 1, 1, 6, 7, 4,
                    7, 0, 1, 5, 7, 7, 4,
                    0, 6, 1, 6, 7, 1, 0, 0,
                    1, 5, 4, 4, 7, 7, 0,
                    1, 5, 5, 7, 6, 0, 7, 7,
                    9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9
                };
            }
        }

        public GameState(Colozak game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            _font = content.Load<SpriteFont>("Fonts/Text");

            // sound bkg
            _bkgs = content.Load<SoundEffect>("Sound/bkgs");
            _bkgsInstance = _bkgs.CreateInstance();
            _bkgsInstance.IsLooped = true;
            _bkgsInstance.Play();
            _bkgsInstance.Volume = Globals.MusicVolume;
            _showLoseMenu = false;
            _showWinMenu = false;



            // Load wall and ceiling textures
            _wallTexture = content.Load<Texture2D>("wall");
            _ceilingTexture = content.Load<Texture2D>("Games/fog");
            _loseLineTexture = content.Load<Texture2D>("Games/thunder");
            Globals.ColliderManager = new ColliderManager(_wallTexture, _ceilingTexture, _loseLineTexture);

            // Load background texture
            _bg = _content.Load<Texture2D>("BG/bg");

            // Load main game frame texture
            _frameTexture = content.Load<Texture2D>("Games/frame");

            // Load cocoon textures
            Globals.CocoonManager.CocoonsTexture[0] = content.Load<Texture2D>("Games/cocoon_blue");
            Globals.CocoonManager.CocoonsTexture[1] = content.Load<Texture2D>("Games/cocoon_green");
            Globals.CocoonManager.CocoonsTexture[2] = content.Load<Texture2D>("cocoon");
            Globals.CocoonManager.CocoonsTexture[3] = content.Load<Texture2D>("cocoon");
            Globals.CocoonManager.CocoonsTexture[4] = content.Load<Texture2D>("Games/cocoon_red");
            Globals.CocoonManager.CocoonsTexture[5] = content.Load<Texture2D>("Games/cocoon_orange");
            Globals.CocoonManager.CocoonsTexture[6] = content.Load<Texture2D>("Games/cocoon_yellow");
            Globals.CocoonManager.CocoonsTexture[7] = content.Load<Texture2D>("Games/cocoon_violet");

            // Create map
            Globals.BoardManager.CreateMap(_map);

            // Load gun
            _gunTexture = content.Load<Texture2D>("gun");
            _gun = new Gun(_gunTexture);

            // Shooting gun FX
            _shootFX = _content.Load<SoundEffect>("Sound/Shoot");
            _shootInstance = _shootFX.CreateInstance();
            _shootInstance.Volume = Globals.SoundVolume;
            // Pop FX
            _popFX = _content.Load<SoundEffect>("Sound/Pop");
            _popInstance = _popFX.CreateInstance();
            _popInstance.Volume = Globals.SoundVolume;
            // Game Lose FX
            _loseFX = _content.Load<SoundEffect>("Sound/Btoom");
            _loseInstance = _loseFX.CreateInstance();
            _loseInstance.Volume = Globals.SoundVolume;
             // Game Win FX
            _winFX = _content.Load<SoundEffect>("Sound/Win");
            _winInstance = _winFX.CreateInstance();
            _winInstance.Volume = Globals.SoundVolume;



        }

        public override void Update(GameTime gameTime)
        {
            Globals.Pop = false;
            // Globals.CocoonManager.Update(gameTime);
            foreach (Cocoon c in Globals.CocoonManager.ActiveCocoons)
            {
                if (c != null)
                    c.Update(gameTime);
            }

            Globals.ColliderManager.Update(gameTime);
            //Globals.BoardManager.Update(gameTime);

            _gun.Update(gameTime);
            //Play SFX
            if (Globals.Shoot) _shootInstance.Play();
            if (Globals.Pop) _popInstance.Play();




            if (Globals.CeilingCanDrop)
            {
                Globals.BoardManager.DropGrids();
                Globals.ColliderManager.Ceiling.DropCeiling();

                Globals.CeilingCanDrop = false;
            }

            if (!Globals.CeilingCanDrop)
            {
                Globals.Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Globals.Timer >= 15f)
                {
                    Globals.CeilingCanDrop = true;
                    Globals.Timer = 0f;
                }
            }

            if (Globals.BoardManager.CheckWin())
            {
                _showWinMenu = true;
                Globals.BoardManager.Update(gameTime);
            }
            if (Globals.BoardManager.CheckLose())
            {

                _showLoseMenu = true;
                Globals.BoardManager.Update(gameTime);

            }

        }

        public override void PostUpdate(GameTime gameTime)
        {


            if (_showLoseMenu == true)
            {
                _loseInstance.Play();
                _game.ChangeState(new GameLoseState(_game, _graphicsDevice, _content));
                _bkgsInstance.Stop();
            }

            if (_showWinMenu == true)
            {
                _winInstance.Play();
                _game.ChangeState(new GameWinState(_game, _graphicsDevice, _content));
                _bkgsInstance.Stop();
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.DarkViolet);

            spriteBatch.Begin();

            // Draw background
            spriteBatch.Draw(_bg, Vector2.Zero, Color.White);

            // Draw walls and ceiling
            Globals.ColliderManager.Draw(spriteBatch);

            // Draw a frame
            spriteBatch.Draw(_frameTexture, Vector2.Zero, Color.White);

            // Draw a gun
            _gun.Draw(spriteBatch);

            // Draw cocoons
            // Globals.CocoonManager.Draw(spriteBatch);
            foreach (Cocoon c in Globals.CocoonManager.ActiveCocoons)
            {
                if (c is null)
                    continue;

                c.Draw(spriteBatch);
            }

            // Debug
            spriteBatch.DrawString(_font, "Gun is shooting: " + Globals.IsShooting, new Vector2(10, 10), Color.Blue);
            spriteBatch.DrawString(_font, "X: " + Globals.CurrentMouseState.X + " Y: " + Globals.CurrentMouseState.Y, new Vector2(10, 30), Color.Green);
            spriteBatch.DrawString(_font, "Last cocoon: " + Globals.CocoonManager.LastCocoonIndex, new Vector2(10, 50), Color.Blue);

            spriteBatch.End();
        }
    }
}
