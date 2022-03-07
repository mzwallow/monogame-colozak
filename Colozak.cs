using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Colozak.Entities;

namespace Colozak
{
    public class Colozak : Game
    {
        // new Vector2(Globals.TILE_SIZE * 10, (Globals.TILE_SIZE * 12) + (Globals.TILE_SIZE / 2))
        private const int GUN_POS_X = 480;
        private const int GUN_POS_Y = 600;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Gun _gun;

        private SpriteFont _font;
        private Texture2D _rect, _gunTexture, _wallTexture, _ceilingTexture;

        private int[] _map
        {
            get
            {
                return new int[90]
                {
                    0, 0, 0, 0, 0, 0, 9, 9,
                    0, 0, 1, 5, 7, 9, 9,
                    9, 9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9, 9,
                    9, 9, 9, 9, 9, 9, 9
                };

                // return new int[90]
                // {
                //     0, 0, 1, 1, 1, 6, 7, 4,
                //     7, 0, 1, 5, 7, 7, 4,
                //     0, 6, 1, 6, 7, 1, 0, 0,
                //     1, 5, 4, 4, 7, 7, 0,
                //     1, 5, 5, 7, 6, 0, 7, 7,
                //     9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9, 9,
                //     9, 9, 9, 9, 9, 9, 9
                // };
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

            Globals.Board = new Board();
            // Globals.CocoonsTexture = new Texture2D[8];
            // Globals.ActiveCocoons = new Cocoon[Globals.MAX_ACTIVE_COCOONS];
            Globals.CocoonManager = new CocoonManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

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
            Globals.Board.CreateMap(_map);

            // Load gun texture
            _gunTexture = this.Content.Load<Texture2D>("gun");
            _gun = new Gun(_gunTexture, new Vector2(GUN_POS_X, GUN_POS_Y));
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
            Globals.Board.Update(gameTime);

            _gun.Update(gameTime);

            if (Globals.CeilingCanDrop)
            {
                Globals.Board.DropGrids();
                Globals.WallAndCeilingManager.Ceiling.DropCeiling();

                Globals.CeilingCanDrop = false;
            }
            
            if (!Globals.CeilingCanDrop)
            {
                Globals.Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Globals.Timer >= 3f)
                {
                    Globals.CeilingCanDrop = true;
                    Globals.Timer = 0f;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aquamarine);

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
                if (c != null)
                    c.Draw(_spriteBatch);
            }

            // Debug
            _spriteBatch.DrawString(_font, "Gun state: " + _gun.CurrentState, new Vector2(10, 10), Color.Blue);
            _spriteBatch.DrawString(_font, "X: " + Globals.CurrentMouseState.X + " Y: " + Globals.CurrentMouseState.Y, new Vector2(10, 30), Color.Green);
            _spriteBatch.DrawString(_font, "Last cocoon: " + Globals.CocoonManager.LastCocoon,  new Vector2(10, 50), Color.Blue);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
