using System;

namespace advent_of_code_2018.Days.Day14
{   
    public class Solution
    {
        private int input = 920831;
        private int elf1Recipe;
        private int elf2Recipe;
        private int recipeCounter = 0;

        private int[] recipes = new int[30000000];

        public string Part1()
        {
            string result = string.Empty;

            Init();

            while(recipeCounter < input + 10)
            {
                CombineAndCreate();

                elf1Recipe = PickNext(elf1Recipe);
                elf2Recipe = PickNext(elf2Recipe);
            }

            for (int i = input; i < recipeCounter -1; i++)
            {             
                result += recipes[i].ToString();
            }

            return result;
        }

        public int Part2()
        {
            string result = string.Empty;

            Init();

            while (true)
            {
                CombineAndCreate();

                elf1Recipe = PickNext(elf1Recipe);
                elf2Recipe = PickNext(elf2Recipe);


                if (recipeCounter > input.ToString().Length)
                {
                    if (CheckForSeq())
                    {                       
                        return recipeCounter - input.ToString().Length;
                    }                   
                }
            }         
        }

        private bool CheckForSeq()
        {
            string strInput = input.ToString();
            int index = recipeCounter - strInput.Length;
            int strIndex = 0;

            while (index < recipeCounter)
            {
                if (!(recipes[index].ToString()[0] == strInput[strIndex]))
                {
                    return false;
                }

                index++;
                strIndex++;
            }

            return true;
        }

        private void Init()
        {
            recipes[0] = 3;
            recipes[1] = 7;
            elf1Recipe = 0;
            elf2Recipe = 1;
            recipeCounter = 1;
        }

        private void CombineAndCreate()
        {
            string recipeSum = (recipes[elf1Recipe] + recipes[elf2Recipe]).ToString();

            foreach (char c in recipeSum)
            {
                recipeCounter++;
                recipes[recipeCounter] = Convert.ToInt32(char.GetNumericValue(c));                    
            }
        }

        private int PickNext(int index)
        {           
            return (index + 1 + recipes[index]) % (recipeCounter + 1);            
        }
    }
}
