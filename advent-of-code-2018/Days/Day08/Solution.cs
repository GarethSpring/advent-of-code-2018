using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2018.Days.Day08
{ 
    public class Solution
    {
        private string input;
        private int curPos;

        private List<Node> nodes = new List<Node>();

        public int Part1()
        {
            LoadInput();

            curPos = 0;
            ParseInput();

            return 0;
        }

        private void ParseInput()
        {
            int curPos = 0;

            while (curPos < input.Length)
            {
                CreateNode();
            }
        }

        private void CreateNode()
        {
            Node node = new Node();
            node.ChildNodeCount = Consume();
            node.MetaDataCount = Consume();

            for (int i = 0; i <= node.ChildNodeCount; i++)
            {
                CreateNode();
            }
        }

        private int Consume()
        {
            int result = 0;

            if (input[curPos + 1] != ' ')
            {
                // Consume two + space
                result = Convert.ToInt32(input.Substring(curPos, 2));
                curPos += 3;
                return result;
            }

            // Consume one + space
            result = Convert.ToInt32(input.Substring(curPos, 1));
            curPos+= 2;
            return result;
        }    

        private void LoadInput()
        {
            input = File.ReadAllText(@"days\day08\input\input.txt");
        }
    }

    public class Node
    {
        public int ChildNodeCount { get; set; }

        public int MetaDataCount { get; set; }

        public List<Node> ChildNodes { get; set; }

        public List<int> Metadata { get; set; }

        public Node()
        {
            ChildNodes = new List<Node>();
            Metadata = new List<int>();
        }        
    }
}
