using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day01
    {
        public static int Part1(string input)
        {
            string[] values = input.Split("\r\n");
            int res = 0;
            int i = 0;
            while (i < values.Length-1)
            {
                res = (Convert.ToInt32(values[i + 1]) > Convert.ToInt32(values[i])) ? res + 1 : res;
                i++;
            }
            return res;
        }

        public static int Part2(string input)
        {
            string[] values = input.Split("\r\n");
            int res = 0;
            int sum = 0;
            int sum2 = 0;
            int i = 0;
            while (i < values.Length-3)
            {
                sum = Convert.ToInt32(values[i + 1]) + Convert.ToInt32(values[i + 2]) + Convert.ToInt32(values[i]);
                sum2 = Convert.ToInt32(values[i + 1]) + Convert.ToInt32(values[i + 2]) + Convert.ToInt32(values[i + 3]);
                res = (sum2 > sum) ? res + 1 : res;
                i++;
            }
            return res;
        }

    }
}
