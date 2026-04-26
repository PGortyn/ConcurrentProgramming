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
    }
}
