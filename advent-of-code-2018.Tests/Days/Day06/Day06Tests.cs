using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day06;

namespace advent_of_code_2018.Tests.Days.Day06
{
    [TestClass]
    public class Day06Tests
    {
        [TestMethod]
        public void Day06_TestPart1()
        {
            var solution = new Solution();
            
            var result = solution.Part1();

            Assert.IsTrue(result == 3894);
        }

        [TestMethod]
        public void Day06_TestPart2()
        {
            var solution = new Solution();

            var result = solution.Part2();

            Assert.IsTrue(result == 39398);
        }
    }
}
