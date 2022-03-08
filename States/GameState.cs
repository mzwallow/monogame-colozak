using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Colozak.Entities;

namespace Colozak.States
{
    public class GameState : State
    {
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

        public GameState(Colozak game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            _font = content.Load<SpriteFont>("Fonts/Text");
            _rect = new Texture2D(graphicsDevice, Globals.TILE_SIZE, Globals.TILE_SIZE);
            Color[] data = new Color[Globals.TILE_SIZE * Globals.TILE_SIZE];
            for (int i = 0; i < data.Length; i++)
                data[i] = Color.YellowGreen;
            _rect.SetData<Color>(data);

            // Load wall and ceiling textures
            _wallTexture = content.Load<Texture2D>("wall");
            _ceilingTexture = content.Load<Texture2D>("ceiling");
            Globals.WallAndCeilingManager = new WallAndCeilingManager(_wallTexture, _ceilingTexture);

            // Load cocoon textures
            Globals.CocoonManager.CocoonsTexture[0] = content.Load<Texture2D>("cocoon_blue");
            Globals.CocoonManager.CocoonsTexture[1] = content.Load<Texture2D>("cocoon_green");
            Globals.CocoonManager.CocoonsTexture[2] = content.Load<Texture2D>("cocoon");
            Globals.CocoonManager.CocoonsTexture[3] = content.Load<Texture2D>("cocoon");
            Globals.CocoonManager.CocoonsTexture[4] = content.Load<Texture2D>("cocoon_red");
            Globals.CocoonManager.CocoonsTexture[5] = content.Load<Texture2D>("cocoon_orange");
            Globals.CocoonManager.CocoonsTexture[6] = content.Load<Texture2D>("cocoon_yellow");
            Globals.CocoonManager.CocoonsTexture[7] = content.Load<Texture2D>("cocoon_violet");

            // Create map
            Globals.BoardManager.CreateMap(_map);

            // Load gun
            _gunTexture = content.Load<Texture2D>("gun");
            _gun = new Gun(_gunTexture);
        }

        public override void Update(GameTime gameTime)
        {
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
                _game.Exit();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.DarkViolet);

            spriteBatch.Begin();

            // Draw play area
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i+j) % 2 == 0)
                        spriteBatch.Draw(
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
                        spriteBatch.Draw(
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
            Globals.WallAndCeilingManager.Draw(spriteBatch);

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
            spriteBatch.DrawString(_font, "Last cocoon: " + Globals.CocoonManager.LastCocoonIndex,  new Vector2(10, 50), Color.Blue);

            spriteBatch.End();
        }
    }
}
