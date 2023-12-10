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
            while((!first.HasValue || !second.HasValue) && i < input.Length)
            {
                if (!first.HasValue && int.TryParse(input[i].ToString(), out int result))
                {
                    first = result;
                }
                if(!second.HasValue && int.TryParse(input[input.Length - (i + 1)].ToString(), out result))
                {
                    second = result;
                }
                i++;
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
            string[] lastChar = new string[2] { "", "" } ;
            int i = 0;
            while ((!first.HasValue || !second.HasValue) && i < input.Length)
            {
                lastChar[0] += input[i].ToString();
                lastChar[1] = input[input.Length - (i + 1)].ToString() + lastChar[1];
                string keyFirst = keyValues.Keys.FirstOrDefault(lastChar[0].EndsWith);
                string keySecond = keyValues.Keys.FirstOrDefault(lastChar[1].StartsWith);

                if (!first.HasValue && int.TryParse(input[i].ToString(), out int result))
                {
                    first = result;
                }
                else if(!first.HasValue && !string.IsNullOrEmpty(keyFirst))
                {
                    first = keyValues[keyFirst];
                }
                if (!second.HasValue && int.TryParse(input[input.Length - (i + 1)].ToString(), out result))
                {
                    second = result;
                }
                else if (!second.HasValue && !string.IsNullOrEmpty(keySecond))
                {
                    second = keyValues[keySecond];
                }
                i++;
            }

            return Convert.ToInt32($"{first}{second}");
        }
    }
}
