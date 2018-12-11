using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day09;

namespace advent_of_code_2018.Tests.Days.Day09
{
    [TestClass]
    public class Day09Tests
    {
        [TestMethod]
        public void Day09_TestPart1()
        {
            var solution = new Solution();
            
            var result = solution.Calculate(71944);

            Assert.IsTrue(result == 418237);            
        }

        [TestMethod]
        public void Day09_TestPart2()
        {
            var solution = new Solution();

            var result = solution.Calculate(71944 * 100);

            Assert.IsTrue(result == 3505711612);
        }
    }
}
