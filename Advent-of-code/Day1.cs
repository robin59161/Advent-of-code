using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_code
{
    public static class Day1
    {
        public static long Part1(int[] inputs)
        {
            Array.Sort(inputs);
            int f = 0; int l = inputs.Length - 1;
            while (f < l)
            {
                if (inputs[f] + inputs[l] > 2020)
                    l--;
                else if (inputs[f] + inputs[l] < 2020)
                    f++;
                else
                {
                    Console.WriteLine(String.Format("{0} + {1} = 2020", inputs[f], inputs[l]));
                    return inputs[f] * inputs[l];
                }
            }
            throw new Exception("No Result");

        }

        public static long Part2(int[] inputs)
        {
            Array.Sort(inputs);
            for (int i = 0; i < inputs.Length - 2; i++)
            {
                int f = i + 1; int l = inputs.Length - 1;
                while (f < l)
                {
                    if (inputs[i] + inputs[f] + inputs[l] > 2020)
                        l--;
                    else if (inputs[i] + inputs[f] + inputs[l] < 2020)
                        f++;
                    else
                    {
                        Console.WriteLine(String.Format("{0} + {1} + {2} = 2020", inputs[i], inputs[f], inputs[l]));
                        return inputs[i] * inputs[f] * inputs[l];
                    }
                }
            }
            throw new Exception("No Result");
        }
    }
}
