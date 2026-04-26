using Data;
namespace Logic;

public interface ILogicManager
{
    public event EventHandler OnBallsUpdated;
    
    public void AddBalls(int amount, bool forceClear = false);
    public List<Ball> GetBalls();
    public void Update();
    public void UpdateSize(float width, float height);
}
