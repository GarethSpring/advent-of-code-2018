using advent_of_code_2018.Days.Day14;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace advent_of_code_2018.Tests.Days.Day14
{
    [TestClass]
    public class Day14Tests
    {
        [TestMethod]
        public void Day14_TestPart1()
        {
            var solution = new Solution();

            var result = solution.Part1();

            Assert.IsTrue(result == "7121102535");
        }

        [TestMethod]
        public void Day14_TestPart2()
        {
            var solution = new Solution();

            var result = solution.Part2();

            Assert.IsTrue(result == 20236441);
        }
    }
}
