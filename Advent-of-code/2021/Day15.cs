using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day15
    {

        public static int Part1(string input)
        {
            List<string> splittedInput = input.Split(Environment.NewLine).ToList();

            Grid grid = new Grid();

            var width = splittedInput[0].Length;
            var height = splittedInput.Count();
            foreach (var x in Enumerable.Range(0, height))
            {
                foreach (var y in Enumerable.Range(0, width))
                {
                    string value = splittedInput[x][y].ToString();
                    grid.cells.Add((x, y), new Cell { Risk = int.MaxValue, visited = false, value = int.Parse(value) });
                }
            }

            int bestPath = Dijsktra(grid);
            return bestPath;
        }

        public static int Part2(string input)
        {
            List<string> splittedInput = input.Split(Environment.NewLine).ToList();
            
            var width = splittedInput[0].Length * 5;
            var height = splittedInput.Count() * 5;
            var chunkWidth = splittedInput[0].Length;
            var chunkHeight = splittedInput.Count();

            Grid grid = new Grid();

            foreach (var x in Enumerable.Range(0, height))
            {
                foreach (var y in Enumerable.Range(0, width))
                {
                    int value = int.Parse(splittedInput[(x % chunkWidth)][(y % chunkHeight)].ToString()) + x / chunkWidth + y / chunkHeight;
                    while (value > 9) value -= 9;

                    grid.cells.Add((x, y), new Cell { Risk = int.MaxValue, visited = false, value = value });
                }
            }

            int bestPath = Dijsktra(grid);
            return bestPath;
        }

        public static int Dijsktra(Grid grid)
        {
            grid.cells.TryGetValue((0,0), out Cell Cell);
            Cell.value = 0;
            Cell.Risk = 0;

            (int targetX, int TargetY) target = (grid.cells.Max(cell => cell.Key.Item1), grid.cells.Max(cell => cell.Key.Item2));
            (int cellX, int cellY) cell = (0, 0);

            Dictionary<(int, int), Cell> allPath = new Dictionary<(int, int), Cell>();
            allPath.Add((0,0), Cell);

            while (target != cell)
            {
                Cell.visited = true;
                Dictionary<(int, int), Cell> neighboors = grid.getNeighboor(cell);
                foreach(KeyValuePair<(int x, int y), Cell> neighboor in neighboors)
                {
                    int nvValue = allPath.GetValueOrDefault(cell).Risk + neighboor.Value.value;
                    if(allPath.ContainsKey(neighboor.Key))
                    {
                        int prevValue = allPath[neighboor.Key].Risk;
                        if(nvValue < prevValue)
                        {
                            allPath.Remove(neighboor.Key);
                            neighboor.Value.Risk = nvValue;
                            allPath.Add(neighboor.Key, neighboor.Value);
                        }
                    }
                    else
                    {
                        neighboor.Value.Risk = nvValue;
                        allPath.Add(neighboor.Key, neighboor.Value);
                    }
                }
                allPath.Remove(cell);
                cell = allPath.Keys.First(path => allPath.GetValueOrDefault(path).Risk == allPath.Values.Min(cell => cell.Risk));
                Cell = allPath[cell];
            }
            return Cell.Risk;
        }

        public class Grid
        {
            public Grid()
            {
                cells = new Dictionary<(int, int), Cell>();
            }

            public Dictionary<(int, int), Cell> cells { get; set; }

            private List<(int, int)> adjacent = new List<(int, int)> { (-1, 0), (1, 0), (0, -1), (0, 1) };

            public Dictionary<(int, int),Cell> getNeighboor((int, int) xy)
            {
                Dictionary<(int, int), Cell> cells = new Dictionary<(int, int), Cell>();
                foreach((int i, int j) in adjacent)
                {
                    if(this.cells.TryGetValue((xy.Item1 + i, xy.Item2 + j), out Cell Cell))
                    {
                        if (!Cell.visited)
                        {
                            cells.Add((xy.Item1 + i, xy.Item2 + j), Cell);
                        }
                    }
                }
                return cells;
            }
        }

        public class Cell
        {
            public int value { get; set; }
            public bool visited { get; set; }
            public int Risk { get; set; }
        }
    }
}
