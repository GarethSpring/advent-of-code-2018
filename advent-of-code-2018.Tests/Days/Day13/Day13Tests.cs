using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day13;

namespace advent_of_code_2018.Tests.Days.Day13
{
    [TestClass]
    public class Day13Tests
    {
        [TestMethod]
        public void Day13_TestPart1()
        {
            var solution = new Solution();

            var result = solution.Part1();

            Assert.IsTrue(result == "64,57");
        }

        [TestMethod]
        public void Day13_TestPart2()
        {
            var solution = new Solution();

            var result = solution.Part2();

            Assert.IsTrue(result == "136,8");
        }
    }
}
