using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using advent_of_code_2018.Days.Day07;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace advent_of_code_2018.Tests.Days.Day07
{
    [TestClass]
    public class Day07Tests
    {
        [TestMethod]
        public void Day07_TestPart1()
        {
            var solution = new Solution();

            string result = solution.Part1();

            // not JNGMHBCFVWEUDYTIALQSPXZORK
            /// "  JNGMHBCFVWEUDYTIALQSPXZORK"
<<<<<<< HEAD
            /// //"JGFMDHNBCIVTUWEQYALSPXZORK"
              /// "JGFMDHNBCIVTUWEQYALSPXZORK"
=======
>>>>>>> 1e3fbbf0a9fade467820a6112f0627869d971906

            Assert.IsTrue(result == "CABDFE");
        }
    }
}
