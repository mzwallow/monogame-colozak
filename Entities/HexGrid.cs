using Microsoft.Xna.Framework;

namespace Colozak.Entities
{
    public class HexGrid
    {
        public Vector2 Position { get; set; }
        public bool IsEmpty { get; set; } = true;

        public HexGrid(Vector2 position) 
        {
            Position = position;
        }
    }
}