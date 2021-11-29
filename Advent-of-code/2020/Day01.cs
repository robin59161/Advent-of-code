using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2020
{
    public static class Day01
    {
        public static long Part1(string inputs)
        {
            int[] numbers = inputs.Split(Environment.NewLine).Select(x => Convert.ToInt32(x)).ToArray();
            Array.Sort(numbers);
            int f = 0; int l = numbers.Length - 1;
            while (f < l)
            {
                if (numbers[f] + numbers[l] > 2020)
                    l--;
                else if (numbers[f] + numbers[l] < 2020)
                    f++;
                else
                {
                    return numbers[f] * numbers[l];
                }
            }
            throw new Exception("No Result");

        }

        public static long Part2(string inputs)
        {
            int[] numbers = inputs.Split(Environment.NewLine).Select(x => Convert.ToInt32(x)).ToArray();
            Array.Sort(numbers);
            for (int i = 0; i < numbers.Length - 2; i++)
            {
                int f = i + 1; int l = numbers.Length - 1;
                while (f < l)
                {
                    if (numbers[i] + numbers[f] + numbers[l] > 2020)
                        l--;
                    else if (numbers[i] + numbers[f] + numbers[l] < 2020)
                        f++;
                    else
                    {
                        return numbers[i] * numbers[f] * numbers[l];
                    }
                }
            }
            throw new Exception("No Result");
        }
    }
}
