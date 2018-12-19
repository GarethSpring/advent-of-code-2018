using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace advent_of_code_2018.Days.Day13
{
    public class Solution
    {
        private char[,] map;
       
        private const string TurnSeq = "<v>^";

        private int mapDimension;

        bool crashed = false;


        private List<Cart> carts = new List<Cart>();

        public string Part1()
        {
            LoadInput();                    

            string crashCoords = string.Empty;

            while (!crashed)
            {
                crashCoords = Tick();                           
            }

            return crashCoords;
        }


        public string Part2()
        {
            LoadInput();

            while (true)
            {
                TickWithRemove();

                if (carts.Count(c => !c.IsCrashed) == 1)
                {
                    return $"{carts.First(c => !c.IsCrashed).X},{carts.First(c => !c.IsCrashed).Y}";                    
                }
            }
        }

        public string Tick()
        {
            var sortedCarts = carts.OrderBy(cart => cart.Y).ThenBy(cart => cart.X);

            foreach (Cart cart in sortedCarts)
            {
                // move in current direction
                switch (cart.Direction)
                {
                    case '^':
                        cart.Y--;
                        break;
                    case '>':
                        cart.X++;
                        break;
                    case '<':                        
                        cart.X--;
                        break;
                    case 'v':
                        cart.Y++;
                        break;
                }

                // Check for crash
                foreach (Cart c2 in carts)
                {
                    if (!cart.Equals(c2))
                    {
                        if (cart.X == c2.X && cart.Y == c2.Y)
                        {
                            crashed = true;
                            Debug.WriteLine($"{cart.X},{cart.Y}");

                            return $"{cart.X},{cart.Y}";
                        }

                    }
                }

                // Check for standard turn and change direction
                if (cart.Direction == '^' && map[cart.X,cart.Y] == '\\')
                {
                    cart.Direction = '<';
                }
                else if (cart.Direction == '^' && map[cart.X, cart.Y] == '/')
                {
                    cart.Direction = '>';
                }
                else if (cart.Direction == 'v' && map[cart.X, cart.Y] == '/')
                {
                    cart.Direction = '<';
                }
                else if (cart.Direction == 'v' && map[cart.X, cart.Y] == '\\')
                {
                    cart.Direction = '>';
                }
                else if (cart.Direction == '<' && map[cart.X, cart.Y] == '/')
                {
                    cart.Direction = 'v';
                }
                else if (cart.Direction == '<' && map[cart.X, cart.Y] == '\\')
                {
                    cart.Direction = '^';
                }
                else if (cart.Direction == '>' && map[cart.X, cart.Y] == '/')
                {
                    cart.Direction = '^';
                }
                else if (cart.Direction == '>' && map[cart.X, cart.Y] == '\\')
                {
                    cart.Direction = 'v';
                }

                // check for crossing

                if (map[cart.X, cart.Y] == '+')
                {
                    cart.Direction = GetDirectionAtCrossing(cart.Direction, cart.NextTurn);
                    if (cart.NextTurn == 'L')
                        cart.NextTurn = 'S';
                    else if (cart.NextTurn == 'S')
                        cart.NextTurn = 'R';
                    else if (cart.NextTurn == 'R')
                        cart.NextTurn = 'L';
                }
            }

            return string.Empty;
        }

        public string TickWithRemove()
        {

            string coords = string.Empty;

            var sortedCarts = carts.Where(x => !x.IsCrashed).OrderBy(cart => cart.Y).ThenBy(cart => cart.X);

            foreach (Cart cart in sortedCarts)
            {
                // move in current direction
                switch (cart.Direction)
                {
                    case '^':
                        cart.Y--;
                        break;
                    case '>':
                        cart.X++;
                        break;
                    case '<':
                        cart.X--;
                        break;
                    case 'v':
                        cart.Y++;
                        break;
                }

                // Check for crash
                foreach (Cart c2 in carts.Where(c => !c.IsCrashed))
                {
                    if (!cart.Equals(c2))
                    {
                        if (cart.X == c2.X && cart.Y == c2.Y)
                        {
                            crashed = true;
                            Debug.WriteLine($"{cart.X},{cart.Y}");

                            cart.IsCrashed = true;
                            c2.IsCrashed = true;

                            coords = $"{cart.X},{cart.Y}";
                        }

                    }
                }

                // Check for standard turn and change direction
                if (cart.Direction == '^' && map[cart.X, cart.Y] == '\\')
                {
                    cart.Direction = '<';
                }
                else if (cart.Direction == '^' && map[cart.X, cart.Y] == '/')
                {
                    cart.Direction = '>';
                }
                else if (cart.Direction == 'v' && map[cart.X, cart.Y] == '/')
                {
                    cart.Direction = '<';
                }
                else if (cart.Direction == 'v' && map[cart.X, cart.Y] == '\\')
                {
                    cart.Direction = '>';
                }
                else if (cart.Direction == '<' && map[cart.X, cart.Y] == '/')
                {
                    cart.Direction = 'v';
                }
                else if (cart.Direction == '<' && map[cart.X, cart.Y] == '\\')
                {
                    cart.Direction = '^';
                }
                else if (cart.Direction == '>' && map[cart.X, cart.Y] == '/')
                {
                    cart.Direction = '^';
                }
                else if (cart.Direction == '>' && map[cart.X, cart.Y] == '\\')
                {
                    cart.Direction = 'v';
                }

                // check for crossing

                if (map[cart.X, cart.Y] == '+')
                {
                    cart.Direction = GetDirectionAtCrossing(cart.Direction, cart.NextTurn);
                    if (cart.NextTurn == 'L')
                        cart.NextTurn = 'S';
                    else if (cart.NextTurn == 'S')
                        cart.NextTurn = 'R';
                    else if (cart.NextTurn == 'R')
                        cart.NextTurn = 'L';
                }
            }

            return coords;
        }

        private void Visualise()
        {
            for (int y = 0; y < mapDimension; y++)
            {
                for (int x = 0; x < mapDimension; x++)
                {
                    Cart cartHere = null;

                    foreach (Cart c in carts.Where(c => !c.IsCrashed))
                    {
                        if (c.X == x && c.Y == y)
                        {
                            cartHere = c;
                        }
                    }

                    Debug.Write(cartHere == null ? map[x, y] : cartHere.Direction);
                }

                Debug.WriteLine("");
            }
        }

        private char GetDirectionAtCrossing(char direction, char nextTurn)
        {
            if (direction == '^' && nextTurn == 'L')
            {
                return '<';
            }
            else if (direction == '<' && nextTurn == 'R')
            {
                return '^';
            }
            if (nextTurn == 'L')
            {
                return TurnSeq.Substring(TurnSeq.IndexOf(direction) + 1, 1)[0];
            }
            if (nextTurn == 'R')
            {
                return TurnSeq.Substring(TurnSeq.IndexOf(direction) - 1, 1)[0];
            }
            
            // Straight
            return direction;            
        }

        private void LoadInput()
        {
            int y = 0;

            File.ReadLines(@"days\day13\input\input.txt").ToList().ForEach(line =>
            {
                if (map == null)
                {
                    mapDimension = line.Length;
                    map = new char[line.Length,line.Length];
                }

                int curX = 0;
                while (curX < line.Length)
                {
                    map[curX, y] = line[curX];

                    // Detect carts                    
                    switch(map[curX, y])
                    {
                        case '<':
                        case '>':
                            carts.Add(new Cart { X = curX, Y = y, Direction = map[curX, y], NextTurn = 'L'});
                            map[curX, y] = '-';
                            break;

                        case '^':
                        case 'v':
                            carts.Add(new Cart { X = curX, Y = y, Direction = map[curX, y], NextTurn = 'L' });
                            map[curX, y] = '|';
                            break;
                    }
                    

                    curX++;
                }

                y++;
            });
        }
    }

    public class Cart
    {
        public int X { get; set; }

        public int Y { get; set; }

        public char Direction { get; set; }

        public char NextTurn { get; set; }

        public bool IsCrashed { get; set; }
    }
}
