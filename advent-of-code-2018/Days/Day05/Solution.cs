using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace advent_of_code_2018.Days.Day05
{
    public class Solution
    {
        private Dictionary<char, int> polymerLengths = new Dictionary<char, int>();

        private Dictionary<char, char> reactions = new Dictionary<char, char>()
            {
                {'a', 'A'},
                {'b', 'B'},
                {'c', 'C'},
                {'d', 'D'},
                {'e', 'E'},
                {'f', 'F'},
                {'g', 'G'},
                {'h', 'H'},
                {'i', 'I'},
                {'j', 'J'},
                {'k', 'K'},
                {'l', 'L'},
                {'m', 'M'},
                {'n', 'N'},
                {'o', 'O'},
                {'p', 'P'},
                {'q', 'Q'},
                {'r', 'R'},
                {'s', 'S'},
                {'t', 'T'},
                {'u', 'U'},
                {'v', 'V'},
                {'w', 'W'},
                {'x', 'X'},
                {'y', 'Y'},
                {'z', 'Z'}
            };

        public string Part1()
        {
            return ReactPolymer(LoadInput());
        }

        public int Part2()
        {
            foreach(KeyValuePair<char,char> reaction in reactions)
            {
                CalcPolymerLength(reaction.Key, reaction.Value, LoadInput());
            }

            char minLength = polymerLengths.Aggregate((x, y) => x.Value < y.Value ? x : y).Key;

            return polymerLengths[minLength];
        }

        private void CalcPolymerLength(char a, char b, string input)
        {
            input = input.Replace(a.ToString(), "");
            input = input.Replace(b.ToString(), "");
            input = ReactPolymer(input);

            polymerLengths.Add(a, input.Length);
        }

        private string LoadInput()
        {
            return File.ReadAllText(@"days\day05\input\input.txt");
        }

        private string ReactPolymer(string input)
        {
            int curPos = 1;

            while (curPos < input.Length)
            {
                if (Match(input[curPos], input[curPos - 1]))
                {
                    input = input.Remove(curPos - 1, 2);
                    curPos -= 2;
                    if (curPos <= 0)
                    {
                        curPos = 1;
                    }
                }

                curPos++;
            }

            return input;
        }

        private bool Match(char a, char b)
        {
            return reactions.Any(kvp => (kvp.Key == a && kvp.Value == b) | (kvp.Key == b && kvp.Value == a)) ? true : false;
        }
    }
}
