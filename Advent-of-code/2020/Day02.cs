using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code._2020
{
    public static class Day02
    {
        public static int Part1(string inputs)
        {
            Regex extract = new Regex(@"(\d+)-(\d+) ([a-z]): (.*)");
            int valid = 0;
            foreach (string input in inputs.Split(Environment.NewLine))
            {
                Match match = extract.Matches(input)[0];
                int min = Int32.Parse(match.Groups[1].Value);
                int max = Int32.Parse(match.Groups[2].Value);
                char c = match.Groups[3].Value[0];
                string pass = match.Groups[4].Value;


                int p = pass.IndexOf(c);
                int count = 0;
                while (p != -1)
                {
                    count++;
                    p = pass.IndexOf(c, p + 1);
                }


                if (count <= max && count >= min)
                {
                    valid++;
                }
            }
            return valid;
        }

        public static int Part2(string inputs)
        {
            Regex extract = new Regex(@"(\d+)-(\d+) ([a-z]): (.*)");
            int valid = 0;
            foreach (string input in inputs.Split(Environment.NewLine))
            {
                Match match = extract.Matches(input)[0];
                int min = Int32.Parse(match.Groups[1].Value)-1;
                int max = Int32.Parse(match.Groups[2].Value)-1;
                char c = match.Groups[3].Value[0];
                string pass = match.Groups[4].Value;
                if ((pass[min] == c && pass[max] != c) || (pass[min] != c && pass[max] == c))
                {
                    valid++;
                }
            }
            return valid;
        }
    }
}
