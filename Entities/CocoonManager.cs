using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System.Collections.Generic;

namespace Colozak.Entities
{
    public class CocoonManager : IGameEntity
    {
        public Texture2D[] CocoonsTexture = new Texture2D[Globals.NUM_COCOONS];
        public Cocoon[] ActiveCocoons = new Cocoon[Globals.MAX_ACTIVE_COCOONS];
        public int LastCocoon = 0;
        public bool HasNext = true;
        public List<Cocoon> ToDestroyCocoons = new List<Cocoon>();
        public List<Cocoon> ToKeepCocoons = new List<Cocoon>();

        public CocoonManager()
        {
            for (int i = 0; i < Globals.MAX_ACTIVE_COCOONS; i++)
                ActiveCocoons[i] = null;
        }

        public void Update(GameTime gameTime) 
        {
            
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            
        }

        public void AddCocoonToGun(Texture2D texture, Vector2 position, float rotation)
        {
            Cocoon c = new Cocoon(texture, position, rotation);
            c.IsMoving = true;
            ActiveCocoons[LastCocoon++] = c;
            Globals.CocoonManager.ActiveCocoons[LastCocoon++] = c;
        }

        public void SetCocoonToBoard(Texture2D texture, Vector2 position, int index)
        {
            Cocoon c = new Cocoon(texture, position);
            c.BoardIndex = index;
            ActiveCocoons[LastCocoon++] = c;
        }

        public void ResetCocoonPosition()
        {
            foreach (Cocoon c in Globals.CocoonManager.ActiveCocoons)
            {
                if (c == null)
                    continue;
                
                c.Position = Globals.Board.Grids[c.BoardIndex].Position;
            }
        }

        public void CheckCocoonPosition(Cocoon cocoon)
        {
            float _min = 999;
            int _boardIndex = 0;

            foreach (HexGrid grid in Globals.Board.Grids)
            {
                if (grid.IsEmpty)
                {
                    if (grid.DistanceTo(cocoon.Position) < _min)
                    {
                        _min = grid.DistanceTo(cocoon.Position);
                        _boardIndex = grid.BoardIndex;
                    }
                }
            }

            cocoon.IsMoving = false;
            cocoon.BoardIndex = _boardIndex;
            cocoon.Position = Globals.Board.Grids[_boardIndex].Position;

            Globals.Board.Grids[_boardIndex].IsEmpty = false;
        }

        public void CheckCocoon(Cocoon cocoon)
        {
            foreach (Cocoon c in ActiveCocoons)
            {
                if (c == null)
                    continue;
                
                if (Vector2.Distance(c.Position, cocoon.Position) <= Globals.TILE_SIZE + 2 &&
                    c.Texture == cocoon.Texture &&
                    !c.IsChecked)
                {
                    c.IsChecked = true;
                    ToDestroyCocoons.Add(c);
                    CheckCocoon(c);
                }
            }
        }

        public void CheckCocoonDown(Cocoon cocoon)
        {
            foreach (Cocoon c in ActiveCocoons)
            {
                if (c == null)
                    continue;
                
                if (Vector2.Distance(c.Position, cocoon.Position) <= Globals.TILE_SIZE + 2 &&
                    !c.IsChecked)
                {
                    c.IsChecked = true;
                    // ToDestroyCocoons.Add(c);
                    CheckCocoonDown(c);
                }
            }
        }
    }
}