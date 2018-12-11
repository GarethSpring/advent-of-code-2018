using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace advent_of_code_2018.Days.Day10
{
    public class Solution
    {
        private List<LightPoint> points = new List<LightPoint>();

        public int Part1()
        {
            LoadInput();

            int count = 0;
            int lastDiff = Int32.MaxValue;

            while (points.Max(x => x.X) - points.Min(x => x.X) > 61) //61
            {
                MovePoints(); 
                              
                count++;                       
            }

            for (int y = points.Min(a => a.Y); y <= points.Max(a => a.Y); y++)
            {
                for (int x = points.Min(a => a.X); x <= points.Max(a => a.X); x++)
                {
                    if (points.Any(p => p.X == x && p.Y == y))
                    {
                        Debug.Write('#');
                    }
                    else
                    {
                        Debug.Write('.');
                    }                    
                }
                Debug.WriteLine(' ');
            }

            return count;
        }

        private void MovePoints()
        {
            foreach (LightPoint point in points)
            {
                point.X += point.XVel;
                point.Y += point.YVel;
            }
        }

        private void LoadInput()
        {            
            File.ReadAllLines(@"days\day10\input\input.txt").ToList().ForEach(l =>
            {
                string firstCoords = l.Substring(l.IndexOf('<') + 1, 14);
                string secondCoords = l.Substring(36, 6);

                var p = new LightPoint
                {
                    X = Convert.ToInt32(firstCoords.Substring(0, 6)),
                    Y = Convert.ToInt32(firstCoords.Substring(7)),
                    XVel = Convert.ToInt32(secondCoords.Substring(0, 2)),
                    YVel = Convert.ToInt32(secondCoords.Substring(3))
                };                                

                points.Add(p);
            });
        }
    }
}
