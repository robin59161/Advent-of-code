using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
namespace System.Runtime.CompilerServices
{
    public class IsExternalInit { }
}
namespace Advent_of_code._2020
{
    public static class Day17
    {
        private record Position(int X, int Y, int Z, int W);
        private record Cycle(ImmutableHashSet<Position> Actives);

        public static long Part1(string input)
        {
            var state = Init(input);

            for (var cycle = 0; cycle < 6; ++cycle)
            {
                state = Execute(state, MatriceVoisin3D); ;
            }

            return state.Actives.Count;
        }

        private static Cycle Init(string inputs)
        {
            var lines = inputs.Split(Environment.NewLine);

            var actives = new HashSet<Position>();


            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[0].Length; x++)
                {
                    switch (lines[y][x])
                    {
                        case '#':
                            actives.Add(new Position(x, y, 0, 0 ));
                            break;
                    }
                }
            }

            return new Cycle(actives.ToImmutableHashSet());
        }


        private static Cycle Execute(Cycle state, ImmutableList<Position> matriceVoisin)
        {
            IEnumerable<Position> GetVoisins(Position center) =>
                matriceVoisin
                    .Select(offset => new Position(
                        center.X + offset.X,
                        center.Y + offset.Y,
                        center.Z + offset.Z,
                        center.W + offset.W));

            var actives = new HashSet<Position>();

            foreach (var position in state.Actives)
            {
                var activeNeighbors = GetVoisins(position).ToList();
                var active = activeNeighbors.Count(neighborPosition => state.Actives.Contains(neighborPosition));

                if (active == 2 || active == 3)
                    actives.Add(position);
            }

            foreach (var position in state.Actives)
            {
                var inactiveNeighbors = GetVoisins(position)
                    .Where(neighborPosition => !state.Actives.Contains(neighborPosition))
                    .Distinct().ToList();

                foreach (var inactiveNeighbor in inactiveNeighbors)
                {
                    var activeNeighbors = GetVoisins(inactiveNeighbor)
                        .Count(neighborPosition => state.Actives.Contains(neighborPosition));

                    if (activeNeighbors == 3)
                        actives.Add(inactiveNeighbor);
                }
            }

            return new Cycle(actives.ToImmutableHashSet());
        }

        private static readonly ImmutableList<Position> MatriceVoisin3D = Create3DVoisins();
        private static ImmutableList<Position> Create3DVoisins(int w = 0, bool includeCenter = false)
        {
            var positions = new List<Position>
            {
                new(-1, -1, -1, w), new(+0, -1, -1, w), new(+1, -1, -1, w),
                new(-1, +0, -1, w), new(+0, +0, -1, w), new(+1, +0, -1, w),
                new(-1, +1, -1, w), new(+0, +1, -1, w), new(+1, +1, -1, w),

                new(-1, -1, +0, w), new(+0, -1, +0, w), new(+1, -1, +0, w),
                new(-1, +0, +0, w),new(+1, +0, +0, w),
                new(-1, +1, +0, w), new(+0, +1, +0, w), new(+1, +1, +0, w),

                new(-1, -1, +1, w), new(+0, -1, +1, w), new(+1, -1, +1, w),
                new(-1, +0, +1, w), new(+0, +0, +1, w), new(+1, +0, +1, w),
                new(-1, +1, +1, w), new(+0, +1, +1, w), new(+1, +1, +1, w),

            };

            if (includeCenter)
                positions.Add(new Position(0, 0, 0, w));

            return positions.ToImmutableList();
        }

        private static ImmutableList<Position> Create4DVoisins()
        {
            var positions = new List<Position>();

            positions.AddRange(Create3DVoisins(-1, true));
            positions.AddRange(Create3DVoisins(0, false));
            positions.AddRange(Create3DVoisins(1, true));

            return positions.ToImmutableList();
        }

        static readonly ImmutableList<Position> MatriceVoisin4D = Create4DVoisins();


        public static long Part2(string input)
        {
            var state = Init(input);
            for (var cycle = 0; cycle < 6; ++cycle)
            {
                state = Execute(state, MatriceVoisin4D);
            }

            return state.Actives.Count();
        }
    }
}
