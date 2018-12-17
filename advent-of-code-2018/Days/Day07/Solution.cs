using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace advent_of_code_2018.Days.Day07
{
    public class Solution
    {
        private List<Tuple<string, string>> steps = new List<Tuple<string, string>>();

        private Dictionary<string, Step> stepDict = new Dictionary<string, Step>();

        private List<string> stepsComplete = new List<string>();

        private string order = string.Empty;

        public string Part1()
        {           
            LoadInput();

            BuildStepList();

            string startStep = GetStartStep();

            DetermineOrder(startStep);

            return order;
        }

        private string GetStartStep()
        {
            List<Step> candidates = new List<Step>();

            foreach (Step startCandidate in stepDict.Values)
            {
                bool foundParents = false;

                foreach (Step parentCheck in stepDict.Values)
                {
                    if (parentCheck.Parents.Any(p => p == startCandidate.Name))
                    {
                        foundParents = true;
                    }                    
                }

                if (!foundParents)
                {
                    candidates.Add(startCandidate);
                }
            }

            return candidates.OrderBy(x => x.Name).First().Name;
        }

        private void DetermineOrder(string startName)
        {            
            stepDict[startName].IsAvailable = true;

            ProcessSteps();
        }
   

        private void ProcessSteps()
        {
            while (stepDict.Values.Any(s => s.IsAvailable))
            {
                // Choose first alphabetically available step
                Step firstAvailable = stepDict.Values.OrderBy(v => v.Name).First(s => s.IsAvailable);

                order += firstAvailable.Name;
                firstAvailable.IsComplete = true;
                firstAvailable.IsAvailable = false;

                // Evaluate all the other steps for availablility and flag them 
                stepDict.Values.Where(x => !x.IsComplete && PreReqsFilled(x)).ToList().ForEach( s => s.IsAvailable = true);
            }
            
        }

        private bool PreReqsFilled(Step x)
        {
            return x.PreReqs.All(s => stepDict[s].IsComplete);           
        }

        private void BuildStepList()
        {
            foreach (Tuple<string, string> stepTuple in steps)
            {
                Step step;

                if (stepDict.ContainsKey(stepTuple.Item1))
                {
                    step = stepDict[stepTuple.Item1];
                }
                else
                {
                    step = new Step(stepTuple.Item1);
                    stepDict.Add(stepTuple.Item1, step);
                }

                step.Parents.Add(stepTuple.Item2);
                step.Parents.Sort();
            }

            // Add steps with no parents
            foreach (var t in steps)
            {
                if (stepDict.Values.All(x => x.Name != t.Item2))
                {
                    stepDict.Add(t.Item2, new Step(t.Item2));
                }
            }

            // PreReqs
            foreach (Tuple<string, string> stepTuple in steps)
            {
                Step step = new Step(stepTuple.Item2);

                if (stepDict.ContainsKey(stepTuple.Item2))
                {
                    step = stepDict[stepTuple.Item2];
                }

                step.PreReqs.Add(stepTuple.Item1);
                step.PreReqs.Sort();
            }

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
            Parents = new List<string>();
            Name = name;
        }

        public string Name { get; set; }

        public List<string> PreReqs { get; set; }

        public List<string> Parents { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsComplete { get; set; }

    }
}
