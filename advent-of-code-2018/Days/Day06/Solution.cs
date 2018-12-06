using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

            //FlagInfiniteAreaCoords();

            var z = areaDict.Where(b => b.Value.Closest != ".").GroupBy(a => a.Value.Closest).OrderByDescending(g => g.Count())
                .Select(b => b.Key).First();

            var count = areaDict.Count(x => x.Value.Closest == z && !x.Value.InifiniteArea);
            //// not 10731
            return count;
        }

        private void CalculateClosest()
        {
            // For every marked coord, calculate distance to every coord. If it's less than the existing ClosestDistance, update name to closest coord
            // If equal, mark as equidistant

            //int i = 0;

            foreach (GridCoord coord in coords)
            {
                SetClosestCoordName(coord);
                //Debug.WriteLine(i);
               // i++;
            }                
        }

        private void SetClosestCoordName(GridCoord coord)
        {
            foreach (KeyValuePair<string, GridCoord> kvp in areaDict)
            {
                           
                int distance = Math.Abs(GetDistance(kvp.Value, coord));

                if (distance < kvp.Value.ClosestDistance)
                {
                    kvp.Value.ClosestDistance = distance;
                    kvp.Value.Closest = coord.Name;
                }
                else if (distance == kvp.Value.ClosestDistance)
                {
                    kvp.Value.IsEquidistant = true;
                    kvp.Value.Closest = ".";
                }                
            }      
        }

        private int GetDistance(Coord gc1, Coord gc2)
        {
            return (gc2.X - gc1.X) + (gc2.Y - gc1.Y);
        }

        private void FlagInfiniteAreaCoords()
        {
            foreach (GridCoord coord in areaDict.Values)
            {
                if (IsInfinite(coord.X, coord.Y))
                {
                    foreach(GridCoord c in areaDict.Values.Where(a => a.Closest == coord.Closest))
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
                    

                    if (i == 0 || i >= gridSize - 1 || j == 0 || j >= gridSize - 1)
                    {
                        infiniteArea = true;
                    }
                    else
                    {
                        infiniteArea = false;
                    }

                    areaDict.Add($"{i},{j}", new GridCoord { X = i, Y = j, IsMarked = false, ClosestDistance = Int32.MaxValue, Name = GetKey(i, j), InifiniteArea = infiniteArea});

                }
            }

            foreach (var coord in coords)
            {
                areaDict[GetKey(coord)] = new GridCoord { X = coord.X, Y = coord.Y, IsMarked = true, ClosestDistance = Int32.MaxValue};               
            }
        }

        private void LoadInput()
        {
            File.ReadAllLines(@"days\day06\input\input.txt").ToList().ForEach(
                x =>
                {
                    string[] line = x.Split(',');
                    coords.Add(new Coord
                    {
                        X = Convert.ToInt32(line[0]),
                        Y = Convert.ToInt32(line[1]),
                        Name = GetKey(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]))
                    });
                    
                });
        }

        private string GetKey(Coord coord)
        {
            return $"{coord.X},{coord.Y}";
        }

        private string GetKey(int x, int y)
        {
            return $"{x},{y}";
        }
    }
}
