using System;

namespace advent_of_code_2018.Days.Day04
{
    public class GuardRecord
    {
        public DateTime EventDate { get; set; }

        public int GuardId { get; set; }

        public GuardAction EventType { get; set; }
    }
}
