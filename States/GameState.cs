using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Colozak.States
{
  public class GameState : State
  {
    SpriteFont _text;
     
    public GameState(Colozak game, GraphicsDevice graphicsDevice, ContentManager content) 
      : base(game, graphicsDevice, content)
    {
       _text = content.Load<SpriteFont>("Fonts/Font");
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      _graphicsDevice.Clear(Color.DarkViolet);
      spriteBatch.Begin();

                //_spriteBatch.DrawString();
               
               spriteBatch.DrawString(_text, "Game NAJA", new Vector2(Globals.SCREEN_WIDTH / 2 - 80, Globals.SCREEN_HEIGHT / 2 - 90), Color.Black);
               
               spriteBatch.End();

    }

    public override void PostUpdate(GameTime gameTime)
    {

    }

    public override void Update(GameTime gameTime)
    {

    }
    public override void LoadContent(){
          

    }
    public override void UnloadContent(){
          

    }
  }
}
