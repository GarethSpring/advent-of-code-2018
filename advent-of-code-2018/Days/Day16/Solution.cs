using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace advent_of_code_2018.Days.Day16
{
    public class Solution
    {
        private Dictionary<string, Func<int, int, int, int>> opCodes = new Dictionary<string, Func<int, int, int, int>>();
        Dictionary<int, string> opCodeLookup = new Dictionary<int, string>();
        private List<Sample> samples = new List<Sample>();

        private const string InstructionMatcher = @"([0123456789]{1,2})\s([0123456789]{1,2})\s([0123456789]{1,2})\s([0123456789]{1,2})";
        private Regex regex = new Regex(InstructionMatcher);

        private int[] registers;        

        public int Part1()
        {
            LoadInput();            

            Init();

            int result = CheckSamples();

            return result;
        }

        public int Part2()
        {
            LoadInput();

            Init();

            DiscoverOpCodes();

            return 0;
        }

        private void Init()
        {
            registers = new int[] { 0, 0, 0, 0 };

            opCodes.Add("addr", Addr);
            opCodes.Add("addi", Addi);
            opCodes.Add("mulr", Mulr);
            opCodes.Add("muli", Muli);
            opCodes.Add("banr", Banr);
            opCodes.Add("bani", Bani);
            opCodes.Add("borr", Borr);
            opCodes.Add("bori", Bori);
            opCodes.Add("setr", Setr);
            opCodes.Add("seti", Seti);
            opCodes.Add("gtir", Gtir);
            opCodes.Add("gtri", Gtri);
            opCodes.Add("gtrr", Gtrr);
            opCodes.Add("eqir", Eqir);
            opCodes.Add("eqri", Eqri);
            opCodes.Add("eqrr", Eqrr);
        }

        private int CheckSamples()
        {
            int sampleCounter = 0;
            
            foreach(Sample s in samples)
            {            

                foreach (var func in opCodes.Values)
                {                    
                    registers[0] = s.Before[0];
                    registers[1] = s.Before[1];
                    registers[2] = s.Before[2];
                    registers[3] = s.Before[3];                    

                    // execute instruction in sample
                    func(s.Instruction[1], s.Instruction[2], s.Instruction[3]);

                    bool match = true;

                    for (int i = 0; i < 4; i++)
                    {
                        if (registers[i] != s.After[i])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                        s.OpCodeMatches++;
                }
                
               

                sampleCounter++;
            }

            return samples.Count(s => s.OpCodeMatches >= 3);
        }

        private void DiscoverOpCodes()
        {           
            string key = string.Empty;

            while (opCodeLookup.Count < opCodes.Count)
            {
                List<int> matchedKeys = new List<int>();
                int opCodeMatches = 0;

                foreach (var func in opCodes.Values)                  
                {                                       
                    foreach (Sample s in samples)
                    {
                        registers[0] = s.Before[0];
                        registers[1] = s.Before[1];
                        registers[2] = s.Before[2];
                        registers[3] = s.Before[3];

                        // execute instruction in sample
                        func(s.Instruction[1], s.Instruction[2], s.Instruction[3]);

                        bool match = true;

                        for (int i = 0; i < 4; i++)
                        {
                            if (registers[i] != s.After[i])
                            {
                                match = false;
                                break;
                            }
                        }
                        
                        if (match)                        
                        {
                            opCodeMatches++;
                            key = opCodes.FirstOrDefault(x => x.Value == func).Key;
                            matchedKeys.Add(s.Instruction[0]);
                        }
                    }

                   
                }

                matchedKeys = matchedKeys.Where(k => !opCodeLookup.Values.Contains(key)).Distinct().ToList();

                if (matchedKeys.Count() == 1)
                {
                    opCodeLookup.Add(s.Instruction[0], key);
                    key = string.Empty;

                }
            }
        }

        private int Addr(int a, int b, int c)
        {
            registers[c] = registers[a] + registers[b];
            return registers[c];
        }

        private int Addi(int a, int b, int c)
        {
            registers[c] = registers[a] + b;

            return registers[c];
        }

        private int Mulr(int a, int b, int c)
        {
            registers[c] = registers[a] * registers[b];

            return registers[c];
        }

        private int Muli(int a, int b, int c)
        {
            registers[c] = registers[a] * b;

            return registers[c];
        }

        private int Banr(int a, int b, int c)
        {
            registers[c] = registers[a] & registers[b];

            return registers[c];
        }

        private int Bani(int a, int b, int c)
        {
            registers[c] = registers[a] & b;

            return registers[c];
        }


        private int Borr(int a, int b, int c)
        {
            registers[c] = registers[a] | registers[b];

            return registers[c];
        }

        private int Bori(int a, int b, int c)
        {
            registers[c] = registers[a] | b;

            return registers[c];
        }

        private int Setr(int a, int b, int c)
        {
            registers[c] = registers[a];

            return registers[c];
        }

        private int Seti(int a, int b, int c)
        {
            registers[c] = a;

            return registers[c];
        }

        private int Gtir(int a, int b, int c)
        {
            registers[c] = a > registers[b] ? 1 : 0;

            return registers[c];
        }

        private int Gtri(int a, int b, int c)
        {
            registers[c] = registers[a] > b ? 1 : 0;

            return registers[c];
        }

        private int Gtrr(int a, int b, int c)
        {
            registers[c] = registers[a] > registers[b] ? 1 : 0;

            return registers[c];
        }

        private int Eqir(int a, int b, int c)
        {
            registers[c] = a == registers[b] ? 1 : 0;

            return registers[c];
        }

        private int Eqri(int a, int b, int c)
        {
            registers[c] = registers[a] == b ? 1 : 0;

            return registers[c];
        }

        private int Eqrr(int a, int b, int c)
        {
            registers[c] = registers[a] == registers[b] ? 1 : 0;

            return registers[c];
        }

        private void LoadInput()
        {
            var rawInput = new List<string>();

            File.ReadLines(@"days\day16\input\input1.txt").ToList().ForEach(line => rawInput.Add(line));

            for (int i = 0; i < rawInput.Count(); i++)
            {
                Sample sample = new Sample();

                i++;

                //Before
                sample.Before[0] = Convert.ToInt32(rawInput[i].Substring(9, 1));
                sample.Before[1] = Convert.ToInt32(rawInput[i].Substring(12, 1));
                sample.Before[2] = Convert.ToInt32(rawInput[i].Substring(15, 1));
                sample.Before[3] = Convert.ToInt32(rawInput[i].Substring(18, 1));

                i++;

                // Instructions
                Match match = regex.Match(rawInput[i]);
                sample.Instruction[0] = Convert.ToInt32(match.Groups[1].Value);
                sample.Instruction[1] = Convert.ToInt32(match.Groups[2].Value);
                sample.Instruction[2] = Convert.ToInt32(match.Groups[3].Value);
                sample.Instruction[3] = Convert.ToInt32(match.Groups[4].Value);

                i++;

                //After
                sample.After[0] = Convert.ToInt32(rawInput[i].Substring(9, 1));
                sample.After[1] = Convert.ToInt32(rawInput[i].Substring(12, 1));
                sample.After[2] = Convert.ToInt32(rawInput[i].Substring(15, 1));
                sample.After[3] = Convert.ToInt32(rawInput[i].Substring(18, 1));

                samples.Add(sample);
            }
            
        }
    }

    public class Register
    {
        public int Value { get; set; }
    }

    public class Sample
    {
        public Sample()
        {
            Before = new int[4];
            Instruction = new int[4];
            After = new int[4];
        }

        public int[] Before { get; set; }

        public int[] Instruction { get; set; }

        public int[] After { get; set; }

        public int OpCodeMatches { get; set; }
    }
}
