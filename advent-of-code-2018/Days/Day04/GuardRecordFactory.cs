using System;

namespace advent_of_code_2018.Days.Day04
{
    public class GuardRecordFactory
    {
        public GuardRecord Create(string input, int guardId = 0)
        {
            var record = new GuardRecord
            {
                EventDate = DateTime.Parse(input.Substring(1, 16))
            };

            if (input.Contains("asleep"))
            {
                record.EventType = GuardAction.FallsAsleep;
                record.GuardId = guardId;
            }
            else if (input.Contains("begins"))
            {
                record.EventType = GuardAction.BeginsShift;

                string tmp = input.Substring(26);
                guardId = Convert.ToInt32(tmp.Substring(0, tmp.IndexOf(' ')));                
                record.GuardId = Convert.ToInt32(guardId);
            }
            else if (input.Contains("wakes"))
            {
                record.EventType = GuardAction.Wakes;
                record.GuardId = guardId;
            }

            return record;
        }
    }
}
