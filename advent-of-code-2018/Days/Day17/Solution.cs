using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace advent_of_code_2018.Days.Day17
{
    public class Solution
    {
        private const string InputMatcher = @"([xy])[=]([0-9]{1,4})[,][\s][xy][=]([0-9]{1,4}).{2}([0-9]{1,4})";
        private Regex regex = new Regex(InputMatcher);

        private List<Vein> veins = new List<Vein>();
        private GridSquare[,] grid;
        private int maxX;
        private int maxY;
        private int minX;
        private int minY;

        public int Part1()
        {
            LoadInput();

            BuildEnvironment();

            CalcSettle();

            int prevCount  = GetGridCount();
            int newCount = Int32.MaxValue;

            while (prevCount != newCount)
            {
                prevCount = GetGridCount();
                Flow(500, minY, 500);
                newCount = GetGridCount();
            }

            Visualise();

            int waterCount = 0;

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    GridSquare gs = grid[x, y];

                    if (gs.Status == Material.CanReach || gs.Status == Material.Water)
                    {
                        waterCount++;
                    };
                }
            }

            return waterCount;
        }

        public int Part2()
        {
            LoadInput();

            BuildEnvironment();

            CalcSettle();

            int prevCount = GetGridCount();
            int newCount = Int32.MaxValue;

            while (prevCount != newCount)
            {
                prevCount = GetGridCount();
                Flow(500, minY, 500);
                newCount = GetGridCount();
            }

            //Visualise();

            int waterCount = 0;

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    GridSquare gs = grid[x, y];

                    if (gs.Status == Material.Water)
                    {
                        waterCount++;
                    };
                }
            }

            return waterCount;
        }

        private int GetGridCount()
        {
            int count = 0;
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if (grid[x, y].Status == Material.Water)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private void LoadInput()
        {     
            File.ReadLines(@"days\day17\input\input.txt").ToList().ForEach(line =>
            {
                Match match = regex.Match(line);

                var vein = new Vein
                {
                    VeinType = Convert.ToChar(match.Groups[1].Value),
                    A = Convert.ToInt32(match.Groups[2].Value),
                    B1 = Convert.ToInt32(match.Groups[3].Value),
                    B2 = Convert.ToInt32(match.Groups[4].Value),
                };    
                
                veins.Add(vein);
            });
        }

        private void Flow(int x, int y, int fromX)
        {
            var current = grid[x, y];
            
            if (CanSettle(x, y))
            {
                current.Status = Material.Water;
            }
            else
            {
                current.Status = Material.CanReach;
            }

            GridSquare next;

            if (y != maxY)
            {
                next = grid[x, y + 1];

                if (next.Status == Material.Sand || next.Status == Material.CanReach)
                {
                    Flow(x, y + 1, x);
                }
                else
                {
                    if (x != minX)
                    {
                        next = grid[x - 1, y];

                        if (next.Status == Material.Sand || (next.Status == Material.CanReach && fromX != x - 1))
                        {
                            Flow(x - 1, y, x);
                        }
                    }

                    if (x != maxX)
                    {
                        next = grid[x + 1, y];

                        if (next.Status == Material.Sand || (next.Status == Material.CanReach && fromX != x + 1))
                        {
                            Flow(x + 1, y, x);
                        }
                    }
                }
            }
        }

        private void BuildEnvironment()
        {
            maxX = veins.Where(v => v.VeinType == 'y').Max(v => v.B2);
            int maxX2 = veins.Where(v => v.VeinType == 'x').Max(v => v.A);
            if (maxX2 > maxX)
            {
                maxX = maxX2;
            }

            maxX++;

            maxY = veins.Where(v => v.VeinType == 'x').Max(v => v.B2);
            int maxY2 = veins.Where(v => v.VeinType == 'y').Max(v => v.A);
            if (maxY2 > maxY)
            {
                maxY = maxY2;
            }

            minX = veins.Where(v => v.VeinType == 'y').Min(v => v.B1) - 1;
            minY = veins.Where(v => v.VeinType == 'x').Min(v => v.B1);

            grid = new GridSquare[maxX + 1, maxY + 1];

            // Set Default
            for (int x = 0; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    grid[x, y] = new GridSquare() {Status = Material.Sand};
                }
            }

            // Add Veins
            foreach (var vein in veins)
            {
                if (vein.VeinType == 'x')
                {
                    for (int y = vein.B1; y <= vein.B2; y++)
                    {
                        grid[vein.A, y].Status = Material.Clay;
                    }
                }

                if (vein.VeinType == 'y')
                {
                    for (int x = vein.B1; x <= vein.B2; x++)
                    {
                        grid[x, vein.A].Status = Material.Clay;
                    }
                }
            }

            grid[500, 0].Status = Material.Spring;

        }

        private void CalcSettle()
        {
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = 0; y <= maxY; y++)
                {
                    grid[x, y].CanSettle = CanSettle(x, y);
                }
            }
        }

        private bool CanSettle(int x, int y)
        {
            // If square to left and right bounded by clay, and all squares under those squares are clay, then can Settle

            GridSquare square = grid[x, y];            

            if ((square.Status != Material.Sand && square.Status != Material.CanReach) || x == minX || x == maxX || y == maxY)
                return false;
            
            bool foundClay = false;
            int x1 = x;
            while (!foundClay && x1 != minX)
            {
                if (grid[x1, y].Status == Material.Clay)
                {
                    foundClay = true;
                }

                x1--;
            }

            if (!foundClay)
                return false;

            foundClay = false;
            int x2 = x;
            while (!foundClay && x2 != maxX)
            {
                if (grid[x2, y].Status == Material.Clay)
                {
                    foundClay = true;
                }

                x2++;
            }

            if (!foundClay)
                return false;

            // Check all squares under found squares are clay or water
            for (int i = x1 + 1; i < x2; i++)
            {
                var sq = grid[i, y + 1];
                if (sq.Status != Material.Clay && sq.Status != Material.Water)
                {
                    return false;
                }
            }

            return true;
        }

        private void Visualise()
        {
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {                  
                    switch (grid[x, y].Status)
                    {
                        case Material.Sand:
                            Debug.Write('.');
                            break;
                        case Material.Clay:
                            Debug.Write('#');
                            break;
                        case Material.Water:
                            Debug.Write('~');
                            break;
                        case Material.Spring:
                            Debug.Write('+');
                            break;
                        case Material.CanReach:
                            Debug.Write('|');
                            break;
                    }
                }

                Debug.WriteLine(' ');                         
            }
        }
    }

    public class Vein
    {
        public int A { get; set; }

        public int B1 { get; set; }

        public int B2 { get; set; }

        public char VeinType { get; set; }
    }

    public class GridSquare
    {
        public Material Status { get; set; }

        public bool CanSettle { get; set; }
    }

    public enum Material
    {
        Sand, Clay, Water, Spring, CanReach
    };    
}
