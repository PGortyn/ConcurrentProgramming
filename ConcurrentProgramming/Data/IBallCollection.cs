namespace Data;

public interface IBallCollection
{
    List<Ball> GetBalls();
    void AddBalls(int amount, float canvasWidth, float canvasHeight, bool forceClear = false);
    void AddBalls(Ball ball, bool forceClear = false);
}
