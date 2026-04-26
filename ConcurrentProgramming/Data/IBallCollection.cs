namespace Data;

public interface IBallCollection
{
    List<Ball> GetBalls();
    void AddBalls(int amount, float canvasWidth, float canvaHeight, bool forceClear = false);
}
