using System.Numerics;
namespace Data;

public class BallCollection : IBallCollection
{
    private List<Ball> m_Balls = new List<Ball>();
    
    

    public List<Ball> GetBalls()
    {
        return m_Balls;
    }

    public void AddBalls(int amount, float canvasWidth, float canvasHeight, bool forceClear = false)
    {
        if (forceClear)
        {
            m_Balls.Clear();
        }
        Random rand = new Random();
        for (int i = 0; i < amount; i++)
        {
            float radius = ((float)rand.NextDouble() * (ABall.MAX_RADIUS - ABall.MIN_RADIUS)) + ABall.MIN_RADIUS;
            float posX = (float)rand.NextDouble() * (canvasWidth - radius) + radius;
            posX = Math.Clamp(posX, radius, canvasWidth - radius);
            float posY = (float)rand.NextDouble() * (canvasHeight - radius) + radius;
            posY = Math.Clamp(posY, radius, canvasHeight - radius);
            Vector2 pos = new Vector2(posX, posY);
            
            float velX = ((float)rand.NextDouble() * (ABall.MAX_VELOCITY * 2)) - ABall.MAX_VELOCITY;
            float velY = ((float)rand.NextDouble() * (ABall.MAX_VELOCITY * 2)) - ABall.MAX_VELOCITY;
            Vector2 vel = new Vector2(velX, velY);
            Ball ball = new Ball(pos, vel, radius);
            m_Balls.Add(ball);
        }
    }
}
