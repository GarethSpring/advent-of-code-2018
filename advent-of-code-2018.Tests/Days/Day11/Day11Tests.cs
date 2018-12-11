using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day11;

namespace advent_of_code_2018.Tests.Days.Day11
{
    [TestClass]
    public class Day11Tests
    {
        [TestMethod]
        public void Day11_TestPart1()
        {
            var solution = new Solution();

            var result = solution.Part1(); 
            
            Assert.IsTrue(result == "19,41");
            
        }

        // <summary>
        /// Takes ~9 minutes, so ignore for CI
        /// </summary>
        [TestMethod, Ignore]
        public void Day11_TestPart2()
        {
            var solution = new Solution();

            var result = solution.Part2();

            Assert.IsTrue(result == "237,284,11");                        
        }
    }
}
