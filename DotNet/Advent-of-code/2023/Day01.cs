using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2023
{
    public static class Day01
    {
        public static int Part1(string input)
        {
            string[] values = input.Split("\r\n");
            int res = 0;
            int i = 0;
            while (i < values.Length)
            {
                res += getCalibration(values[i]);
                i++;
            }
            return res;
        }

        static int getCalibration(string input)
        {
            int? first = null;
            int? second = null;

            int i = 0;
            while(i < input.Length)
            {
                if (int.TryParse(input[i].ToString(), out int result))
                {
                    if(!first.HasValue)
                    {
                        first = result;
                    }
                    else
                    {
                        second = result;
                    }
                }
                i++;
            }

            if(!second.HasValue)
            {
                second = first;
            }

            return Convert.ToInt32($"{first}{second}");
        }

        public static int Part2(string input)
        {
            string[] values = input.Split("\r\n");
            int res = 0;
            int i = 0;
            while (i < values.Length)
            {
                res += getCalibration2(values[i]);
                i++;
            }
            return res;
        }

        static Dictionary<string, int> keyValues = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };

        static int getCalibration2(string input)
        {
            int? first = null;
            int? second = null;
            string lastChar = "";
            int i = 0;
            while (i < input.Length)
            {
                lastChar += input[i].ToString();
                string key = keyValues.Keys.FirstOrDefault(lastChar.EndsWith);
                int result = 0;
                if (int.TryParse(input[i].ToString(), out result) || !string.IsNullOrEmpty(key))
                {
                    if (result == 0)
                        result = keyValues[key];
                    lastChar = "";
                    if (!first.HasValue)
                    {
                        first = result;
                    }
                    else
                    {
                        second = result;
                    }
                }
                i++;
            }

            if (!second.HasValue)
            {
                second = first;
            }

            return Convert.ToInt32($"{first}{second}");
        }
    }
}
