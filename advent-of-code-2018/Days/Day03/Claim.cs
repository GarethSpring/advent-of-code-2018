namespace advent_of_code_2018.Days.Day03
{
    public class Claim
    {
        public int ClaimId { get; set; }

        public int LeftOffset { get; set; }

        public int TopOffset { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool Overlapped { get; set; }
    }
}
