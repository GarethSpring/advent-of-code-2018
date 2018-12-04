using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018.Days.Day04
{
    public class Solution
    {
        private List<GuardRecord> records = new List<GuardRecord>();
        private readonly Dictionary<int, TimeSpan> guardSleepDurations = new Dictionary<int, TimeSpan>();

        private readonly List<Guard> guards = new List<Guard>();

        public int Part1()
        {
            ParseInput();

            SortRecords();
            PopulateGuardId();
            CalcMinutesAsleep();

            var guardIdOfMaxValue = guardSleepDurations.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            var longestSleepingGuardRecord = records.Last(x => x.GuardId == guardIdOfMaxValue && x.EventType == GuardAction.Wakes);

            var minute = guards.First(x => x.GuardId == longestSleepingGuardRecord.GuardId).MinuteAsleepTotals.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            return minute * guardIdOfMaxValue;
        }

        public int Part2()
        {
            Part1();

            int sleepyGuardId = 0;
            int mostFrequentMinute = 0;
            int mostFrequentMinuteQuantity = 0;

            foreach (Guard guard in guards)
            {                
                foreach(KeyValuePair<int, int> data in guard.MinuteAsleepTotals)
                {
                    if (data.Value > mostFrequentMinuteQuantity)
                    {
                        mostFrequentMinuteQuantity = data.Value;
                        mostFrequentMinute = data.Key;
                        sleepyGuardId = guard.GuardId;
                    }
                }
            }

            return mostFrequentMinute * sleepyGuardId;
        }

        private void ParseInput()
        {
            var factory = new GuardRecordFactory();                        

            File.ReadAllLines(@"days\day04\input\input.txt").ToList().ForEach(
                x =>
                {                    
                    records.Add(factory.Create(x));                    
                });
        }

        private void SortRecords()
        {
            records = records.OrderBy(r => r.EventDate).ToList();
        }

        private void PopulateGuardId()
        {
            int prevId = 0;

            foreach (GuardRecord record in records)
            {
                if (record.GuardId == 0)
                {
                    record.GuardId = prevId;
                }
                else
                {
                    prevId = record.GuardId;
                }
            }
        }

        private void CalcMinutesAsleep()
        {
            DateTime sleepTime = DateTime.MinValue;

            foreach (GuardRecord record in records)
            {
                if (!guards.Any(x => x.GuardId == record.GuardId))
                {
                    guards.Add(new Guard(record.GuardId));
                }

                switch (record.EventType)
                {
                    case GuardAction.FallsAsleep:
                        sleepTime = record.EventDate;
                        break;
                    case GuardAction.Wakes:

                        var sleepDuration = record.EventDate.Subtract(sleepTime);

                        if (guardSleepDurations.ContainsKey(record.GuardId))
                        {
                            guardSleepDurations[record.GuardId] = guardSleepDurations[record.GuardId] + sleepDuration;

                            Guard relevantGuard = guards.First(x => x.GuardId == record.GuardId);

                            DateTime trackingTime = sleepTime;
                            while (trackingTime < sleepTime + sleepDuration)
                            {                                
                                if (relevantGuard.MinuteAsleepTotals.ContainsKey(trackingTime.Minute))
                                {
                                    relevantGuard.MinuteAsleepTotals[trackingTime.Minute]++;
                                }
                                else
                                {
                                    relevantGuard.MinuteAsleepTotals.Add(trackingTime.Minute, 1);
                                }                                

                                trackingTime = trackingTime.AddMinutes(1);
                            }
                        }
                        else
                        {
                            guardSleepDurations[record.GuardId] = sleepDuration;
                        }

                        break;
                }
            }
        }    
    }    
}
