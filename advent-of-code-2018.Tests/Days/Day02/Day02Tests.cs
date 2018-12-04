using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day02;

namespace advent_of_code_2018.Tests.Days.Day02
{
    [TestClass]
    public class Day02Tests
    {
        [TestMethod]
        public void Day02_TestPart1()
        {
            var solution = new Solution();

            int result = solution.Part1();

            Assert.IsTrue(result == 6150);
        }

        [TestMethod]
        public void Day02_TestPart2()
        {
            var solution = new Solution();

            string result = solution.Part2();

            Assert.AreEqual(result, "rteotyxzbodglnpkudawhijsc");
        }
    }
}
