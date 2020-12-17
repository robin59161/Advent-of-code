using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Advent_of_code
{
    public static class Day11
    {
        public static int FLOOR = 0;
        public static int FREE = 1;
        public static int OCCUPIED = 2;

        static int[,] matrix;
        static int[,] copyMatrix;

        private static IList<(int x, int y)> _neighbors = new List<(int x, int y)>
        {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1), (0, 1),
            (1, -1), (1, 0), (1, 1)
        };

        public static void ConstructMatrix(string input)
        {
            List<string> splitted = input.Split(Environment.NewLine).ToList();
            matrix = new int[splitted[0].Length, splitted.Count + 1];
            foreach (var (l, yIndex) in splitted.WithIndex())
            {
                foreach (var (ch, xIndex) in l.WithIndex())
                {
                    if (ch == 'L')
                    {
                        matrix[yIndex, xIndex] = FREE;
                    }
                    else
                    {
                        matrix[yIndex, xIndex] = FLOOR;
                    }
                }
            }
        }


        public static int Part1(string input)
        {
            ConstructMatrix(input);
            int res = Solve(GetDirectCovid);
            return res;
        }

        public static bool GetDirectCovid((int X, int Y) s)
        {
            int occuped = 0;
            for (int j = Math.Max(0, s.X - 1); j <= Math.Min(s.X + 1, matrix.GetLength(0) - 1); j++)
            {
                for (int i = Math.Max(0, s.Y - 1); i <= Math.Min(s.Y + 1, matrix.GetLength(1) - 1); i++)
                {
                    if (j != s.X || i != s.Y)
                    {
                        if (copyMatrix[j, i] == Day11.OCCUPIED)
                            occuped++;
                    }
                }
            }
            if ((matrix[s.X, s.Y] == Day11.OCCUPIED && occuped >= 4))
            {
                matrix[s.X, s.Y] = Day11.FREE;
                return true;
            }
            else if ((matrix[s.X, s.Y] == Day11.FREE && occuped == 0))
            {
                matrix[s.X, s.Y] = Day11.OCCUPIED;
                return true;
            }
            return false;
        }

        public static int Part2(string input)
        {
            ConstructMatrix(input);

            int res = Solve(GetFilleDeTaRegion);
            return res;
        }

        

        public static int Solve(Func<(int,int),bool> action)
        {
            bool stabilized = false;
            while (!stabilized)
            {
                copyMatrix = matrix.Clone() as int[,];
                bool changed = false;
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    for (int y = 0; y < matrix.GetLength(1); y++)
                    {
                        changed |= action((x, y));
                    }
                }
                //DrawState();
                stabilized = !changed;
            }
            return matrix.Cast<int>().Count(a => a == OCCUPIED);
        }

        public static bool GetFilleDeTaRegion((int X, int Y) s)
        {
            int occuped = 0;
            foreach(var dim in _neighbors)
            {
                var multiplier = 1;
                var currentSeat = 0;
                do
                {
                    var _x = (dim.x * multiplier) + s.X;
                    var _y = (dim.y * multiplier) + s.Y;
                    if (!ValidePos(_x, _y))
                    {
                        break;
                    }
                    currentSeat = copyMatrix[_x, _y];
                    multiplier++;
                } while (currentSeat == FLOOR);

                if(currentSeat == OCCUPIED) 
                    occuped++;
            }

            if ((matrix[s.X, s.Y] == Day11.OCCUPIED && occuped >= 5))
            {
                matrix[s.X, s.Y] = Day11.FREE;
                return true;
            }
            else if ((matrix[s.X, s.Y] == Day11.FREE && occuped == 0))
            {
                matrix[s.X, s.Y] = Day11.OCCUPIED;
                return true;
            }
            return false;
        }

        public static bool ValidePos(int x,int y)
        {
            return x >= 0 && x < matrix.GetLength(0)
                && y >= 0 && y < matrix.GetLength(1);
        }


        public static void DrawState()
        {
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    int s = matrix[x, y];
                    switch (s)
                    {
                        case 0:
                            Console.Write(".");
                            break;
                        case 1:
                            Console.Write("L");
                            break;
                        default:
                            Console.Write("#");
                            break;

                    }
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }

    public static class LinqExtension
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
           => self.Select((item, index) => (item, index));
    }
}
