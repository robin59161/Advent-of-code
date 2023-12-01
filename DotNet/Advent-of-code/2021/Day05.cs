using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day05
    {
        public static int Part1(string input)
        {
            string[] splittedInput = input.Split(Environment.NewLine);
            /*Grid grid = new Grid();
            foreach (string inputCoord in splittedInput)
            {
                int[][] coords = inputCoord.Split(" -> ").Select(x => x.Split(",").Select(y => Convert.ToInt32(y)).ToArray()).ToArray();
                int x1 = coords[0][0];
                int y1 = coords[0][1];
                int x2 = coords[1][0];
                int y2 = coords[1][1];
                if (x1 == x2 || y1 == y2)
                {
                    for (int i = Math.Min(x1, x2) ; i <= Math.Max(x1, x2); i++)
                    {
                        for(int j = Math.Min(y1, y2); j <= Math.Max(y1, y2); j++)
                        {
                            if (grid.cells.Count(cell => cell.x == i && cell.y == j) != 0)
                            {
                                grid.cells.First(cell => cell.x == i && cell.y == j).values++;
                            }
                            else
                            {
                                grid.cells.Add(new Cell { x = i, y = j, values = 1 });
                            }
                        }
                    }
                }
            }
            return grid.cells.Where(cell => cell.values >= 2).Count();*/
            return 0; 
        }

        internal class Grid
        {
            public List<Cell> cells { get; set; }

            public Grid()
            {
                cells = new List<Cell>();
            }
        }

        internal class Cell
        {
            public int x { get; set; }
            public int y { get; set; }
            public int values { get; set; }
        }
    }
}
