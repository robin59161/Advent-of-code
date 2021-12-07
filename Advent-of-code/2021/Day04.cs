using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day04
    {
        public static int Part1(string input)
        {
            string[] splittedInput = input.Split(Environment.NewLine+Environment.NewLine);
            int[] numbersCall = splittedInput[0].Split(",").Select(x => Convert.ToInt32(x)).ToArray();
            List<Grid> grids = new List<Grid>();
            for (int i = 1; i < splittedInput.Length; i++){
                Grid grid = new Grid();
                string[] lines = splittedInput[i].Split(Environment.NewLine);
                for (int x = 0; x < lines.Length; x++)
                {
                    string[] line = lines[x].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    for(int y = 0; y < line.Length; y++)
                    {
                        grid.cells.Add(new Cell { x = x, y = y, value = Convert.ToInt32(line[y]), marked = false });
                    }
                }
                grids.Add(grid);
            }

            int j = 0;
            Grid Finalgrid = new Grid();
            while(j != numbersCall.Length && !Stop(grids, out Finalgrid))
            {
                int call = numbersCall[j];
                foreach(Grid gr in grids)
                {
                    if(gr.cells.Where(cell => cell.value == call).Count() > 0)
                    {
                        foreach(Cell cell in gr.cells.Where(cell => cell.value == call))
                        {
                            cell.marked = true;
                        }
                    }
                }
                j++;
            }

            return Finalgrid.cells.Where(x => !x.marked).Select(cell => cell.value).Sum() * numbersCall[j-1];
        }

        internal static bool Stop(List<Grid> grids, out Grid gr)
        {
            foreach(Grid grid in grids)
            {
                for(int i=0; i < grid.cells.Where(x => x.x == 0).Count(); i++)
                {
                    if (grid.cells.Where(cell => cell.x == i).All(cell => cell.marked))
                    {
                        gr = grid;
                        return true;
                    }
                    if (grid.cells.Where(cell => cell.y == i).All(cell => cell.marked))
                    {
                        gr = grid;
                        return true;
                    }
                }   
            }
            gr = new Grid();
            return false;
        }

        public static int Part2(string input)
        {
            string[] splittedInput = input.Split(Environment.NewLine + Environment.NewLine);
            int[] numbersCall = splittedInput[0].Split(",").Select(x => Convert.ToInt32(x)).ToArray();
            List<Grid> grids = new List<Grid>();
            for (int i = 1; i < splittedInput.Length; i++)
            {
                Grid grid = new Grid { finish = false };
                string[] lines = splittedInput[i].Split(Environment.NewLine);
                for (int x = 0; x < lines.Length; x++)
                {
                    string[] line = lines[x].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    for (int y = 0; y < line.Length; y++)
                    {
                        grid.cells.Add(new Cell { x = x, y = y, value = Convert.ToInt32(line[y]), marked = false });
                    }
                }
                grids.Add(grid);
            }

            int j = 0;
            Grid LastFinish = new Grid();
            while (j < numbersCall.Length && grids.Where(grid => !grid.finish).Count() > 0)
            {
                int call = numbersCall[j];
                foreach (Grid gr in grids.Where(grid => !grid.finish))
                {
                    if (gr.cells.Where(cell => cell.value == call).Count() > 0)
                    {
                        foreach (Cell cell in gr.cells.Where(cell => cell.value == call))
                        {
                            cell.marked = true;
                        }
                    }
                    for (int i = 0; i < gr.cells.Where(x => x.x == 0).Count(); i++)
                    {
                        if (gr.cells.Where(cell => cell.x == i).All(cell => cell.marked))
                        {
                            gr.finish = true;
                            LastFinish = gr;
                        }
                        if (gr.cells.Where(cell => cell.y == i).All(cell => cell.marked))
                        {
                            gr.finish = true;
                            LastFinish = gr;
                        }
                    }
                }
                j++;
            }
            return LastFinish.cells.Where(cell => !cell.marked).Select(x => x.value).Sum() * numbersCall[j - 1];
        }

        internal class Grid
        { 
            public List<Cell> cells { get; set; }

            public bool finish { get; set; }

            public Grid()
            {
                cells = new List<Cell>();
            }
        }

        internal class Cell
        {
            public int x { get; set; }
            public int y { get; set; }
            public int value { get; set; }
            public bool marked { get; set; }
        }
    }
}

