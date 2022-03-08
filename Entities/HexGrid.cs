using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Colozak.Entities
{
    public class HexGrid : IGameEntity
    {
        public Vector2 Position;
        
        public bool IsEmpty { get; set; }

        public int BoardIndex { get; set; }

        public HexGrid(Vector2 position) 
        {
            Position = position;
            IsEmpty = true;
        }

        public void Update(GameTime gameTime) { }

        public void Draw(SpriteBatch spriteBatch) { }

        public float DistanceTo(Vector2 cocoonPosition)
        {
            return Vector2.Distance(Position, cocoonPosition);
        }
    }
}