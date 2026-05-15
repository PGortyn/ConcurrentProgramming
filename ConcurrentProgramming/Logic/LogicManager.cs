using System.Numerics;
using Data;

namespace Logic
{
    public class LogicManager : ILogicManager
    {
        public static float TEMP_WIDTH_FIX = 38;
        public static float TEMP_HEIGHT_FIX = 110;
        
        public event EventHandler OnBallsUpdated;

        private readonly IBallCollection m_BallCollection;
        
        public float Width { get; private set; }
        public float Height { get; private set; }

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
        }
        
        public List<Ball> GetBalls()
        {
            return m_BallCollection.GetBalls();
        }

        public void Update()
        {
            List<Ball> Balls = m_BallCollection.GetBalls();
            Parallel.ForEach(Balls, ball =>
            {
                if (ball != null)
                {
                    ball.UpdatePosition(Height, Width);
                    HandleWallCollision(ball);
                }
            });
            for (int i = 0; i < Balls.Count; i++)
            {
                for (int j = i + 1; j < Balls.Count; j++)
                {
                    Ball ballA = Balls[i];
                    Ball ballB = Balls[j];
                    if (AreBallsColliding(ballA, ballB))
                    {
                        HandleBallCollision(ballA, ballB);
                    }
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
        
        public void HandleWallCollision(ABall ball)
        {
            float r = ball.Radius;
            if (ball.Position.X - r <= 0 || ball.Position.X + r >= Width)
            {
                ball.MirrorXVelocity();
            }
            if (ball.Position.Y - r <= 0 || ball.Position.Y + r >= Height)
            {
                ball.MirrorYVelocity();
            }
        }
        
        public bool AreBallsColliding(ABall ballA, ABall ballB)
        {
            return Vector2.Distance(ballA.Position, ballB.Position) <= ballA.Radius + ballB.Radius;
        }
        
        public void HandleBallCollision(ABall ballA, ABall ballB)
        {
            ABall first = ballA.ID < ballB.ID ? ballA : ballB;
            ABall second = ballA.ID < ballB.ID ? ballB : ballA;

            lock (first.Sync)
            {
                lock (second.Sync)
                {
                    // Test 
                    first.MirrorXVelocity();
                    first.MirrorYVelocity();
                    second.MirrorXVelocity();
                    second.MirrorYVelocity();
                    Vector2 vel1 = first.Velocity;
                    Vector2 vel2 = second.Velocity;
                    Vector2 newVel1 = vel1 * (-1 * vel2);
                }
            }
        }
    }
}
