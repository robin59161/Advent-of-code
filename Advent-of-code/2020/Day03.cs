using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2020
{
    public static class Day03
    {
        public static int Part1(string input,int salopeX,int salopeY)
        {
            string[] lines = input.Split(Environment.NewLine);
            int posY = 0;
            int posX = 0;
            int count = 0;
            while(posY < lines.Length)
            {
                if (lines[posY][posX%lines[posY].Length] == '#')
                    count++;
                posX += salopeX;
                posY += salopeY;
            }
            return count;
        }

        public static int Part2(string input)
        {
            int[][] salopes =  { 
                                new int[]{ 3, 1 }, 
                                new int[]{ 1, 1 },
                                new int[]{ 5, 1 }, 
                                new int[]{ 7, 1 }, 
                                new int[]{ 1, 2 } 
            };
            int sum = 1;
            Action<int[]> action = delegate (int[] salope)
            {
                sum *= Part1(input, salope[0], salope[1]);
            };
            Array.ForEach(salopes, action);
            return sum;
        }
    }
}
