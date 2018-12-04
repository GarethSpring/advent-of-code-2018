using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day04;

namespace advent_of_code_2018.Tests.Days.Day04
{
    [TestClass]
    public class Day04Tests
    {
        [TestMethod]
        public void Day04_TestPart1()
        {           
            var solution = new Solution();

            int result = solution.Part1();

            Assert.IsTrue(result == 14346);
        }

        [TestMethod]
        public void Day04_TestPart2()
        {
            var solution = new Solution();
            
            int result = solution.Part2();

            Assert.IsTrue(result == 5705 );
        }
    }
}
