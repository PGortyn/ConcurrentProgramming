using System.Numerics;
namespace Data;

public abstract class ABall
{
    public static int MAX_VELOCITY = 5;
    public static float MIN_RADIUS = 10f;
    public static float MAX_RADIUS = 30f;
    
    public Vector2 Position { get; protected set; }
    public Vector2 Velocity { get; protected set; }
    public float Radius { get; protected set; }

    public abstract void UpdatePosition(float maxHeight, float maxWidth);
}
