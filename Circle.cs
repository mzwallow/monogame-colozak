using Microsoft.Xna.Framework;
using System;

namespace Colozak
{
    public struct Circle
    {
        public Vector2 Center { get; set; }
        public float Radius { get; set; }

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool Contains(Vector2 point)
        {
            return ((point - Center).Length() <= Radius);
        }

        public bool Intersects(Circle other)
        {
            return ((other.Center - Center).Length() <= (other.Radius + Radius));
        }

        public bool Intersects(Rectangle rectangle)
        {
            // Get the rectangle half width and height
            float rW = (rectangle.Width) / 2;
            float rH = (rectangle.Height) / 2;

            // Get the positive distance. This exploits the symmetry so that we now are
            // just solving for one corner of the rectangle (memory tell me it fabs for 
            // floats but I could be wrong and its abs)
            float distX = Math.Abs(Center.X - (rectangle.Left + rW));
            float distY = Math.Abs(Center.Y - (rectangle.Top + rH));

            if (distX >= Radius + rW || distY >= Radius + rH)
            {
                // Outside see diagram circle E
                return false;
            }
            if (distX < rW || distY < rH)
            {
                // Inside see diagram circles A and B
                return true; // touching
            }

            // Now only circles C and D left to test
            // get the distance to the corner
            distX -= rW;
            distY -= rH;

            // Find distance to corner and compare to circle radius 
            // (squared and the sqrt root is not needed
            if (distX * distX + distY * distY < Radius * Radius)
            {
                // Touching see diagram circle C
                return true;
            }
            return false;

            // Credit
            // https://stackoverflow.com/a/43546279
        }
    }
}