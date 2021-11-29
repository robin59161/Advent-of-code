using System;
using System.Collections.Generic;
using System.Text;

namespace Advent_of_code._2020
{
    public static class Day12
    {
        public static int Part1(string input)
        {
            char DirectionFacing = 'E';
            (int x, int y) position = (0,0);
            (int x, int y) Process((char instruction, int value) i) => i.instruction
             switch
            {
                'N' => (position.x, position.y + i.value),
                'S' => (position.x, position.y - i.value),
                'E' => (position.x+i.value, position.y),
                'W' => (position.x - i.value, position.y),
                'F' => Process((DirectionFacing,i.value)),
                'R' => Turn(i.instruction,i.value),
                'L' => Turn(i.instruction, i.value),
            };
            (int x,int y) Turn(char direction, int value)
            {
                char[] directions = new char[] { 'N', 'E', 'S', 'W' };
                int current = Array.IndexOf(directions, DirectionFacing);
                int turn = value / 90;
                if (direction == 'R')
                    DirectionFacing = directions[(current+4 + turn) % 4];
                else
                    DirectionFacing = directions[(current+4 - turn) % 4];
                return (position.x, position.y);
            }

            foreach (string s in input.Split(Environment.NewLine))
            {
                char instruction = s[0];
                int amount = Convert.ToInt32(new string(s[1..s.Length]));
                position = Process((instruction, amount));
            }
            return Math.Abs(position.x) + Math.Abs(position.y);
        }

        public static int Part2(string input)
        {
            char DirectionFacing = 'E';
            (int x, int y) position = (0, 0);
            (int x, int y) waypointPosition = (10, 1);
            (int x, int y) Process((char instruction, int value) i) => i.instruction
             switch
            {
                'N' => (waypointPosition.x, waypointPosition.y + i.value),
                'S' => (waypointPosition.x, waypointPosition.y - i.value),
                'E' => (waypointPosition.x + i.value, waypointPosition.y),
                'W' => (waypointPosition.x - i.value, waypointPosition.y),
                'F' => (position.x + waypointPosition.x*i.value, position.y + waypointPosition.y * i.value),
                'R' => Turn(i.instruction, i.value),
                'L' => Process(('R', 360-i.value)),
            };
            (int x, int y) Turn(char direction, int value)
            {
                var cos = Math.Round(Math.Cos(value * (Math.PI / 180.0)));
                var sin = Math.Round(Math.Sin(value * (Math.PI / 180.0)));
                var yprim = (waypointPosition.y * cos) - (waypointPosition.x * sin);
                var xprim = (waypointPosition.y * sin) + (waypointPosition.x * cos);
                return (Convert.ToInt32(xprim), Convert.ToInt32(yprim));
            }

            foreach (string s in input.Split(Environment.NewLine))
            {
                char instruction = s[0];
                int amount = Convert.ToInt32(new string(s[1..s.Length]));
                if (instruction == 'F')
                    position = Process((instruction, amount));
                else
                    waypointPosition = Process((instruction, amount));
            }
            return Math.Abs(position.x) + Math.Abs(position.y);
        }
    }
}
