using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Colozak.Entities
{
    public class CocoonManager
    {
        public CocoonManager()
        {
            for (int i = 0; i < Globals.MAX_ACTIVE_COCOONS; i++)
                Globals.ActiveCocoons[i] = null;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            for (int i = 0; i < Globals.MAX_ACTIVE_COCOONS; i++)
            {
                if (Globals.ActiveCocoons[i] != null)
                    Globals.ActiveCocoons[i].Draw(spriteBatch);
            }
        }

        public void SetCocoon(Texture2D texture, Vector2 position, int index)
        {
            Cocoon c = new Cocoon(texture, position);

            Globals.ActiveCocoons[Globals.LastCocoon++] = c;
        }
    }
}