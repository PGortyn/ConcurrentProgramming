using Data;
namespace Logic;

public interface ILogicManager
{
    public event EventHandler OnBallsUpdated;
    
    public void AddBalls(int amount, bool forceClear = false);
    public List<Ball> GetBalls();
    public void Update(float delta = 0);
    public void UpdateSize(float width, float height);
    public void HandleWallCollision(ABall ball);
    public bool AreBallsColliding(ABall ballA, ABall ballB);
    public void HandleBallCollision(ABall ballA, ABall ballB);
}
