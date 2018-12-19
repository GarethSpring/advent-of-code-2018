using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent_of_code_2018.Days.Day19;

namespace advent_of_code_2018.Tests.Days.Day19
{
    [TestClass]
    public class Day19Tests
    {
        [TestMethod]
        public void Day19_TestPart1()
        {
            var solution = new Solution();

            var result = solution.Run(new [] { 0,0,0,0,0,0 });

            Assert.IsTrue(result == 912);
        }

        [TestMethod]
        public void Day19_TestPart2()
        {
            var solution = new Solution();

            var result = solution.Run(new[] { 1, 0, 0, 0, 0, 0 });

            Assert.IsTrue(result == 912);
        }
    }
}
