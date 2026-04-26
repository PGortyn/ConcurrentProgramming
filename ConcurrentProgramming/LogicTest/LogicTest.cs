using Logic;

namespace LogicTest
{
    public class LogicTest
    {
        [Fact]
        public void CreateLogicManagerTest()
        {
            LogicManager lm = new LogicManager(200, 100, 50);
            float expectedW = 200;
            float expectedH = 150;
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
            LogicManager lm = new LogicManager(200, 100, 50);
            
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
    }
}