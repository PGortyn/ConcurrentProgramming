using Data;
using Logic;

namespace LogicTest
{
    public class LogicTest
    {
        [Fact]
        public void CreateLogicManagerTest()
        {
            float w = 500;
            float h = 300;
            LogicManager lm = new LogicManager(w, h);
            float expectedW = w - LogicManager.TEMP_WIDTH_FIX;
            float expectedH = h - LogicManager.TEMP_HEIGHT_FIX;
            Assert.Equal(lm.Width,expectedW);
            Assert.Equal(lm.Height,expectedH);
        }

        [Fact]
        public void AddBallsTest()
        {
            float w = 500;
            float h = 300;
            LogicManager lm = new LogicManager(w, h);
            
            lm.AddBalls(4);
            List<Ball> Balls = lm.GetBalls();
            float expectedAmount = 4;
            Assert.Equal(Balls.Count, expectedAmount);
            
            lm.AddBalls(3);
            Balls = lm.GetBalls();
            expectedAmount = 7;
            Assert.Equal(Balls.Count, expectedAmount);

            lm.AddBalls(3, true);
            Balls = lm.GetBalls();
            expectedAmount = 3;
            Assert.Equal(Balls.Count, expectedAmount);
        }

        [Fact]
        public void UpdateNonRealTimeTest()
        {
            float w = 500;
            float h = 300;
            LogicManager lm = new LogicManager(w, h);
            
            lm.AddBalls(1);
            List<Ball> Balls = lm.GetBalls();
            Ball ball = Balls[0];
            float oldX = ball.Position.X;
            float oldY = ball.Position.Y;
            
            lm.Update();
            Balls = lm.GetBalls();
            float expectedX = Balls[0].Position.X;
            float expectedY = Balls[0].Position.Y;

            if (ball.Velocity.X != 0)
            {
                Assert.NotEqual(oldX, expectedX);
            }
            else
            {
                Assert.Equal(oldX, expectedX);
            }
            
            if (ball.Velocity.Y != 0)
            {
                Assert.NotEqual(oldY, expectedY);
            }
            else
            {
                Assert.Equal(oldY, expectedY);
            }
        }
    }
}