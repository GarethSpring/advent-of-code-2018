using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace advent_of_code_2018.Days.Day08
{
    public class Solution
    {
        private string input;
        private int curPos;
        private int parentValue;
        private bool firstNode;

        private List<Node> nodes = new List<Node>();

        public int Part1()
        {
            // Increase stack size, not ideal!
            Thread T = new Thread(() => { Part1Private(); }, 1024 * 1024 * 32);

            T.Start();
            T.Join();

            int sum = 0;

            foreach(Node n in nodes)
            {
                n.Metadata.ForEach(m => sum += m);
            }
                        
            return sum;     
        }

        public int Part2()
        {
            Thread T = new Thread(() => { Part2Private(); }, 1024 * 1024 * 32);

            T.Start();
            T.Join();

            return parentValue;
        }

        private void Part1Private()
        {
            try
            {
                LoadInput();

                curPos = 0;
                ParseInput();
            }
            catch (Exception ex)
            {

            }
        }

        private void Part2Private()
        {
            try
            {
                LoadInput();

                curPos = 0;
                firstNode = true;
                ParseInput();

                parentValue = GetChildNodeValue(nodes.First(node => node.IsRoot));
            }
            catch (Exception ex)
            {

            }
        }

        private int GetChildNodeValue(Node node)
        {
            int nodeValue = 0;

            if (node.ChildNodes.Count == 0)
            {
                // If a node has no child nodes, its value is the sum of its metadata entries. 
                node.Metadata.ForEach(i => nodeValue += i);
                return nodeValue;
            }
            else
            {
                // if a node does have child nodes, the metadata entries become indexes which refer to those child nodes
                node.Metadata.Where(n => n != 0).ToList().ForEach(i =>
                {
                    if (i <= node.ChildNodes.Count)
                    {
                        nodeValue += GetChildNodeValue(node.ChildNodes[i - 1]);
                    }
                });                
            }

            return nodeValue;
        }

        private void ParseInput()
        {           
            CreateNode();            
        }

        private Node CreateNode()
        {
            if (curPos == input.Length - 1)
            {
                return null;
            }

            Node node = new Node();
            node.ChildNodeCount = Consume();
            node.MetaDataCount = Consume();
            if (firstNode)
            {
                node.IsRoot = true;
                firstNode = false;
            }

            if (node.ChildNodeCount > 0)
            {
                for (int i = 0; i < node.ChildNodeCount; i++)
                {
                   node.ChildNodes.Add(CreateNode());
                }
            }
            
            if (node.MetaDataCount > 0)
            {
                for (int i = 0; i < node.MetaDataCount; i++)
                {
                    node.Metadata.Add(Consume());                     
                }
            }

            nodes.Add(node);

            return node;
        }

        private int Consume()
        {
            int result = 0;            

            if (curPos == input.Length - 1)
            {
                // EOF when consuming 1
                return Convert.ToInt32(input.Substring(curPos, 1));                
            }

            if (curPos == input.Length - 2)
            {
                // EOF when consuming 2
                result = Convert.ToInt32(input.Substring(curPos, 2));
                curPos += 1;
                return result;
            }

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

        public bool IsRoot { get; set; }

        public Node()
        {
            ChildNodes = new List<Node>();
            Metadata = new List<int>();
        }        
    }
}
