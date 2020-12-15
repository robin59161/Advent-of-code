using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code
{
    public static class Day14
    {
        public static long Part1(string input)
        {
            Dictionary<int,long> memory = new Dictionary<int, long>();
            string CurrentMask = "";
            foreach(string line in input.Split(Environment.NewLine))
            {
                if (line.StartsWith("mask"))
                {
                    Regex r = new Regex(@"mask = (.*)");
                    CurrentMask = r.Match(line).Groups[1].Value;
                }
                else
                {
                    Regex r = new Regex(@"mem\[(\d+)\] = (\d+)");
                    Match m = r.Match(line);
                    int adress = Convert.ToInt32(m.Groups[1].Value);
                    long value = Convert.ToInt64(m.Groups[2].Value);
                    memory[adress] = ApplyMask(CurrentMask, value);
                }
            }
            return memory.Sum(x => x.Value);
        }

        public static long ApplyMask(string mask,long value)
        {
            foreach ((char value,int index) c in mask.Reverse().WithIndex())
            {
                if(c.value != 'X')
                {
                    if (c.value == '1')
                        value |= (long)1 << c.index;
                    else
                        value &= ~((long)1 << c.index);
                }
            }
            return value;
        }

        public static long Part2(string input)
        {
            Dictionary<long, long> memory = new Dictionary<long, long>();
            string CurrentMask = "";
            foreach (string line in input.Split(Environment.NewLine))
            {
                if (line.StartsWith("mask"))
                {
                    Regex r = new Regex(@"mask = (.*)");
                    CurrentMask = r.Match(line).Groups[1].Value;
                }
                else
                {
                    Regex r = new Regex(@"mem\[(\d+)\] = (\d+)");
                    Match m = r.Match(line);
                    int adress = Convert.ToInt32(m.Groups[1].Value);
                    int value = Convert.ToInt32(m.Groups[2].Value);
                    long[] values = ApplyMask2(CurrentMask, adress);
                    foreach (long mem in values)
                    {
                        memory[mem] = value;
                        adress++;
                    }
                }
            }
            return memory.Sum(x => x.Value);
        }

        public static long[] ApplyMask2(string mask, int value)
        {
            List<long> values = new List<long>() { value };
            foreach ((char value, int index) c in mask.Reverse().WithIndex())
            {
                if (c.value == '0') continue;
                if (c.value == '1')
                {
                    for (int n = 0; n < values.Count; n++)
                    {
                        values[n] |= (long)1 << c.index;
                    }
                }
                else
                {
                    List<long> tempValues = new List<long>();
                    for (int n = 0; n < values.Count; n++)
                    {
                        tempValues.Add(values[n] | ((long)1 << c.index));
                        tempValues.Add(values[n] &~ ((long)1 << c.index));
                    }
                    values = tempValues;
                }
            }
            return values.ToArray();
        }

    }
}
