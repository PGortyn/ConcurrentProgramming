using System.Numerics;
namespace Data;

public abstract class ABall
{
    public static int MAX_VELOCITY = 5;
    public static float MIN_RADIUS = 10f;
    public static float MAX_RADIUS = 30f;
    
    public readonly object Sync = new object();
    private Vector2 m_Position;
    public Vector2 Position 
    { 
        get { lock (Sync) { return m_Position; } }
        protected set { lock (Sync) { m_Position = value; } }
    }
    private Vector2 m_Velocity;
    public Vector2 Velocity
    { 
        get { lock (Sync) { return m_Velocity; } }
        protected set { lock (Sync) { m_Velocity = value; } }
    }
    private float m_Radius;
    public float Radius
    { 
        get { lock (Sync) { return m_Radius; } }
        protected set { lock (Sync) { m_Radius = value; } }
    }
    private float m_Mass;
    public float Mass
    { 
        get { lock (Sync) { return m_Mass; } }
        protected set { lock (Sync) { m_Mass = value; } }
    }
    public int ID { get; protected set; }

    public abstract void UpdatePosition(float maxHeight, float maxWidth);
    public abstract void MirrorXVelocity();
    public abstract void MirrorYVelocity();
    public abstract void SetVelocity(Vector2 v);
    public abstract void SetPosition(Vector2 p);
}
