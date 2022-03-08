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
        public int LastCocoonIndex = 0;
        public List<Cocoon> ToDestroyCocoons = new List<Cocoon>();
        public List<Cocoon> ToKeepCocoons = new List<Cocoon>();

        /// <summary>
        /// Manage cocoon's position, 3-cocoon match, and dangling cocoons.
        /// </summary>
        public CocoonManager()
        {
            for (int i = 0; i < Globals.MAX_ACTIVE_COCOONS; i++)
                ActiveCocoons[i] = null;
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch) { }

        /// <summary>
        /// Add a cocoon to a gun.
        /// </summary>
        public void AddCocoonToGun(Texture2D texture, Vector2 position, float rotation)
        {
            Cocoon c = new Cocoon(texture, position, rotation);
            c.IsMoving = true;
            ActiveCocoons[LastCocoonIndex++] = c;
        }

        /// <summary>
        /// Set cocoon to the board when create map.
        /// </summary>
        public void SetCocoonToBoard(Texture2D texture, Vector2 position, int index)
        {
            Cocoon c = new Cocoon(texture, position);
            c.BoardIndex = index;
            ActiveCocoons[LastCocoonIndex++] = c;
        }

        /// <summary>
        /// Reset all cocoons position when ceiling moved down.
        /// </summary>
        public void ResetCocoonPosition()
        {
            foreach (Cocoon c in Globals.CocoonManager.ActiveCocoons)
            {
                if (c == null)
                    continue;
                
                c.Position = Globals.Board.Grids[c.BoardIndex].Position;
            }
        }

        /// <summary>
        /// Set shooting cocoon's position to the nearest grid position on the board.
        /// </summary>
        public void SetCocoonPosition(Cocoon cocoon)
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

            Globals.IsShooting = false;
        }

        /// <summary>
        /// Find the same color of other adjacent cocoon.
        /// </summary>
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

        /// <summary>
        /// Find dangling cocoons.
        /// </summary>
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
                    CheckCocoonDown(c);
                }
            }
        }
    }
}