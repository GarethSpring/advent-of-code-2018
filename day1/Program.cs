using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;

namespace Day1
{
    class Program
    {
        private static readonly List<int> Frequencies = new List<int>();
        private static readonly List<int> Seen = new List<int>();

        static void Main()
        {
            GetFrequencies();

            Part1();

            Console.WriteLine();

            Part2();

            Console.ReadLine();
        }

        static void GetFrequencies()
        {
            foreach (string line in File.ReadAllLines("day1.csv"))
            {
                Frequencies.Add(Convert.ToInt32(line.Replace('+',' ')));
            }            
        }

        static void Part1()
        {
            int current = 0;
            
            foreach (int freq in Frequencies)
            {
                current += freq;
            }

            Console.WriteLine(current);
        }

        static void Part2()
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

                        Console.WriteLine(current);
                        end = true;
                        break;
                    }

                    Seen.Add(current);                    
                }
            }           
        }
    }
}
