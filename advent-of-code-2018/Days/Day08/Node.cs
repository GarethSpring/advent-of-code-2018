using System.Collections.Generic;

namespace advent_of_code_2018.Days.Day08
{
    public class Node
    {
        public int ChildNodeCount { get; set; }

        public int MetaDataCount { get; set; }

        public List<Node> ChildNodes { get; set; }

        public List<int> Metadata { get; set; }

        public bool IsRoot { get; set; }

        public Node()
        {
            ChildNodes = new List<Node>();
            Metadata = new List<int>();
        }
    }
}
