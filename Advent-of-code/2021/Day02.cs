using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day02
    {
        public static int Part1(string input)
        {
            string[] values = input.Split(Environment.NewLine);
            int x = 0;
            int y = 0;
            for (int i = 0; i < values.Length; i++)
            {
                string[] ligne = values[i].Split(" ");
                int value = Convert.ToInt32(ligne[1]);
                switch(ligne[0])
                {
                    case ("forward"):
                        x += value;
                        break;
                    case ("up"):
                        y -= value;
                        break;
                    case ("down"):
                        y += value;
                        break;
                }
            }
            return x*y;
        }

        public static int Part2(string input)
        {
            string[] values = input.Split(Environment.NewLine);
            int x = 0;
            int y = 0;
            int aim = 0;
            for (int i = 0; i < values.Length; i++)
            {
                string[] ligne = values[i].Split(" ");
                int value = Convert.ToInt32(ligne[1]);
                switch (ligne[0])
                {
                    case ("forward"):
                        y += value * aim;
                        x += value;
                        break;
                    case ("up"):
                        aim -= value;
                        break;
                    case ("down"):
                        aim += value;
                        break;
                }
            }
            return x * y;
        }
    }
}
