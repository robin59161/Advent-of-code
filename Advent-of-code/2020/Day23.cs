using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Advent_of_code._2020
{
    public static class Day23
    {

        public static string Part1(string input)
        {
            ImmutableList<int> clock = input.Select(x => Convert.ToInt32(x.ToString())).ToImmutableList();
            var cups = new LinkedList<int>(clock);
            var res = Run2(cups, 100);
            var result = "";
            foreach (var _ in Enumerable.Range(0, 8))
            {
                res = res.NextOrFirst();
                result += res.Value;
            }
            return result;
        }
        //First naïv attempt
        public static int[] Run(int[] clock,int nbRepetition)
        {
            int current = 0;
            int CLOCK_LENGTH = clock.Length;
            for (int i = 0; i < nbRepetition; i++)
            {
                int[] pickup = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    pickup[j] = clock[(current + j + 1) % clock.Length];
                    clock[(current + j + 1) % CLOCK_LENGTH] = -1;
                }
                int destinationIndex = Array.IndexOf(clock, clock[current] - 1);
                if (destinationIndex == -1)
                {
                    if (Array.Exists(clock, x => x < clock[current] && x != -1))
                    {
                        int lookinkFor = clock[current] - 1;
                        while ((destinationIndex = Array.IndexOf(clock, lookinkFor)) == -1)
                        {
                            lookinkFor--;
                        }
                    }
                    else
                    {
                        destinationIndex = Array.IndexOf(clock, clock.Max());
                    }
                }
                if (destinationIndex < current)
                    destinationIndex += CLOCK_LENGTH;
                for (int k = current + 4; k <= destinationIndex; k++)
                {
                    clock[(k - 3) % CLOCK_LENGTH] = clock[k % CLOCK_LENGTH];
                }
                for (int k = 0; k < 3; k++)
                {
                    destinationIndex++;
                    clock[(destinationIndex - 3) % CLOCK_LENGTH] = pickup[k];
                }
                current = (current + 1) % CLOCK_LENGTH;
            }
            return clock;
        }

        public static ulong Part2(string input)
        {
            ImmutableList<int> clock = input.Select(x => Convert.ToInt32(x.ToString())).ToImmutableList();
            clock = clock.AddRange(Enumerable.Range(10, 1000001 - 10));
            var cups = new LinkedList<int>(clock);
            var res = Run2(cups, 10000000);
            return 1UL * (ulong)res.NextOrFirst().Value * (ulong)res.NextOrFirst().NextOrFirst().Value;
        }
        
        static LinkedListNode<int> Run2(LinkedList<int> cups, int rounds)
        {
            var cupsIndex = new Dictionary<int, LinkedListNode<int>>();
            var s = cups.First;
            while (s != null)
            {
                cupsIndex.Add(s.Value, s);
                s = s.Next;
            }

            var currentRound = 0;
            var currentCup = cups.First;
            do
            {
                currentRound++;
                var pickUp = new List<LinkedListNode<int>> {
                        currentCup.NextOrFirst(),
                        currentCup.NextOrFirst().NextOrFirst(),
                        currentCup.NextOrFirst().NextOrFirst().NextOrFirst()};
                foreach (var pick in pickUp)
                {
                    cups.Remove(pick);
                }

                var destinationCupValue = currentCup.Value - 1;
                while (destinationCupValue < 1 ||
                    pickUp.Any(p => p.Value == destinationCupValue) ||
                    destinationCupValue == currentCup.Value
                )
                {
                    destinationCupValue -= 1;
                    if (destinationCupValue < 1)
                    {
                        destinationCupValue = cupsIndex.Count();
                    }
                }

                currentCup = currentCup.NextOrFirst();
                var target = cupsIndex[destinationCupValue];
                foreach (var pick in pickUp)
                {
                    cups.AddAfter(target, pick);
                    target = target.NextOrFirst();
                }
            } while (currentRound < rounds);

            return cupsIndex[1];
        }
    }
   
    static class CircularLinkedList
    {
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
        {
            return current.Next ?? current.List.First;
        }
    }
}
