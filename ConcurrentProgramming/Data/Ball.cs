using System.Numerics;

namespace Data
{
    public class Ball: ABall
    {
        public Ball(Vector2 pos, Vector2 vel, float r)
        {
            Position = pos;
            Velocity = vel;
            Radius = r;
        }

        public override void UpdatePosition(float maxHeight, float maxWidth)
        {
            Position += Velocity;
            float posX = Math.Clamp(Position.X, Radius, maxWidth - Radius);
            float posY = Math.Clamp(Position.Y, Radius, maxHeight - Radius);
            Position = new Vector2(posX, posY);
        }
    }
}
