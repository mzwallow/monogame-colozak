using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Colozak.Entities;
using Colozak.States;

namespace Colozak
{
    public class Colozak : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private State _currentState;

        private State _nextState;

<<<<<<< HEAD


=======
>>>>>>> 887e17ea0078aa4719670c54d5cca5176f284257
        public void ChangeState(State state)
        {
            _nextState = state;
        }
        
        private Gun _gun;

        private SpriteFont _font;
        private Texture2D _rect, _gunTexture, _wallTexture, _ceilingTexture;

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

        public Colozak()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Globals.SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = Globals.SCREEN_HEIGHT;
            _graphics.ApplyChanges();

            Globals.BoardManager = new Board();
            Globals.CocoonManager = new CocoonManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);


            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);


            _font = this.Content.Load<SpriteFont>("Fonts/JetBrainsMono");
            _rect = new Texture2D(_graphics.GraphicsDevice, Globals.TILE_SIZE, Globals.TILE_SIZE);
            Color[] data = new Color[Globals.TILE_SIZE * Globals.TILE_SIZE];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.YellowGreen;
            _rect.SetData<Color>(data);

            // Load wall and ceiling textures
            _wallTexture = this.Content.Load<Texture2D>("wall");
            _ceilingTexture = this.Content.Load<Texture2D>("ceiling");
            Globals.WallAndCeilingManager = new WallAndCeilingManager(_wallTexture, _ceilingTexture);

            // Load cocoon textures
            Globals.CocoonManager.CocoonsTexture[0] = this.Content.Load<Texture2D>("cocoon_blue");
            Globals.CocoonManager.CocoonsTexture[1] = this.Content.Load<Texture2D>("cocoon_green");
            Globals.CocoonManager.CocoonsTexture[2] = this.Content.Load<Texture2D>("cocoon");
            Globals.CocoonManager.CocoonsTexture[3] = this.Content.Load<Texture2D>("cocoon");
            Globals.CocoonManager.CocoonsTexture[4] = this.Content.Load<Texture2D>("cocoon_red");
            Globals.CocoonManager.CocoonsTexture[5] = this.Content.Load<Texture2D>("cocoon_orange");
            Globals.CocoonManager.CocoonsTexture[6] = this.Content.Load<Texture2D>("cocoon_yellow");
            Globals.CocoonManager.CocoonsTexture[7] = this.Content.Load<Texture2D>("cocoon_violet");

            // Create map
            Globals.BoardManager.CreateMap(_map);

            // Load gun
            _gunTexture = this.Content.Load<Texture2D>("gun");
            _gun = new Gun(_gunTexture);
        }

        protected override void Update(GameTime gameTime)

        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.PreviousMouseState = Globals.CurrentMouseState;
            Globals.CurrentMouseState = Mouse.GetState();

            // Globals.CocoonManager.Update(gameTime);
            foreach (Cocoon c in Globals.CocoonManager.ActiveCocoons)
            {
                if (c != null)
                    c.Update(gameTime);
            }

            Globals.WallAndCeilingManager.Update(gameTime);
            Globals.BoardManager.Update(gameTime);

            _gun.Update(gameTime);

            if (Globals.CeilingCanDrop)
            {
                Globals.BoardManager.DropGrids();
                Globals.WallAndCeilingManager.Ceiling.DropCeiling();

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
                this.Exit();

            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
<<<<<<< HEAD
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _currentState.Draw(gameTime, _spriteBatch);


=======
            GraphicsDevice.Clear(Color.Aquamarine);
            
            _currentState.Draw(gameTime, _spriteBatch);

            _spriteBatch.Begin();

            // Draw play area
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i+j) % 2 == 0)
                        _spriteBatch.Draw(
                            _rect, 
                            new Vector2((Globals.TILE_SIZE*j) + (Globals.TILE_SIZE*6), (Globals.TILE_SIZE*i) + (Globals.TILE_SIZE*2)), 
                            null, 
                            Color.Black, 
                            0f, 
                            Vector2.Zero, 
                            1f, 
                            SpriteEffects.None, 
                            0f
                        );
                    else
                        _spriteBatch.Draw(
                            _rect, 
                            new Vector2((Globals.TILE_SIZE*j) + (Globals.TILE_SIZE*6), (Globals.TILE_SIZE*i) + (Globals.TILE_SIZE*2)), 
                            null, 
                            Color.AliceBlue, 
                            0f, 
                            Vector2.Zero, 
                            1f, 
                            SpriteEffects.None, 
                            0f
                        );
                }
            }

            // Draw walls and ceiling
            Globals.WallAndCeilingManager.Draw(_spriteBatch);

            // Draw a gun
            _gun.Draw(_spriteBatch);

            // Draw cocoons
            // Globals.CocoonManager.Draw(_spriteBatch);
            foreach (Cocoon c in Globals.CocoonManager.ActiveCocoons)
            {
                if (c is null)
                    continue;

                c.Draw(_spriteBatch);
            }

            // Debug
            _spriteBatch.DrawString(_font, "Gun is shooting: " + Globals.IsShooting, new Vector2(10, 10), Color.Blue);
            _spriteBatch.DrawString(_font, "X: " + Globals.CurrentMouseState.X + " Y: " + Globals.CurrentMouseState.Y, new Vector2(10, 30), Color.Green);
            _spriteBatch.DrawString(_font, "Last cocoon: " + Globals.CocoonManager.LastCocoonIndex,  new Vector2(10, 50), Color.Blue);

            _spriteBatch.End();
>>>>>>> 887e17ea0078aa4719670c54d5cca5176f284257

            base.Draw(gameTime);
        }



    }
}
