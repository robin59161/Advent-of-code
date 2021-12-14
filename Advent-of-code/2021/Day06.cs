using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Numerics;

namespace Advent_of_code._2021
{
    public static class Day06
    {
        public static BigInteger Part1(string input)
        {
            long[] numbers = input.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

            BigInteger[] fishdata = new BigInteger[9];

            foreach (int n in numbers)
                fishdata[n]++;

            int total_days = 80;

            for (int n = 0; n < total_days; n++)
            {
                BigInteger newfish = fishdata[0];
                fishdata[0] = fishdata[1];
                fishdata[1] = fishdata[2];
                fishdata[2] = fishdata[3];
                fishdata[3] = fishdata[4];
                fishdata[4] = fishdata[5];
                fishdata[5] = fishdata[6];
                fishdata[6] = fishdata[7];
                fishdata[7] = fishdata[8];
                fishdata[8] = newfish;
                fishdata[6] += newfish;
            }

            BigInteger total = 0;

            foreach (BigInteger n in fishdata)
                total += n;

            return total;
        }

        public static BigInteger Part2(string input)
        {
            long[] numbers = input.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

            BigInteger[] fishdata = new BigInteger[9];

            foreach (int n in numbers)
                fishdata[n]++;

            int total_days = 256;

            for (int n = 0; n < total_days; n++)
            {
                BigInteger newfish = fishdata[0];
                fishdata[0] = fishdata[1];
                fishdata[1] = fishdata[2];
                fishdata[2] = fishdata[3];
                fishdata[3] = fishdata[4];
                fishdata[4] = fishdata[5];
                fishdata[5] = fishdata[6];
                fishdata[6] = fishdata[7];
                fishdata[7] = fishdata[8];
                fishdata[8] = newfish;
                fishdata[6] += newfish;
            }

            BigInteger total = 0;

            foreach (BigInteger n in fishdata)
                total += n;

            return total;
        }
    }
}
