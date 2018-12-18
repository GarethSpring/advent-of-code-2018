using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
        }        
    }


    public class Register
    {
        public int Value { get; set; }
    }
}
