using System;
using System.Text.RegularExpressions;

namespace advent_of_code_2018.Days.Day03
{
    public class ClaimFactory
    {
        const string InputMatcher = @"(?<=\#)(.*?)(?=\@)[@]\s([0-9]{1,3})[,]([0-9]{1,3})[:]\s([0-9]{1,2})[x]([0-9]{1,2})";
        private Regex regex = new Regex(InputMatcher);

        public Claim Create(string input)
        {
            Claim claim;
            Match match = regex.Match(input);

            if (match.Success)
            {
                claim = new Claim()
                {
                    ClaimId = Convert.ToInt32(match.Groups[1].Value),
                    LeftOffset = Convert.ToInt32(match.Groups[2].Value),
                    TopOffset = Convert.ToInt32(match.Groups[3].Value),
                    Width = Convert.ToInt32(match.Groups[4].Value),
                    Height = Convert.ToInt32(match.Groups[5].Value)
                };
            }
            else
            {
                throw new Exception("Nope");
            }      

            return claim;
        }
    }
}
