using Logic;

namespace LogicTest
{
    public class LogicTest1
    {
        [Fact]
        public void Test1()
        {
            LogicClass ls = new LogicClass();
            bool result = ls.IsWorking(false);
            Assert.True(result);
        }
    }
}