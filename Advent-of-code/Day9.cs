using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code
{
    public static class Day9
    {
        static readonly int preamble = 25;

        public static long Part1(string input)
        {
            Int64[] nums = input.Split(Environment.NewLine).Select(x => Convert.ToInt64(x)).ToArray();
            for (int i = preamble;i< nums.Length;i++)
            {
                bool found = false;
                for(int check = i - preamble; check < i-1; check++)
                {
                    if (nums[(check+1)..(i)].Contains(nums[i] - nums[check]))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    return nums[i];
            }
            throw new Exception("No result");
        }

        public static long Part2(string input)
        {
            long res = Part1(input);
            Int64[] nums = input.Split(Environment.NewLine).Select(x => Convert.ToInt64(x)).ToArray();
            int index = Array.IndexOf(nums, res);
            for (int i = 2;i < index; i++)
            {
                for(int j = 0; j < index-i;j++)
                {
                    if (nums[j..(j + i)].Sum() == res)
                    {
                        long[] range = nums[j..(j + i)];
                        return range.Max() + range.Min();
                    }
                }
            }
            throw new Exception("No result");

        }
    }
}
