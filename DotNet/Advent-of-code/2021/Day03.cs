using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_of_code._2021
{
	public static class Day03
	{
		public static int Part1(string input)
		{
			string[] binaries = input.Split(Environment.NewLine);
			List<char> digits = new List<char>();
			for (int i = 0; i < binaries[0].Length; i++)
			{
				int x = 0;
				for (int j = 0; j < binaries.Length; j++)
				{
					x = (binaries[j][i] == '0') ? x-1 : x+1;
				}
				digits.Add((x < 0) ? '0' : '1');
			}
			int gamma = Convert.ToInt32(string.Join("", digits), 2);
			int epsilon = Convert.ToInt32(string.Join("", digits.Select(x => x == '0' ? '1' : '0')), 2);
			return gamma * epsilon;
		}


		public static int Part2(string input)
        {
			string[] binaries = input.Split(Environment.NewLine);
			int co2 = CO2(binaries);
			int oxygene = Oxygene(binaries);
			return co2 * oxygene;
        }

		public static int CO2(string[] input, int position = 0)
        {
			if(input.Length == 1)
            {
				return Convert.ToInt32(input[0], 2);
            }
			else
            {
				int x = 0;
				for(int i = 0; i<input.Length; i++)
                {
					x = (input[i][position] == '0') ? x + 1 : x - 1;
                }
                if (x > 0)
                {
					return CO2(input.Where(x => x[position] == '1').ToArray(), position + 1);
                }
                else
                {
					return CO2(input.Where(x => x[position] == '0').ToArray(), position + 1);
                }
			}
        }

		public static int Oxygene(string[] input, int position = 0)
		{
			if (input.Length == 1)
			{
				return Convert.ToInt32(input[0], 2);
			}
			else
			{
				int x = 0;
				for (int i = 0; i < input.Length; i++)
				{
					x = (input[i][position] == '1') ? x + 1 : x - 1;
				}
				if (x >= 0)
				{
					return Oxygene(input.Where(x => x[position] == '1').ToArray(), position + 1);
				}
				else
				{
					return Oxygene(input.Where(x => x[position] == '0').ToArray(), position + 1);
				}
			}
		}
	}
}
