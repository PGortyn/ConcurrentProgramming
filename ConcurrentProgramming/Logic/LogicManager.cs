using System.Numerics;
using Data;

namespace Logic
{
    public class LogicManager
    {
        public static float TEMP_WIDTH_FIX = 38;
        public static float TEMP_HEIGHT_FIX = 110;
        
        public event EventHandler OnBallsUpdated;

        private readonly IBallCollection m_BallCollection;
        
        // public List<Ball> Balls = new List<Ball>();
        public float Width { get; private set; }
        public float Height { get; private set; }
        // private const int MAX_VELOCITY = 5;
        // private const float MIN_RADIUS = 10f;
        // private const float MAX_RADIUS = 30f;

        public LogicManager(IBallCollection ballCollection, float width, float height)
        {
            m_BallCollection = ballCollection;
            UpdateSize(width, height);
        }
        
        public LogicManager(float width, float height)
        {
            m_BallCollection = new BallCollection();
            UpdateSize(width, height);
        }

        public void AddBalls(int amount, bool forceClear = false)
        {
            m_BallCollection.AddBalls(amount, Width, Height, forceClear);
            // if (forceClear)
            // {
            //     Balls.Clear();
            // }
            // Random rand = new Random();
            // for (int i = 0; i < amount; i++)
            // {
            //     float radius = ((float)rand.NextDouble() * (MAX_RADIUS - MIN_RADIUS)) + MIN_RADIUS;
            //     float posX = (float)rand.NextDouble() * (Width - radius) + radius;
            //     // posX = Math.Clamp(posX, 0, Width);
            //     float posY = (float)rand.NextDouble() * (Height - radius) + radius;
            //     // posY = Math.Clamp(posY, 0, Height);
            //     Vector2 pos = new Vector2(posX, posY);
            //     Vector2 vel = new Vector2(rand.Next(-MAX_VELOCITY, MAX_VELOCITY), rand.Next(-MAX_VELOCITY, MAX_VELOCITY));
            //     Ball ball = new Ball(pos, vel, radius);
            //     Balls.Add(ball);
            // }
        }

        public void Update()
        {
            List<Ball> Balls = m_BallCollection.GetBalls();
            for (int i = 0; i < Balls.Count; i++)
            {
                Ball ball = Balls[i];
                if (ball != null)
                {
                    ball.UpdatePosition(Height, Width);
                }
            }
            OnBallsUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateSize(float width, float height)
        {
            // TEMP FIX Later
            Width = width - TEMP_WIDTH_FIX;
            Height = height - TEMP_HEIGHT_FIX;
        }
        
        public List<Ball> GetBalls()
        {
            return m_BallCollection.GetBalls();
        }
    }
}
