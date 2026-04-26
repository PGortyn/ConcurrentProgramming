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
            
            lm.AddBalls(4);
            float expectedAmount = 4;
            Assert.Equal(lm.Balls.Count, expectedAmount);
            
            lm.AddBalls(3);
            expectedAmount = 7;
            Assert.Equal(lm.Balls.Count, expectedAmount);

            lm.AddBalls(3, true);
            expectedAmount = 3;
            Assert.Equal(lm.Balls.Count, expectedAmount);
        }

        [Fact]
        public void AddBallsTest()
        {
            float w = 500;
            float h = 300;
            LogicManager lm = new LogicManager(w, h);
            
            lm.AddBalls(4);
            float expectedAmount = 4;
            Assert.Equal(lm.Balls.Count, expectedAmount);
            
            lm.AddBalls(3);
            expectedAmount = 7;
            Assert.Equal(lm.Balls.Count, expectedAmount);

            lm.AddBalls(3, true);
            expectedAmount = 3;
            Assert.Equal(lm.Balls.Count, expectedAmount);
        }

        [Fact]
        public void UpdateNonRealTimeTest()
        {
            float w = 500;
            float h = 300;
            LogicManager lm = new LogicManager(w, h);
            
            lm.AddBalls(1);
            Ball ball = lm.Balls[0];
            float expextedX = ball.Position.X + ball.Velocity.X;
            expextedX = Math.Clamp(expextedX, 0 + ball.Radius, w - ball.Radius);
            float expextedY = ball.Position.Y + ball.Velocity.Y;
            expextedY = Math.Clamp(expextedY, 0 + ball.Radius, h - ball.Radius);
            
            lm.Update();
            
            Assert.Equal(lm.Balls[0].Position.X, expextedX);
            Assert.Equal(lm.Balls[0].Position.Y, expextedY);
        }
    }
}