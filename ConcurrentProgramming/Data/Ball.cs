using System.Numerics;

namespace Data
{
    public class Ball
    {
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; }
        public float Radius { get; private set; }
            
        public Ball(Vector2 pos, Vector2 vel, float r)
        {
            Position = pos;
            Velocity = vel;
            Radius = r;
        }

        public void UpdatePosition(float maxHeight, float maxWidth)
        {
            Position += Velocity;
            float posX = Math.Clamp(Position.X, Radius, maxWidth - Radius);
            float posY = Math.Clamp(Position.Y, Radius, maxHeight - Radius);
            Position = new Vector2(posX, posY);
        }
    }
}
