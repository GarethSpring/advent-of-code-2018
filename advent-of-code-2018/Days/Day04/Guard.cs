using System.Collections.Generic;

namespace advent_of_code_2018.Days.Day04
{
    public class Guard
    {
        public int GuardId { get; set; }

        public Dictionary<int, int> MinuteAsleepTotals { get; set; }

        public Guard(int id)
        {
            GuardId = id;

            MinuteAsleepTotals = new Dictionary<int, int>();
        }
    }
}
