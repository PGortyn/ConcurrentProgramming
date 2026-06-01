using System.Numerics;

namespace Data
{
    public class Ball: ABall
    {
        public Ball(Vector2 pos, Vector2 vel, float r, int id = 0, bool decreaseVelocityMult = false)
        {
            ID = id;
            Position = pos;
            Velocity = vel;
            Radius = r;
            Mass = Radius * Radius;
            if (decreaseVelocityMult)
            {
                VelocityMultiplier = 1;
            }
        }

        public override void UpdatePosition(float maxHeight, float maxWidth, float deltaTime)
        {
            Position += Velocity * deltaTime * VelocityMultiplier;
            float posX = Math.Clamp(Position.X, Radius, maxWidth - Radius);
            float posY = Math.Clamp(Position.Y, Radius, maxHeight - Radius);
            Position = new Vector2(posX, posY);
        }
        
        public override void MirrorXVelocity()
        {
            Velocity = new Vector2(-1 * Velocity.X, Velocity.Y);
        }
        public override void MirrorYVelocity()
        {
            Velocity = new Vector2(Velocity.X, -1 * Velocity.Y);
        }

        public override void SetVelocity(Vector2 v)
        {
            Velocity = v;
        }

        public override void SetPosition(Vector2 p)
        {
            Position = p;
        }
    }
}
