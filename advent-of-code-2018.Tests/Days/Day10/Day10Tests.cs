using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day10;

namespace advent_of_code_2018.Tests.Days.Day10
{
    [TestClass]
    public class Day10Tests
    {
        [TestMethod]
        public void Day10_TestBothParts()
        {
            var solution = new Solution();

            var result = solution.Part1();

            Assert.IsTrue(result == 10081);
        }
    }
}
