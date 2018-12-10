using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace advent_of_code_2018.Days.Day07
{
    public class Solution
    {
        private List<Tuple<string, string>> steps = new List<Tuple<string, string>>();

        private List<Step> stepList = new List<Step>();

        private List<string> stepsAlreadyUsed = new List<string>();

        public string Part1()
        {
            string result = string.Empty;

            LoadInput();

            result = GetOrder();

            return result;
        }

        private string GetOrder()
        {
            string order = string.Empty;

            foreach (var s in steps)
            {
                Step step;
                if (stepList.Any(x => x.Name == s.Item2))
                {
                    step = stepList.First(x => x.Name == s.Item2);                    
                }
                else
                {
                    step = new Step(s.Item2);
                    stepList.Add(step);
                }

                step.PreReqs.Add(s.Item1);
                step.PreReqs.Sort();
            }

            // add steps with no prereqs
            foreach (var t in steps)
            {
                if (!stepList.Any(x => x.Name == t.Item1))
                {
                    stepList.Add(new Step(t.Item1));
                }
            }


            StringBuilder sb = new StringBuilder();

            string first = stepList.Last().Name;
            string current = stepList.Last().Name;
            string prevChoice = current;
            stepsAlreadyUsed.Add(current);

            sb.Append(current);

            // While ChooseNext != nulll
            while (sb.Length < steps.Count - 1)
            {
                string currentCopy = current;
                current = ChooseNext(current, prevChoice);

                if (current == null)
                {
                    ChooseNext(first, prevChoice);
                }

                prevChoice = currentCopy;
                sb.Append(current);               
                
            }



            //stepArray.ToList().ForEach(s => sb.Append(s.Name));

            return sb.ToString();
        }

        private string ChooseNext(string current, string prevChoice)
        {
            // Get Options           
            var options = new List<string>();

            steps.Where(x => x.Item1 == current && stepsAlreadyUsed.All(t => t != x.Item2)).ToList().ForEach(
                g =>
                {
                    options.Add(g.Item2);
                });

            if (options.Count == 0)
            {              
                if (prevChoice != null)
                {
                    return ChooseNext(prevChoice, null);
                }

                return null;
            }

            options.Sort();

            return options.First();
        }
     
        private void Swap(Step[] array, int i, int j)
        {
            Step temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        private void LoadInput()
        {                      
            File.ReadAllLines(@"days\day07\input\input.txt").ToList().ForEach(step =>
            {
                steps.Add(new Tuple<string, string>(step.Substring(5,1), step.Substring(36,1)));
            });

        }
    }

    public class Step
    {
        public Step(string name)
        {
            PreReqs = new List<string>();
            Name = name;
        }

        public string Name { get; set; }

        public List<string> PreReqs { get; set; }
    }
}
