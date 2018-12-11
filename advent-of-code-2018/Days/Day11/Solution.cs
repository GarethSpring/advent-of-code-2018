using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace advent_of_code_2018.Days.Day11
{
    public class Solution
    {
        private const int Input = 5535;        
        private const int GridSize = 300;

        private readonly Dictionary<string, FuelCell> cells = new Dictionary<string, FuelCell>();
        private readonly int[,,] powerCache = new int[301,301,301];

        public string Part1()
        {            
            InitCells();

            int maxPowerXCoord = 0;
            int maxPowerYCoord = 0;
            int maxPower = 0;

            for (int testX = 1; testX < GridSize - 2; testX++)
            {
                for (int testY = 1; testY < GridSize - 2; testY++)
                {
                    int power = GetPowerLevel(testX, testY, 3);
                    {
                        if (power > maxPower)
                        {
                            maxPower = power;
                            maxPowerXCoord = testX;
                            maxPowerYCoord = testY;
                        }
                    }
                }
            }

            return $"{maxPowerXCoord},{maxPowerYCoord}";
        }

        public string Part2()
        {
            InitCells();

            int maxPowerXCoord = 0;
            int maxPowerYCoord = 0;
            int maxPowerSampleSize = 0;
            int maxPower = 0;

            for (int sampleSize = 2; sampleSize <= GridSize; sampleSize++)
            {
                for (int x = 1; x <= GridSize - sampleSize; x++)
                {
                    for (int y = 1; y <= GridSize - sampleSize; y++)
                    {
                        int power = GetPowerLevelWithCache(x, y, sampleSize);
                        {

                            powerCache[x, y, sampleSize] = power;

                            if (power > maxPower)
                            {
                                maxPower = power;
                                maxPowerXCoord = x;
                                maxPowerYCoord = y;
                                maxPowerSampleSize = sampleSize;
                            }
                        }
                    }
                }

                // Progress
                Debug.WriteLine(sampleSize);
            }

            return $"{maxPowerXCoord},{maxPowerYCoord},{maxPowerSampleSize}";
        }

        private int GetPowerLevelWithCache(int x, int y, int size)
        {
            int power = powerCache[x, y, size-1];

            for (int a = x; a < x + size; a++)
            {
                power += cells[$"{a},{ y + size - 1}"].PowerLevel;
            }

            for (int b = y; b < y + size - 1 ; b++)
            {
                power += cells[$"{x + size -1},{b}"].PowerLevel;
            }
            return power;
        }

        private int GetPowerLevel(int x, int y, int size)
        {                                      
            int power = 0;

            for (int a = x; a < x + size; a++)
            {
                for (int b = y; b < y + size; b++)
                {
                    power += cells[$"{a},{b}"].PowerLevel;
                }
            }
            return power;
        }

        private void InitCells()
        {
            for (int x = 1; x <= GridSize; x++)
            {
                for (int y = 1; y <= GridSize; y++)
                {
                    var cell = new FuelCell
                    {
                        RackID = x + 10,
                        X = x, 
                        Y = y
                    };

                    cell.PowerLevel = (cell.RackID * y) + Input;
                    cell.PowerLevel = cell.PowerLevel * cell.RackID;
                    cell.PowerLevel = cell.PowerLevel < 99 ? 0 : Math.Abs(cell.PowerLevel / 100 % 10);
                    cell.PowerLevel -= 5;
                    cell.Id = $"{x},{y}";

                    cells.Add(cell.Id, cell);

                    powerCache[x, y, 1] = cell.PowerLevel;
                }
            }
        }
    }

    public class FuelCell
    {
        public int RackID { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int PowerLevel { get; set; }

        public string Id { get; set; } 
    }    
}

