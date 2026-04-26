using System.Numerics;
using Data;

namespace DataTest
{
    public class BallTest
    {
        [Fact]
        public void CreateBallTest()
        {
            Ball dc = new Ball(new Vector2(2, 3), new Vector2(1, 4), 7);
            float expectedX = 2;
            float expectedY = 3;
            float expectedVelX = 1;
            float expectedVelY = 4;
            float expectedR = 7;
            Assert.Equal(dc.Position.X, expectedX);
            Assert.Equal(dc.Position.Y, expectedY);
            Assert.Equal(dc.Velocity.X, expectedVelX);
            Assert.Equal(dc.Velocity.Y, expectedVelY);
            Assert.Equal(dc.Radius, expectedR);
        }
    }
}