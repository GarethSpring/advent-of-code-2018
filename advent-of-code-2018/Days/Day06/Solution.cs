using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2018.Days.Day06
{
    public class Solution
    {
        private const int gridSize = 360;

        private List<GridCoord> coords = new List<GridCoord>();

        private Dictionary<string, GridCoord> areaDict = new Dictionary<string, GridCoord>();

        public int Part1()
        {
            LoadInput();

            PopulateGrid();

            CalculateClosest();

            FlagInfiniteAreaCoords();

            var infiniteAreas = coords.Where(c => c.InifiniteArea).Select(c => c.Name);

            var largestArea = areaDict
                .Where(b => b.Value.Closest != "." && !infiniteAreas.Contains(b.Value.Closest))
                .GroupBy(a => a.Value.Closest)
                .OrderByDescending(g => g.Count())
                .Select(b => b.Key).First();

            var count = areaDict.Count(x => x.Value.Closest == largestArea);

            return count;
        }

        public int Part2()
        {
            Part1();

            foreach (KeyValuePair<string, GridCoord> kvp in areaDict)
            {
                CalcDistanceToAllCoords(kvp.Value);
            }

            return areaDict.Where(b => b.Value.DistanceToAllCoords < 10000).Count();            
        }

        private void CalcDistanceToAllCoords(GridCoord gridCoord)
        {
            foreach(GridCoord c in coords)
            {
                gridCoord.DistanceToAllCoords += GetDistance(gridCoord, c);
            }
        }      

        private void CalculateClosest()
        {
            foreach (GridCoord coord in coords)         
            {
                SetClosestCoordName(coord);                
            }                
        }

        private void SetClosestCoordName(GridCoord coord)        
        {
            foreach (KeyValuePair<string, GridCoord> kvp in areaDict)
            {                           
                int distance = GetDistance(kvp.Value, coord);

                if (distance < kvp.Value.ClosestDistance)
                {
                    kvp.Value.ClosestDistance = distance;
                    kvp.Value.Closest = coord.Name;
                    kvp.Value.IsEquidistant = false;
                }
                else if (distance == kvp.Value.ClosestDistance)
                {
                    kvp.Value.IsEquidistant = true;
                    kvp.Value.Closest = ".";
                    kvp.Value.ClosestDistance = distance;
                }                
            }      
        }

        private int GetDistance(GridCoord gc1, GridCoord gc2)
        {
            return Math.Abs(gc1.X - gc2.X) + Math.Abs(gc1.Y - gc2.Y);
        }

        private void FlagInfiniteAreaCoords()
        {
            foreach (GridCoord coord in areaDict.Values)
            {
                if (IsInfinite(coord.X, coord.Y))
                {
                    foreach(GridCoord c in coords.Where(x => coord.Closest == x.Name))
                    {
                        c.InifiniteArea = true;
                    }
                }
            }
        }

        private bool IsInfinite(int x, int y)
        {
            if (x == 0 || x >= gridSize - 1 || y == 0 || y >= gridSize - 1)
            {
                return true;
            }

            return false;
        }

        private void PopulateGrid()
        {
            bool infiniteArea = false;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    infiniteArea = IsInfinite(i, j);               

                    areaDict.Add($"{i},{j}", new GridCoord
                    {
                        X = i, Y = j, ClosestDistance = Int32.MaxValue, Name = GetKey(i, j), InifiniteArea = infiniteArea
                    });

                }
            }
        }

        private void LoadInput()
        {
            File.ReadAllLines(@"days\day06\input\input.txt").ToList().ForEach(
                x =>
                {
                    string[] line = x.Split(',');
                    coords.Add(new GridCoord
                    {
                        X = Convert.ToInt32(line[0]),
                        Y = Convert.ToInt32(line[1]),
                        Name = GetKey(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]))
                    });
                    
                });
        }

        private string GetKey(GridCoord coord)
        {
            return $"{coord.X},{coord.Y}";
        }

        private string GetKey(int x, int y)
        {
            return $"{x},{y}";
        }
    }
}