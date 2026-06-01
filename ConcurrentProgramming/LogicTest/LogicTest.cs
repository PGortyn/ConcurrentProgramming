using System.Numerics;
using Data;
using Logic;

namespace LogicTest
{
    public class LogicTest
    {
        private class TestCollection : IBallCollection
        {
            private readonly List<Ball> Balls = new List<Ball>();
            public List<Ball> GetBalls() => Balls;
            public void AddBalls(int amount, float canvasWidth, float canvasHeight, bool forceClear = false) {}
            public void AddBalls(Ball ball, bool forceClear = false)
            {
                if (forceClear)
                {
                    Balls.Clear();
                }
                Balls.Add(ball);
            }
        }

        private ILogicManager CreateLogicWithBalls(Ball ball1, Ball ball2, float width, float height)
        {
            IBallCollection testCollection = new TestCollection();
            testCollection.AddBalls(ball1);
            testCollection.AddBalls(ball2);
            ILogicManager testLogic = new LogicManager(testCollection, width, height, createLogger: false);
            return testLogic;
        }
        
        [Fact]
        public void CreateLogicManagerTest()
        {
            float w = 500;
            float h = 300;
            LogicManager lm = new LogicManager(w, h, createLogger: false);
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
            LogicManager lm = new LogicManager(w, h, createLogger: false);
            
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
        public void UpdateTest()
        {
            float w = 500;
            float h = 300;
            LogicManager lm = new LogicManager(w, h, createLogger: false);
            
            lm.AddBalls(20);
            List<Ball> Balls = lm.GetBalls();
            Ball ball = Balls[0];
            float oldX = ball.Position.X;
            float oldY = ball.Position.Y;
            
            lm.Update(1);
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

        [Fact]
        public void BallWallBounceTest()
        {
            float w = 500;
            float h = 300;
            Ball ball1 = new Ball(new Vector2(250, 299), new Vector2(0, 1), 1, 1, decreaseVelocityMult: true);
            Ball ball2 = new Ball(new Vector2(1, 150), new Vector2(-1, 0), 1, 2, decreaseVelocityMult: true);
            ILogicManager lm = CreateLogicWithBalls(ball1, ball2, w, h);
            lm.Update(1);
            float expectedVel1X = 0;
            float expectedVel1Y = -1;
            float expectedVel2X = 1;
            float expectedVel2Y = 0;
            List<Ball> balls = lm.GetBalls();
            float vel1X = balls[0].Velocity.X;
            float vel1Y = balls[0].Velocity.Y;
            float vel2X = balls[1].Velocity.X;
            float vel2Y = balls[1].Velocity.Y;
            
            Assert.Equal(vel1X, expectedVel1X);
            Assert.Equal(vel1Y, expectedVel1Y);
            Assert.Equal(vel2X, expectedVel2X);
            Assert.Equal(vel2Y, expectedVel2Y);
        }

        [Fact]
        public void BallsCollisionTest()
        {
            float w = 500;
            float h = 300;
            Ball ball1 = new Ball(new Vector2(248.5f, 150), new Vector2(1, 0), 1, 1, decreaseVelocityMult: true);
            Ball ball2 = new Ball(new Vector2(251.3f, 150), new Vector2(-1, 0), 1, 2, decreaseVelocityMult: true);
            ILogicManager lm = CreateLogicWithBalls(ball1, ball2, w, h);
            
            lm.Update(1);
            float expectedVel1X = -1;
            float expectedVel1Y = 0;
            float expectedVel2X = 1;
            float expectedVel2Y = 0;
            List<Ball> balls = lm.GetBalls();
            float vel1X = balls[0].Velocity.X;
            float vel1Y = balls[0].Velocity.Y;
            float vel2X = balls[1].Velocity.X;
            float vel2Y = balls[1].Velocity.Y;
            
            Assert.Equal(vel1X, expectedVel1X);
            Assert.Equal(vel1Y, expectedVel1Y);
            Assert.Equal(vel2X, expectedVel2X);
            Assert.Equal(vel2Y, expectedVel2Y);
        }
    }
}