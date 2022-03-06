using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Colozak.Entities;

namespace Colozak
{
    public class Colozak : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont _font;
        private Texture2D _rect;

        private int[] _map
        {
            get
            {
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

            Globals.Board = new Board();
            Globals.CocoonsTexture = new Texture2D[8];
            Globals.ActiveCocoons = new Cocoon[Globals.MAX_ACTIVE_COCOONS];
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

            // Load cocoons texture
            Globals.CocoonsTexture[0] = this.Content.Load<Texture2D>("cocoon_blue");
            Globals.CocoonsTexture[1] = this.Content.Load<Texture2D>("cocoon_green");
            Globals.CocoonsTexture[2] = this.Content.Load<Texture2D>("cocoon");
            Globals.CocoonsTexture[3] = this.Content.Load<Texture2D>("cocoon");
            Globals.CocoonsTexture[4] = this.Content.Load<Texture2D>("cocoon_red");
            Globals.CocoonsTexture[5] = this.Content.Load<Texture2D>("cocoon_orange");
            Globals.CocoonsTexture[6] = this.Content.Load<Texture2D>("cocoon_yellow");
            Globals.CocoonsTexture[7] = this.Content.Load<Texture2D>("cocoon_violet");

            // Create map
            Globals.Board.CreateMap(_map);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aquamarine);

            _spriteBatch.Begin();

            // Draw Play area
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

            // Draw cocoon
            foreach (var c in Globals.ActiveCocoons)
            {
                if (c != null)
                    c.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
