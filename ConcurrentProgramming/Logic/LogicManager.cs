using System.Numerics;
using Data;
using Timer = System.Timers.Timer;
using System.Diagnostics;

namespace Logic
{
    public class LogicManager : ILogicManager
    {
        private readonly object UpdateLock = new object();

        private readonly Timer m_Timer;
        
        public static float TEMP_WIDTH_FIX = 38;
        public static float TEMP_HEIGHT_FIX = 110;
        
        private readonly Stopwatch m_StopWatch = new Stopwatch();
        private long m_LastTicks;
        public float m_DeltaTime;
        
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
            
            m_Timer = new Timer();
            m_Timer.Elapsed += (sender, e) =>
            {
                m_StopWatch.Start();
                long currTicks = m_StopWatch.ElapsedTicks;
                long deltaTicks = currTicks - m_LastTicks;
                m_LastTicks = currTicks;
                m_DeltaTime = (float)deltaTicks / Stopwatch.Frequency;
                // Console.WriteLine(m_DeltaTime.ToString());
                Update();
                OnBallsUpdated?.Invoke(this, EventArgs.Empty);
            };
            m_Timer.Interval = 16;
            m_Timer.Start();
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
            lock (UpdateLock)
            {
                List<Ball> Balls = m_BallCollection.GetBalls();
                Parallel.ForEach(Balls, ball =>
                {
                    if (ball != null)
                    {
                        ball.UpdatePosition(Height, Width, m_DeltaTime);
                        HandleWallCollision(ball);
                    }
                });
                Parallel.For(0, Balls.Count, i =>
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
                });
            }
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
                    Vector2 v1 = first.Velocity;
                    Vector2 p1 = first.Position;
                    float m1 = first.Mass;
                    
                    Vector2 v2 = second.Velocity;
                    Vector2 p2 = second.Position;
                    float m2 = second.Mass;
                    
                    // collision normal
                    Vector2 norm = p2 - p1;
                    float distance = norm.Length();
                    if (distance == 0) return;
                    norm /= distance;
                    
                    // relative velocity
                    Vector2 relativeVel = v2 - v1;
                    float velAlongNorm = Vector2.Dot(relativeVel, norm);
                    
                    if (velAlongNorm > 0) return;

                    // Elasticity (1 = perfectly elastic
                    float e = 1;
                    // impulse scalar
                    float impulseScalar = -(1 + e) * velAlongNorm;
                    impulseScalar /= (1 / m1) + (1 / m2);
                    Vector2 impulse = impulseScalar * norm;

                    v1 -= impulse / m1;
                    v2 += impulse / m2;

                    first.SetVelocity(v1);
                    second.SetVelocity(v2);
                    
                    // Correct position slightly
                    float penetration = first.Radius + ballB.Radius - distance;
                    if (penetration > 0)
                    {
                        float percent = 0.8f;
                        Vector2 correction = norm * penetration * percent;

                        p1 -= correction / m1;
                        p2 += correction / m2;

                        first.SetPosition(p1);
                        second.SetPosition(p2);
                    }
                }
            }
        }
    }
}
