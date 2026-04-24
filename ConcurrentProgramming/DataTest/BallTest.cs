using System.Numerics;
using Data;

namespace DataTest
{
    public class BallTest
    {
        [Fact]
        public void Test1()
        {
            Ball dc = new Ball(new Vector2(2, 3), 7);
            float expectedX = 2;
            float expectedY = 3;
            float expectedR = 7;
            Assert.Equal(dc.Position.X, expectedX);
            Assert.Equal(dc.Position.Y, expectedY);
            Assert.Equal(dc.Radius, expectedR);
        }
    }
}