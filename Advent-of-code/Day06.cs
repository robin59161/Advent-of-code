using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code
{
    public static class Day06
    {
        public static int Part1(string input)
        {
            List<string> Groups = input.Split("\r\n\r\n").ToList();
            int[] values = Groups.Select(x =>
            {
                return String.Join("", input.Split(Environment.NewLine)).ToCharArray().Distinct().Count();
            }).ToArray();
            return values.Sum();
        }

        public static int Part2(string input)
        {
            List<string> Groups = input.Split("\r\n\r\n").ToList();
            int[] values = Groups.Select(x =>
            {
                List<char[]> answers =  x.Split(Environment.NewLine).Select(x => x.ToCharArray()).ToList();
                char[] res = answers.Aggregate((a, b) => a.Intersect(b).ToArray());
                return res.Count();
            }).ToArray();
            return values.Sum();
        }
    }
}
