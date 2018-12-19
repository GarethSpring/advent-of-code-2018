using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day18;

namespace advent_of_code_2018.Tests.Days.Day18
{
    [TestClass]
    public class Day18Tests
    {
        [TestMethod]
        public void Day18_TestPart1()
        {
            var solution = new Solution();
            
            var result = solution.Part1();

            Assert.IsTrue(result == 531417);
        }

        [TestMethod]
        public void Day18_TestPart2()
        {
            var solution = new Solution();

            var result = solution.Part2();

            Assert.IsTrue(result == 205296);
        }
    }
}
