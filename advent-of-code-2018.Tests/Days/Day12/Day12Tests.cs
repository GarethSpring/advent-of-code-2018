using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day12;

namespace advent_of_code_2018.Tests.Days.Day12
{
    [TestClass]
    public class Day12Tests
    {
        [TestMethod]
        public void Day12_TestPart1()
        {
            var solution = new Solution();

            var result = solution.Part1();

            Assert.IsTrue(result == 4217);
        }

        [TestMethod]
        public void Day12_TestPart2()
        {
            var solution = new Solution();

            var result = solution.Part2();

            Assert.IsTrue(result == 4550000002111);
        }
    }
}
