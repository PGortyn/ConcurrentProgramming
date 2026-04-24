using System.Numerics;

namespace Data
{
    public class Ball
    {
        public Vector2 Position { get; private set; }
        public Vector2 Speed { get; private set; }
        public float Radius { get; private set; }

        public Ball()
        {
            
        }
            
        public Ball(Vector2 pos, float r)
        {
            Position = pos;
            Radius = r;
        }
        
    }
}
