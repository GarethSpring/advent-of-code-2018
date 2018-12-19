using advent_of_code_2018.Days.Day07;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace advent_of_code_2018.Tests.Days.Day07
{
    [TestClass]
    public class Day07Tests
    {
        [TestMethod]
        public void Day07_TestPart1()
        {
            var solution = new Solution();

            string result = solution.Part1();          

            Assert.IsTrue(result == "GJFMDHNBCIVTUWEQYALSPXZORK");
        }

        [TestMethod]
        public void Day07_TestPart2()
        {
            var solution = new Solution();

            var result = solution.Part2();

            Assert.IsTrue(result == 1050);
        }
    }
}
