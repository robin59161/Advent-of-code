using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day07
    {
        public static int Part1(string input)
        {
            List<int> fuels = input.Split(",").Select(int.Parse).ToList();
            fuels.Sort();
            int median = fuels.ElementAt((fuels.Count() - 1) / 2);
            int sumFuels = 0;
            foreach(int fuel in fuels)
            {
                sumFuels += Math.Abs(fuel - median);
            }
            return sumFuels;
        }

        public static int Part2(string input)
        {
            var positions = input.Split(",").Select(x => int.Parse(x));
            int min = positions.Min(), max = positions.Max();
            var diffs = Enumerable.Range(min, max)
                .Select(x => positions.Select(p => Math.Abs(x - p)));

            var minFuel = diffs.Min(d => d.Sum());
            var crabFuel = (int)diffs.Min(d => d.Sum(x => x * new[] { 1, x }.Average()));

            return crabFuel;
        }
    }
}
