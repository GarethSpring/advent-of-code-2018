using System;
using System.Collections.Generic;
using System.IO;

namespace advent_of_code_2018.Days.Day01
{
    public class Solution
    {
        private readonly List<int> Frequencies = new List<int>();
        private readonly List<int> Seen = new List<int>();

        public int Part1()
        {
            GetFrequencies();

            return CalcFrequency();
        }

        public int Part2()
        {
            GetFrequencies();

            return CalcSeen();
        }

        private void GetFrequencies()
        {
            foreach (string line in File.ReadAllLines(@"days\day01\input\input.csv"))
            {
                Frequencies.Add(Convert.ToInt32(line.Replace('+', ' ')));
            }
        }

        private int CalcFrequency()
        {
            int current = 0;

            foreach (int freq in Frequencies)
            {
                current += freq;
            }

            return current;
        }

        private int CalcSeen()
        {
            int current = 0;
            bool end = false;

            while (!end)
            {
                foreach (int freq in Frequencies)
                {
                    current += freq;
                    if (Seen.Contains(current))
                    {
                        return current;
                    }

                    Seen.Add(current);
                }
            }

            return 0;
        }
    }
}
