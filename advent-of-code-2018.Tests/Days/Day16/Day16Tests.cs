using advent_of_code_2018.Days.Day16;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace advent_of_code_2018.Tests.Days.Day16
{
    [TestClass]
    public class Day16Tests
    {
        [TestMethod]
        public void Day16_TestPart1()
        {
            var solution = new Solution();

            var result = solution.Part1();

            Assert.IsTrue(result == 529);
        }

        [TestMethod]
        public void Day16_TestPart2()
        {
            var solution = new Solution();

            var result = solution.Part2();

            //Assert.IsTrue(result == 529);
        }
    }
}
