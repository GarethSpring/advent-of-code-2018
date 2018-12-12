using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018.Days.Day12
{    
    public class Solution
    {
        private Dictionary<long, Pot> pots = new Dictionary<long, Pot>();
        private List<string> input;
        private readonly List<Rule> rules = new List<Rule>();

        public long Part1()
        {
            LoadInput();

            int generations = 20;

            for (int i = 0; i < generations; i++)
            {
                CalcGeneration();
            }

            var result = pots.Where(x => x.Value.ContainsPlant).Select(b => b.Value.Number).Sum();

            return result;
        }

        public long Part2()
        {
            LoadInput();

            // This approach spots a pattern to avoid iterating billions of times
            const long generationsToTest = 50000000000;

            long prevSum = 0;            
            long diff = -1;
            int inconsistentGenerations = 0;

            while (true)
            {
                CalcGeneration();
                long curSum = pots.Where(x => x.Value.ContainsPlant).Select(b => b.Value.Number).Sum();
              
                if (curSum - prevSum == diff)
                {
                    break;
                }

                inconsistentGenerations++;

                diff = curSum - prevSum;
                prevSum = curSum;                
            }
          
            // Sum the inconsistent generations
            var result = pots.Where(x => x.Value.ContainsPlant).Select(b => b.Value.Number).Sum();

            // Now it's a simple multiplication
            result += (generationsToTest - inconsistentGenerations - 1) * diff;

            return result;
        }

        private void CalcGeneration()
        {
            Dictionary<long, Pot> nextGenPots = new Dictionary<long, Pot>();

            // Add empty pots at extremities ..#.# patterns match
            long min = pots.Values.Select(x => x.Number).Min();
            pots.Add(min - 3, new Pot { Number = min - 3 });
            pots.Add(min - 2, new Pot { Number = min - 2});
            pots.Add(min - 1, new Pot { Number = min - 1 });

            long max = pots.Values.Select(x => x.Number).Max();
            pots.Add(max + 3, new Pot { Number = max + 3 });
            pots.Add(max + 2, new Pot { Number = max + 2 });
            pots.Add(max + 1, new Pot { Number = max + 1 });


            foreach (KeyValuePair<long, Pot> kvp in pots)
            {
                Rule match = GetMatchingRule(kvp.Value);
                if (match == null)
                {
                    nextGenPots.Add(kvp.Key, new Pot {Number = kvp.Key });
                    continue;
                }

                nextGenPots.Add(kvp.Key, new Pot { Number = kvp.Key, ContainsPlant = match.Future});
            }

            pots = nextGenPots;
        }        

        /// <summary>
        /// Get the rule, if any, that could apply to the current pot
        /// </summary>
        /// <param name="pot">Pot</param>
        /// <returns>Rule</returns>
        private Rule GetMatchingRule(Pot pot)
        {
            foreach (Rule r in rules)
            {
                if (
                    ((pots.ContainsKey(pot.Number - 2) && pots[pot.Number - 2].ContainsPlant == r.L1))
                    && ((pots.ContainsKey(pot.Number - 1) && pots[pot.Number - 1].ContainsPlant == r.L2))
                    && (pots[pot.Number].ContainsPlant == r.Current)
                    && ((pots.ContainsKey(pot.Number + 1) && pots[pot.Number + 1].ContainsPlant == r.R1))
                    && ((pots.ContainsKey(pot.Number + 2) && pots[pot.Number + 2].ContainsPlant == r.R2))
                        )
                {
                    return r;
                }
            }

            return null;
        }

        private void LoadInput()
        {
            input = File.ReadAllLines(@"days\day12\input\input.txt").ToList();

            int index = 0;

            // Load Initial State
            foreach (char c in input[0])
            {
                pots.Add(index, new Pot
                {
                    Number = index,
                    ContainsPlant =  c == '#' 
                });

                index++;
            }

            // Load rules
            foreach (string s in input)
            {
                if (!s.Contains("=>"))
                    continue;

                Rule rule = new Rule
                {
                    L1 = s[0] == '#',
                    L2 = s[1] == '#',
                    Current = s[2] == '#',
                    R1 = s[3] == '#',
                    R2 = s[4] == '#',
                    Future =  s[9] == '#'
                };

                rules.Add(rule);
            }
        }
        
    }

    public class Pot
    {
        public long Number { get; set; }

        public bool ContainsPlant { get; set; }
    }

    public class Rule
    {
        // L1|L2|Current|R1|R2 => Future

        public bool L1 { get; set; }

        public bool L2 { get; set; }

        public bool Current { get; set; }

        public bool R1 { get; set; }

        public bool R2 { get; set; }

        public bool Future { get; set; }        
    }
}

