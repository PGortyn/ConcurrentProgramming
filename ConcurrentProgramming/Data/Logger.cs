using System.Collections.Concurrent;
using System.Numerics;
using System.Text;
namespace Data;

public class Logger : IDisposable
{
    private const string DIRECTORY = "../../../../logs";
    private const string LOG_PREFIX = "log_";
    private const string LOG_SUFFIX = ".txt";
    private readonly TimeSpan m_FlushInterval = TimeSpan.FromSeconds(2);
    private readonly TimeSpan m_RetrayDelay = TimeSpan.FromMilliseconds(100);

    private readonly ConcurrentQueue<string> m_LogQueue = new ConcurrentQueue<string>();
    private StreamWriter m_Writer;
    private Task m_FlushTask;
    private CancellationTokenSource m_CancelToken;
    private readonly object m_Sync = new object();
    private bool m_IsDisposed = false;

    public Logger()
    {
        Directory.CreateDirectory(DIRECTORY);

        CreateLogFile();
        StartFlashTask();
    }

    private void CreateLogFile()
    {
        lock (m_Sync)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string logFilePath = Path.Combine(DIRECTORY, $"{LOG_PREFIX}{timestamp}{LOG_SUFFIX}");
            m_Writer?.Dispose();
            m_Writer = new StreamWriter(logFilePath, true, Encoding.ASCII);
            m_Writer.AutoFlush = false;
        }
    }
    
    private void StartFlashTask()
    {
        m_CancelToken = new CancellationTokenSource();
        m_FlushTask = Task.Run(async () =>
        {
            while(!m_CancelToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(m_FlushInterval, m_CancelToken.Token);
                    await FlushQueueToFileAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            {
                
            }
        }, m_CancelToken.Token);
    }
    
    private async Task FlushQueueToFileAsync()
    {
        bool success = false;
        while(!success)
        {
            try
            {
                lock (m_Sync)
                {
                    while(m_LogQueue.TryDequeue(out string entry))
                    {
                        m_Writer.WriteLine(entry);
                    }
                    m_Writer.Flush();
                }
                success = true;
            }
            catch (IOException e)
            {
                Console.WriteLine("Write error, waiting");
                await Task.Delay(m_RetrayDelay);
            }
            catch (Exception e)
            {
                Console.WriteLine("Writing delay, not IO, breaking...");
                break;
            }
        }
    }

    public void LogWallCollision(ABall ball, Vector2 pos)
    {
        if (ball != null)
        {
            string entryTime = DateTime.Now.ToString("u");
            string ID = ball.ID.ToString();
            string position = $"({pos.X}, {pos.Y})";
            string entry = $"At {entryTime} Ball of ID: {ID} collided with wall at: {position}";
            m_LogQueue.Enqueue(entry);
        }
    }

    public void LogBallsCollision(ABall ball1, ABall ball2, Vector2 pos)
    {
        if (ball1 != null && ball2 != null)
        {
            string entryTime = DateTime.Now.ToString("u");
            string ID1 = ball1.ID.ToString();
            string ID2 = ball2.ID.ToString();
            string position = $"({pos.X}, {pos.Y})";
            string entry = $"At {entryTime} Ball of ID: {ID1} collided with ball of ID: {ID2} at: {position}";
            m_LogQueue.Enqueue(entry);
        }
    }

    public void Dispose()
    {
        if (m_IsDisposed)
        {
            return;
        }

        m_IsDisposed = true;
        m_CancelToken?.Cancel();

        try
        {
            m_FlushTask?.Wait();
            FlushQueueToFileAsync().GetAwaiter().GetResult();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while closing logger:", e.Message);
        }
        finally
        {
            m_Writer?.Dispose();
            m_CancelToken?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
