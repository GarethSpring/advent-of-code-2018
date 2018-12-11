using System;
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
            
            Assert.IsTrue(result.Item1 == 19);
            Assert.IsTrue(result.Item2 == 41);
        }

        [TestMethod]
        public void Day11_TestPart2()
        {
            var solution = new Solution();

            solution.Part2();       
                        
            // 237,284,11
        }
    }
}
