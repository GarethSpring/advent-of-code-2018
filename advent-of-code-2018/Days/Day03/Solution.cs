using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;

namespace advent_of_code_2018.Days.Day03
{
    public class Solution
    {
        private readonly List<Claim> claims = new List<Claim>();
        private int[,] overlap = new int[1001,1001];

        public int Part1()
        {
            LoadInput();
            CalculateOverlaps();
            return CountOverlaps();
        }

        public int Part2()
        {
            LoadInput();
            CalculateOverlaps();
            FlagOverlaps();

            var result = claims.Where(claim => !claim.Overlapped).First();

            return result.ClaimId;
        }

        private void FlagOverlaps()
        {
            for (int i = 0; i < claims.Count; i++)
            {
                for (int j = 0; j < claims.Count; j++)
                {
                    // Don't compare to self
                    if (i != j)
                    {
                        if (Intersects(claims[i], claims[j]))
                        {
                            claims[i].Overlapped = true;
                            claims[j].Overlapped = true;
                        }
                    }
                }
            }
        }

        private bool Intersects(Claim claim1, Claim claim2)
        {
            var r1 = new Rectangle(claim1.LeftOffset, claim1.TopOffset, claim1.Width, claim1.Height);
            var r2 = new Rectangle(claim2.LeftOffset, claim2.TopOffset, claim2.Width, claim2.Height);

            return r1.IntersectsWith(r2);
        }

        private void CalculateOverlaps()
        {
            foreach (Claim claim in claims)
            {
                ClaimFabric(claim);
            }
        }

        private void ClaimFabric(Claim claim)
        {
            for (int x = claim.LeftOffset; x <= claim.LeftOffset + claim.Width -1; x++)
            {
                for (int y = claim.TopOffset; y <= claim.TopOffset + claim.Height - 1; y++)
                {
                    overlap[x, y]++;
                }
            }
        }

        private int CountOverlaps()
        {
            int overlapCount = 0;

            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    if (overlap[x,y] > 1)
                    {
                        overlapCount++;
                    }
                }
            }

            return overlapCount;
        }

        private void LoadInput()
        {
            var factory = new ClaimFactory();

            File.ReadAllLines(@"days\day03\input\input.csv")
                .ToList()
                .ForEach(x => claims.Add(factory.Create(x)));
        }
    }
}
