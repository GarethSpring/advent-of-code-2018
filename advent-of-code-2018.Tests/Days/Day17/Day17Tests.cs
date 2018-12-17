using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day17;

namespace advent_of_code_2018.Tests.Days.Day17
{
    [TestClass]
    public class Day17Tests
    {
        [TestMethod, Ignore ] // Too slow
        public void Day17_TestPart1()
        {
            var solution = new Solution();

            var result = solution.Part1();

            Assert.IsTrue(31861 == result);
        }

        [TestMethod, Ignore] // Too slow
        public void Day17_TestPart2()
        {
            var solution = new Solution();

            var result = solution.Part2();

            Assert.IsTrue(26030 == result);
        }
    }
}
