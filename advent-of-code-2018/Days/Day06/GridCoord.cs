using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2018.Days.Day06
{
    public class GridCoord 
    {
        public bool IsMarked { get; set; }

        public string Closest { get; set; }

        public bool IsEquidistant { get; set; }

        public int ClosestDistance { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public string Name { get; set; }

        public bool InifiniteArea { get; set; }
    }
}
