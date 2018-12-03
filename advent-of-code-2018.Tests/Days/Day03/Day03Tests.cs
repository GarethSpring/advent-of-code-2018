using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day03;

namespace advent_of_code_2018.Tests
{
    [TestClass]
    public class Day03Tests
    {
        [TestMethod]
        public void TestPart1()
        {
            var Solution = new Solution();

            int result = Solution.Part1();

            Assert.IsTrue(result == 121259);
        }

        [TestMethod]
        public void TestPart2()
        {
            var Solution = new Solution();

            int result = Solution.Part2();

            Assert.IsTrue(result == 239);
        }
    }
}
