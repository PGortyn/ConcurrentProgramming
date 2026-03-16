using Data;

namespace DataTest
{
    public class DataTest1
    {
        [Fact]
        public void Test1()
        {
            Ball dc = new Ball();
            int result = dc.AddTest(1, 2);
            int expected = 3;
            Assert.Equal(expected, result);
        }
    }
}