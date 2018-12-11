using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2018.Days.Day09
{
    public class Solution
    {
        private const int Players = 423;

        private Marble currentMarble = null;
        private long nextAvailableNumber = 1;

        private List<Marble> marbles = new List<Marble>();
        private Dictionary<int, long> playerScores = new Dictionary<int, long>();

        public long Calculate(int lastMarbleValue)
        {
            InitScores();

            AddFirstMarble();

            while (currentMarble.Number < lastMarbleValue && nextAvailableNumber < lastMarbleValue)
            {
                for (int i = 1; i <= Players; i++)
                {
                    Marble newMarble = new Marble
                    {
                        Number = nextAvailableNumber
                    };

                    InsertMarble(newMarble, i);

                    if (newMarble.Number == lastMarbleValue)
                    {
                        break;
                    }
                }
            }      
                 
            return playerScores.Values.Max();
        }

        private void InitScores()
        {
            for (int i = 1; i <= Players; i++)
            {
                playerScores.Add(i, 0);
            }
        }

        private void InsertMarble(Marble marble, int player)
        {
            if (marble.Number % 23 == 0)
            {
                playerScores[player] += marble.Number;
          
                playerScores[player] +=
                    currentMarble.PrevMarble.PrevMarble.PrevMarble.PrevMarble.PrevMarble.PrevMarble.PrevMarble.Number;

                Marble marble6 = currentMarble.PrevMarble.PrevMarble.PrevMarble.PrevMarble.PrevMarble.PrevMarble;
                Marble marble8 = currentMarble.PrevMarble.PrevMarble.PrevMarble.PrevMarble.PrevMarble.PrevMarble.PrevMarble.PrevMarble;

                marble8.NextMarble = marble6;
                marble6.PrevMarble = marble8;

                currentMarble = marble6;
            }
            else
            {
                marble.NextMarble = currentMarble.NextMarble.NextMarble;
                marble.PrevMarble = currentMarble.NextMarble;
                
                currentMarble.NextMarble.NextMarble.PrevMarble = marble;
                currentMarble.NextMarble.NextMarble = marble;

                currentMarble = marble;
            }

            nextAvailableNumber = nextAvailableNumber + 1;
        }

        private void AddFirstMarble()
        {
            Marble marble = new Marble { Number = 0 };
            marble.NextMarble = marble;
            marble.PrevMarble = null;
            marbles.Add(marble);
            currentMarble = marble;
        }
    }

    public class Marble
    {
        public long Number { get; set; }

        public Marble PrevMarble { get; set; }

        public Marble NextMarble { get; set; }
    }
}