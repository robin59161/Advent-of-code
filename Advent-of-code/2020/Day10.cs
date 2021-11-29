using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2020
{
    public static class Day10
    {
        public static int Part1(string input)
        {
            Dictionary<int, int> diff = new Dictionary<int, int>();
            diff.Add(1, 0);
            diff.Add(3, 1);
            int[] inputs = input.Split(Environment.NewLine).Select(x => Convert.ToInt32(x)).Append(0).OrderBy(x => x).ToArray();
            for(int i = 0; i < inputs.Length - 1; i++)
            {
                diff[inputs[i + 1] - inputs[i]] += 1;
            }
            return diff[1] * diff[3];
        }

        public static long Part2(string input)
        {
            List<string> chemins = new List<string>();
            var inputs = input.Split(Environment.NewLine).Select(x => Convert.ToInt32(x));
            var newData = Enumerable.Empty<int>()
                .Append(0)
                .Concat(inputs)
                .Append(inputs.Max() + 3)
                .OrderBy(x => x)
                .ToArray();

            var arrangements = Solver(newData);
            return arrangements;
        }
        private static long Solver(Span<int> adapters, Dictionary<int, long> visited = null)
        {
            visited ??= new Dictionary<int, long>();
            if (adapters.Length <= 1)
            {
                return 1;
            }

            if (visited.TryGetValue(adapters.Length, out var res))
            {
                return res;
            }

            long arrangements = 0;
            for (int j = 1; j <= 3; j++)
            {
                if (j >= adapters.Length)
                {
                    break;
                }

                if (adapters[j] - adapters[0] > 3)
                {
                    break;
                }

                arrangements += Solver(adapters[j..], visited);
            }
            visited[adapters.Length] = arrangements;
            return arrangements;
        }
    }
}
