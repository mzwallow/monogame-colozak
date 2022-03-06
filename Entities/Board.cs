using Microsoft.Xna.Framework;

namespace Colozak.Entities
{
    public class Board
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
                        j++;
                    }
                }
                else 
                {
                    for (x = 7*Globals.TILE_SIZE; x < 13*Globals.TILE_SIZE + Globals.TILE_SIZE/2; x += Globals.TILE_SIZE)
                    {
                        Grids[j] = new HexGrid(new Vector2(x, y));
                        j++;
                    }
                }
                i++;
            }
        } 

        public void CreateMap(int[] number)
        {
            for (int i = 0; i < NUM_TILE; i++)
            {
                if (number[i] >= 0 && number[i] < 8)
                {
                    Globals.CocoonManager.SetCocoon(Globals.CocoonsTexture[number[i]], Grids[i].Position, i);
                    Grids[i].IsEmpty = false;
                }
            }
        }
    }
}