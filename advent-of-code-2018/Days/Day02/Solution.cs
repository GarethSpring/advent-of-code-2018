using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace advent_of_code_2018.Days.Day02
{
    public class Solution
    {
        private List<string> boxes = new List<string>();
        private List<char> validchars = new List<char>
            { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z' };      

        public int Part1()
        {
            LoadInput();

            var countTwo = boxes.Where(b => ContainsExactlyX(b, 2)).Count();
            var countThree = boxes.Where(b => ContainsExactlyX(b, 3)).Count();

            return countTwo * countThree;
        }

        public string Part2()
        {
            LoadInput();

            var similarBoxes = GetSimilarBoxes();

            string result = string.Empty;

            for (int i = 0; i < similarBoxes[0].Length; i++)
            {
                if (similarBoxes[0][i].Equals(similarBoxes[1][i]))
                {
                    result += similarBoxes[0][i];
                }
            }

            return result;
        }

        private List<string> GetSimilarBoxes()
        {
            var similarBoxes = new List<string>();

            for (int i = 0; i < boxes.Count; i++)
            {
                for (int j = 0; j < boxes.Count; j++)
                {
                    if (GetCharsDifferent(boxes[i], boxes[j]) == 1)
                    {
                        similarBoxes.AddRange(new List<string> { boxes[i], boxes[j] });
                        return similarBoxes;
                    }
                }
            }

            throw new Exception("Nope");
        }

        private int GetCharsDifferent(string s1, string s2)
        {
            const int BoxNumLength = 26;

            bool[] matches = new bool[s2.Length];

            for (int i = 0; i < s1.Length; i++)
            {
                if (!matches[i] && s1[i] == s2[i])
                {
                    matches[i] = true;
                }
            }

            return BoxNumLength - matches.Count(x => x);
        }

        private bool ContainsExactlyX(string s, int x)
        {
            foreach(char c in validchars)
            {
                if (s.Count(z => z == c) == x)
                {
                    return true;
                }
            }

            return false;
        }

        private void LoadInput()
        {
            boxes = File.ReadAllLines(@"days\day02\input\input.txt").ToList();
        }
    }
}
