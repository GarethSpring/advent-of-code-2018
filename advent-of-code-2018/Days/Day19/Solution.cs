using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace advent_of_code_2018.Days.Day19
{
    public class Solution
    {
        private const string Matcher = @"([a-z]{4})\s([0123456789]{1,2})\s([0123456789]{1,2})\s([0123456789]{1,2})";
        private Regex regex = new Regex(Matcher);
        
        private Dictionary<string, Func<int, int, int, int>> opCodes = new Dictionary<string, Func<int, int, int, int>>();
        private List<Instruction> instructions = new List<Instruction>();

        private int[] registers;
        private int instructionPointer = 0;
        private int instructionPointerRegister = 0;


        public int Run(int[] registers)
        {
            this.registers = registers;

            LoadInput();

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
            

            while (true)
            {
                Execute(instructions[instructionPointer]);

                instructionPointer++;

                if (instructionPointer >= instructions.Count)
                {
                    break;
                }
            }

            return registers[0];

        }

        private void Execute(Instruction instruction)
        {            
            registers[instructionPointerRegister] = instructionPointer;

            var func = opCodes[instruction.OpCode];

            func(instruction.A, instruction.B, instruction.C);

            instructionPointer = registers[instructionPointerRegister];

            if (Debugger.IsAttached)
            {
                Debug.WriteLine(
                    $"{instruction.OpCode},{registers[0]},{registers[1]},{registers[2]},{registers[3]},{registers[4]},{registers[5]}");
            }
        }

        private void LoadInput()
        {
            File.ReadLines(@"days\day19\input\input.txt").ToList().ForEach(line =>
            {
                if (line.Contains("#ip"))
                {
                    instructionPointerRegister = Convert.ToInt32(line.Substring(4, 1));                   
                }
                else
                {
                    Match match = regex.Match(line);

                    Instruction instruction = new Instruction()
                    {
                        OpCode = match.Groups[1].Value,
                        A = Convert.ToInt32(match.Groups[2].Value),
                        B = Convert.ToInt32(match.Groups[3].Value),
                        C = Convert.ToInt32(match.Groups[4].Value),
                    };

                    instructions.Add(instruction);
                }
            });
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

    }

    public class Instruction
    {
        public string OpCode { get; set; }

        public int A { get; set; }

        public int B { get; set; }

        public int C { get; set; }
    }
}
