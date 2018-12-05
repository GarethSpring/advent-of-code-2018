using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day05;

namespace advent_of_code_2018.Tests.Days.Day05
{
    [TestClass]
    public class Day05Tests
    {
        [TestMethod]
        public void Day05_TestPart1()
        {
            var solution = new Solution();

            int result = solution.Part1().Length;

            Assert.AreEqual(result, 11720);           
        }

        [TestMethod]
        public void Day05_TestPart2()
        {
            var solution = new Solution();

            int result = solution.Part2();

            Assert.AreEqual(result, 4956);
        }
    }
}
