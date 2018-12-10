using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day08;

namespace advent_of_code_2018.Tests.Days.Day08
{
    [TestClass]
    public class Day08Tests
    {
        [TestMethod]
        public void Day08_TestPart1()
        {
            var solution = new Solution();
            
            var result = solution.Part1();

            Assert.IsTrue(result == 49426);
        }

        [TestMethod]
        public void Day08_TestPart2()
        {
            var solution = new Solution();

            var result = solution.Part2();

            Assert.IsTrue(result == 40688);
        }
    }
}
