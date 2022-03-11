using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Colozak.Entities
{
    public class Board : IGameEntity
    {
        public const int NUM_TILE = 90;

        public HexGrid[] Grids;

        public Board()
        {
            Grids = new HexGrid[NUM_TILE];

            int x, y;
            int i = 0;
            int j = 0;
            for (y = 2*Globals.TILE_SIZE + Globals.TILE_SIZE/2; j < NUM_TILE; y += Globals.TILE_SIZE-6)
            {
                if (i % 2 == 0)
                {
                    for (x = 6*Globals.TILE_SIZE + Globals.TILE_SIZE/2; x < 14*Globals.TILE_SIZE; x += Globals.TILE_SIZE)
                    {
                        Grids[j] = new HexGrid(new Vector2(x, y));
                        Grids[j].BoardIndex = j;
                        j++;
                    }
                }
                else 
                {
                    for (x = 7*Globals.TILE_SIZE; x < 13*Globals.TILE_SIZE + Globals.TILE_SIZE/2; x += Globals.TILE_SIZE)
                    {
                        Grids[j] = new HexGrid(new Vector2(x, y));
                        Grids[j].BoardIndex = j;
                        j++;
                    }
                }
                i++;
            }
        }

        public void Reset()
        {
            Grids = new HexGrid[NUM_TILE];

            int x, y;
            int i = 0;
            int j = 0;
            for (y = 2*Globals.TILE_SIZE + Globals.TILE_SIZE/2; j < NUM_TILE; y += Globals.TILE_SIZE-6)
            {
                if (i % 2 == 0)
                {
                    for (x = 6*Globals.TILE_SIZE + Globals.TILE_SIZE/2; x < 14*Globals.TILE_SIZE; x += Globals.TILE_SIZE)
                    {
                        Grids[j] = new HexGrid(new Vector2(x, y));
                        Grids[j].BoardIndex = j;
                        j++;
                    }
                }
                else 
                {
                    for (x = 7*Globals.TILE_SIZE; x < 13*Globals.TILE_SIZE + Globals.TILE_SIZE/2; x += Globals.TILE_SIZE)
                    {
                        Grids[j] = new HexGrid(new Vector2(x, y));
                        Grids[j].BoardIndex = j;
                        j++;
                    }
                }
                i++;
            }
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch) { }

        public void CreateMap(int[] number)
        {
            for (int i = 0; i < NUM_TILE; i++)
            {
                if (number[i] >= 0 && number[i] < 8)
                {
                    Globals.CocoonManager.SetCocoonToBoard(Globals.CocoonManager.CocoonsTexture[number[i]], Grids[i].Position, i);
                    Grids[i].IsEmpty = false;
                }
            }
        }

        public void DropGrids()
        {
            foreach (HexGrid grid in Grids)
            {
                grid.Position.Y += Globals.TILE_SIZE - 6;
                Globals.CocoonManager.ResetCocoonPosition();
            }
        }

        public bool CheckWin()
        {
            foreach (HexGrid grid in Grids)
            {
                if (!grid.IsEmpty)
                    return false;
            }

            return true;
        }

        public bool CheckLose()
        {
            foreach (Cocoon c in Globals.CocoonManager.ActiveCocoons)
            {
                if (c is null)
                    continue;
                
                if (c.CollisionBox.Intersects(Globals.ColliderManager.LoseLine.CollisionBox) && !c.IsMoving)
                    return true;
            }

            return false;
        }
    }
}