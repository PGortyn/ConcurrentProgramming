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

        [Fact]
        public void UpdateBallPositionTest()
        {
            Ball b = new Ball(new Vector2(150, 150), new Vector2(1, -1), 1);
            float expectedX = 151;
            float expectedY = 149;
            b.UpdatePosition(300, 300);
            float bX = b.Position.X;
            float bY = b.Position.Y;
            
            Assert.Equal(bX, expectedX);
            Assert.Equal(bY, expectedY);
        }

        [Fact]
        public void MirrorBallVelocityTest()
        {
            Ball b1 = new Ball(new Vector2(150, 150), new Vector2(1, -1), 1);
            Ball b2 = new Ball(new Vector2(160, 160), new Vector2(-1, 0), 1);
            
            float expectedX1 = -1;
            float expectedY1 = 1;
            float expectedX2 = 1;
            float expectedY2 = 0;
            
            b1.MirrorXVelocity();
            b1.MirrorYVelocity();
            b2.MirrorXVelocity();
            b2.MirrorYVelocity();
            
            float b1X = b1.Velocity.X;
            float b1Y = b1.Velocity.Y;
            float b2X = b2.Velocity.X;
            float b2Y = b2.Velocity.Y;
            
            Assert.Equal(b1X, expectedX1);
            Assert.Equal(b1Y, expectedY1);
            Assert.Equal(b2X, expectedX2);
            Assert.Equal(b2Y, expectedY2);
        }

        [Fact]
        public void SetBallPositionAndVelocityTest()
        {
            Ball b = new Ball(new Vector2(150, 150), new Vector2(1, -1), 1);
            float expectedPX = 160;
            float expectedPY = 90;
            float expectedVX = -4;
            float expectedVY = 2;
            
            b.SetPosition(new Vector2(160, 90));
            b.SetVelocity(new Vector2(-4, 2));
            
            float pX = b.Position.X;
            float pY = b.Position.Y;
            float vX = b.Velocity.X;
            float vY = b.Velocity.Y;
            
            Assert.Equal(pX, expectedPX);
            Assert.Equal(pY, expectedPY);
            Assert.Equal(vX, expectedVX);
            Assert.Equal(vY, expectedVY);
        }
    }
}