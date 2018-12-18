using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace advent_of_code_2018.Days.Day18
{
    public class Solution
    {
        private GridSquare[,] area = new GridSquare[50,50];

        private const char Tree = '|';
        private const char Open = '.';
        private const char Lumberyard = '#';

        private int xMax;
        private int yMax;

        public int Part1()
        {
            LoadInput();

            for (int i = 0; i < 10; i++)
            {
                Tick();
                
            }

            int trees = 0;
            int lumberyards = 0;

            for (int x = 0; x < xMax; x++)
            {
                for (int y = 0; y < yMax; y++)
                {
                    char acre = area[x, y].Acre;

                    if (acre == Tree)
                        trees++;
                    else if (acre == Lumberyard)
                        lumberyards++;
                }
            }

            Visualise();

            return trees * lumberyards;
        }

        public int Part2()
        {
            LoadInput();

            int result = 0;

            int k = (1000000000 - 510) % 28;

            for (int i = 0; i < 510 + k; i++)
            {
                Tick();                

                int trees = 0;
                int lumberyards = 0;

                for (int x = 0; x < xMax; x++)
                {
                    for (int y = 0; y < yMax; y++)
                    {
                        char acre = area[x, y].Acre;

                        if (acre == Tree)
                            trees++;
                        else if (acre == Lumberyard)
                            lumberyards++;
                    }
                }

                result = trees * lumberyards;
            }

            Visualise();

            return result;
        }

        private void Visualise()
        {
            if (!Debugger.IsAttached)
            {
                return;
            }

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    Debug.Write(area[x,y].Acre);
                }

                Debug.WriteLine(" ");
            }           
        }

        private void Tick()
        {
            var areaCopy = CopyArea();

            for (int x = 0; x < xMax; x++)
            {
                for (int y = 0; y < yMax; y++)
                {
                    if (area[x, y].Acre == Open && OpenToTrees(x, y))
                    {
                        areaCopy[x, y].Acre = Tree;
                    }
                    else if (area[x, y].Acre == Tree && TreesToLumberyard(x, y))
                    {
                        areaCopy[x, y].Acre = Lumberyard;
                    }

                    else if (area[x, y].Acre == Lumberyard && LumberyardToOpen(x, y))
                    {
                        areaCopy[x, y].Acre = Open;
                    }
                }
            }

            area = areaCopy;
        }

        private GridSquare[,] CopyArea()
        {
            GridSquare[,] copy = new GridSquare[xMax,yMax];

            for (int x = 0; x < xMax; x++)
            {
                for (int y = 0; y < yMax; y++)
                {
                    copy[x,y] = new GridSquare { Acre = area[x,y].Acre };
                }
            }

            return copy;
        }

        private bool OpenToTrees(int x, int y)
        {
            // An open acre will become filled with trees if three or more adjacent acres contained trees.
            // Otherwise, nothing happens.            
            return GetCandidates(x, y).Count(c => c.Acre == Tree) >= 3;
        }

        private bool TreesToLumberyard(int x, int y)
        {
            // An acre filled with trees will become a lumberyard if three or more adjacent acres were lumberyards.
            // Otherwise, nothing happens.   
            return GetCandidates(x, y).Count(c => c.Acre == Lumberyard) >= 3;
        }

        private bool LumberyardToOpen(int x, int y)
        {
            // An acre containing a lumberyard will remain a lumberyard if it was adjacent to at least one other lumberyard and at least one acre containing trees.
            // Otherwise, it becomes open.
            List<GridSquare> candidates = GetCandidates(x, y);

            if (candidates.Count(c => c.Acre == Lumberyard) >= 1 && candidates.Count(c => c.Acre == Tree) >= 1)
            {
                return false;
            }

            return true;
        }

        private List<GridSquare> GetCandidates(int x, int y)
        {
            var candidates = new List<GridSquare>();

            if (x != 0 && y != 0)
            {
                candidates.Add(area[x - 1, y - 1]);
            }

            if (y != 0)
            {
                candidates.Add(area[x, y - 1]);
            }

            if (x != xMax - 1 && y != 0)
            {
                candidates.Add(area[x + 1, y - 1]);
            }

            if (x != xMax - 1)
            {
                candidates.Add(area[x + 1, y]);
            }

            if (x != xMax - 1 && y != yMax - 1)
            {
                candidates.Add(area[x + 1, y + 1]);
            }


            if (y != yMax - 1)
            {
                candidates.Add(area[x, y + 1]);
            }

            if (x != 0 && y != yMax - 1)
            {
                candidates.Add(area[x - 1, y + 1]);
            }

            if (x != 0)
            {
                candidates.Add(area[x - 1, y]);
            }

            return candidates;
        }

        private void LoadInput()
        {
            int x = 0, y = 0;

            File.ReadLines(@"days\day18\input\input.txt").ToList().ForEach(line =>
            {
                x = 0;

                foreach (char c in line)
                {
                    area[x, y] = new GridSquare
                    {
                        Acre = c
                    };

                    x++;
                }

                y++;

            });

            xMax = x;
            yMax = y;
        }

    }

    public class GridSquare
    {
        public char Acre { get; set; }
    }
}
