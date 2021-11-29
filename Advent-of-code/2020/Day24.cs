using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code._2020
{
    public static class Day24
    {
        static Hex origin = new Hex(0, 0);
        static Dictionary<Hex, bool> floor = new Dictionary<Hex, bool> { [origin] = false };

        static IEnumerable<List<Hex>> operations;
        public static int Part1(string input)
        {
            operations = Parse(input.Split(Environment.NewLine));
            foreach (var op in operations)
            {
                var current = op.Aggregate(origin, (curr, step) => curr + step);

                if (floor.TryGetValue(current, out var set))
                    floor[current] = !set;
                else
                    floor[current] = true;
            }

            return floor.Values.Count(c => c);
        }

        public static long Part2(string input)
        {
            foreach (var n in floor.Keys.SelectMany(c => c.Neighbors).ToList())
                if (!floor.ContainsKey(n))
                    floor[n] = false;

            for (var i = 0; i < 100; ++i)
            {
                var next = new Dictionary<Hex, bool>();

                foreach (var (tile, wasSet) in floor)
                {
                    var countBlack = 0;
                    foreach (var neigh in tile.Neighbors)
                    {
                        if (floor.TryGetValue(neigh, out var blackNeighbor))
                        {
                            if (blackNeighbor)
                                countBlack++;
                        }
                        else
                        {
                            next[neigh] = false;
                        }
                    }

                    next[tile] = countBlack switch
                    {
                        0 or > 2 when wasSet => false,
                        2 when !wasSet => true,
                        _ => wasSet
                    };
                }

                floor = next;

            }

            return floor.Values.Count(c => c);
        }

        static IEnumerable<List<Hex>> Parse(IEnumerable<string> input)
        {
            var directions = new List<List<Hex>>();

            var map = new Dictionary<string, Hex>
            {
                ["e"] = Hex.Directions[0],
                ["ne"] = Hex.Directions[1],
                ["nw"] = Hex.Directions[2],
                ["w"] = Hex.Directions[3],
                ["sw"] = Hex.Directions[4],
                ["se"] = Hex.Directions[5]
            };

            foreach (var line in input)
            {
                var steps = new List<Hex>();

                for (var i = 0; i < line.Length; ++i)
                {
                    var ch = line[i];
                    steps.Add(
                        ch is 'n' or 's'
                            ? map[line[i..(++i + 1)]]
                            : map[$"{ch}"]
                    );
                }

                directions.Add(steps);
            }

            return directions;
        }

        public record Hex(int Q, int R)
        {
            public static readonly Hex[] Directions = { new(1, 0), new(1, -1), new(0, -1), new(-1, 0), new(-1, 1), new(0, 1) };

            public static Hex operator +(Hex a, Hex b) => new(a.Q + b.Q, a.R + b.R);

            public Hex Neighbor(int dir) => this + Directions[(6 + dir % 6) % 6];

            public bool IsBlack;

            public Hex[] Neighbors => new[]
            {
                Neighbor(0),
                Neighbor(1),
                Neighbor(2),
                Neighbor(3),
                Neighbor(4),
                Neighbor(5),
            };
        }
    }
}
