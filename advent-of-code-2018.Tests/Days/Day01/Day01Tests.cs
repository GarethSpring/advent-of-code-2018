using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day01;

namespace advent_of_code_2018.Tests.Days.Day01
{
    [TestClass]
    public class Day01Tests
    {
        [TestMethod]
        public void TestPart1()
        {
            var Solution = new Solution();

            int result = Solution.Part1();

            Assert.IsTrue(result == 590);
        }

        [TestMethod]
        public void TestPart2()
        {
            var Solution = new Solution();

            int result = Solution.Part2();

            Assert.IsTrue(result == 83445);
        }
    }
}
