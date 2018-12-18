using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2018.Days.Day16
{
    public class Solution
    {
        private Dictionary<string, Func<int, int, int>> opCodes = new Dictionary<string, Func<int, int, int>>
        {
            //new KeyValuePair<string, Func<int, int, int int>>("addr", Addr)
        };

        private Register rA = new Register();
        private Register rB = new Register();
        private Register rC = new Register();
        private Register rD = new Register();

        private int result;

        public string Part1()
        {
            opCodes.Add("addr", Addr);
            return null;
        }

        private int Addr(int a, int b)
        {
            rC.Value = rA.Value + rB.Value;

            return rC.Value;
        }

        private int Addi(int a, int b)
        {
            rC.Value = rA.Value + b;

            return rC.Value;
        }

    }


    public class Register
    {
        public int Value { get; set; }
    }
}
