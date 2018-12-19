using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace advent_of_code_2018.Days.Day07
{
    public class Solution
    {
        private const int BaseStepTimeSecs = 60;

        private List<Tuple<string, string>> steps = new List<Tuple<string, string>>();

        private Dictionary<string, Step> stepDict = new Dictionary<string, Step>();

        private List<string> stepsComplete = new List<string>();

        private List<Worker> workers = new List<Worker>() { new Worker(), new Worker(), new Worker(), new Worker(), new Worker() };

        private Dictionary<char, int> LetterValues = new Dictionary<char, int>()
        {
            { 'A', 1 }, { 'B', 2 }, { 'C', 3 }, { 'D', 4 }, { 'E', 5 }, { 'F', 6 }, { 'G', 7 }, { 'H', 8 },
            { 'I', 9 }, { 'J', 10 }, { 'K', 11 }, { 'L', 12 }, { 'M', 13 }, { 'N', 14 }, { 'O', 15 }, { 'P', 16 },
            { 'Q', 17 }, { 'R', 18 }, { 'S', 19 }, { 'T', 20 }, { 'U', 21 }, { 'V', 22 }, { 'W', 23 }, { 'X', 24 }, { 'Y', 25 }, { 'Z', 26 }

        };

        private int secondsElapsed = 0;

        private string order = string.Empty;

        public string Part1()
        {           
            LoadInput();

            BuildStepList();

            stepDict[GetStartSteps().First().Name].IsAvailable = true;
            
            ProcessSteps();

            return order;
        }

        public int Part2()
        {
            LoadInput();

            BuildStepList();

            GetStartSteps().ForEach(s => s.IsAvailable = true);

            ProcessParallelSteps();

            return secondsElapsed;
        }

        private List<Step> GetStartSteps()
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

            return candidates.OrderBy(x => x.Name).ToList();
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
                stepDict.Values.Where(x => !x.IsComplete && PreReqsFilled(x)).ToList().ForEach(s => s.IsAvailable = true);
            }    
        }

        private void ProcessParallelSteps()
        {
            while (stepDict.Values.Count(s => s.IsComplete) < stepDict.Count())
            {
                Debug.WriteLine($"Second {secondsElapsed}");

                // Choose first alphabetically available step
                // Step firstAvailable = stepDict.Values.OrderBy(v => v.Name).First(s => s.IsAvailable);
                var batch = stepDict.Values.OrderBy(v => v.Name).Where(s => s.IsAvailable && !s.IsComplete)
                    .Take(workers.Where(w => w.TimeUntilAvailable == 0).Count()).ToList();

                batch.ForEach(s =>
                {
                    Worker worker = workers.FirstOrDefault(w => w.TimeUntilAvailable == 0 && w.step == null);

                    if (worker != null)
                    {
                        worker.TimeUntilAvailable = BaseStepTimeSecs + LetterValues[s.Name[0]] - 1; // Count this second as productive
                        Debug.WriteLine($"Starting Step {s.Name} at {secondsElapsed} which will take {worker.TimeUntilAvailable} secs");
                        worker.step = s;
                        s.IsComplete = false;
                        s.IsAvailable = false;
                    }
                });

                workers.ForEach(w =>
                {
                    if (w.TimeUntilAvailable > 0)
                    {
                        w.TimeUntilAvailable = w.TimeUntilAvailable - 1;
                    }
                    else
                    {
                        if (w.step != null)
                        {
                            Debug.WriteLine($"Flagging step {w.step.Name} as complete");
                            w.step.IsComplete = true;
                            order += w.step.Name;
                            w.step = null;
                        }
                    }
                });

                secondsElapsed++;

                // Evaluate all the other steps for availablility and flag them 
                var toFlag = stepDict.Values.Where(
                    x => !x.IsComplete && PreReqsFilled(x) && !workers.Where(w => w.step != null).Any(w => w.step.Name == x.Name))
                    .ToList();

                toFlag.ForEach(s =>
                {
                    s.IsAvailable = true;
                    Debug.WriteLine($"Flagging step {s.Name} as available");
                });
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

    public class Worker
    {
        public int TimeUntilAvailable { get; set; }

        public Step step { get; set; }
    }
}
