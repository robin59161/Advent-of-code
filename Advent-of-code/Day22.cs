using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code
{
    public static class Day22
    {
        static Dictionary<int, Queue<int>> cards;
        public static long Part1(string input)
        {
            Parse(input);
            while (cards.Where(x=>x.Value.Count == 0).Count() == 0)
            {
                int p1 = cards[1].Dequeue();
                int p2 = cards[2].Dequeue();
                if(p1 > p2)
                {
                    cards[1].Enqueue(p1);
                    cards[1].Enqueue(p2);
                }
                else
                {
                    cards[2].Enqueue(p2);
                    cards[2].Enqueue(p1);
                }
            }
            int[] winner = cards.First(x => x.Value.Count != 0).Value.ToArray();
            int res = 0;
            for(int i = winner.Length;i > 0; i--)
            {
                res += i * winner[winner.Length-i];
            }
            return res;
        }

        public static long Part2(string input)
        {
            Parse(input);
            KeyValuePair<int, Queue<int>> winner = Game(cards);
            int[] deck = winner.Value.ToArray();
            int res = 0;
            for (int i = deck.Length; i > 0; i--)
            {
                res += i * deck[deck.Length - i];
            }
            return res;
        }

        static KeyValuePair<int, Queue<int>> Game(Dictionary<int,Queue<int>> deck)
        {
            int round = 1;
            HashSet<string> config = new HashSet<string>();
            while (deck.Where(x => x.Value.Count == 0).Count() == 0)
            {
                string conf = String.Join(',', deck[1]) + "|" + String.Join(',', deck[2]);
                if (!config.Add(conf))
                    return new KeyValuePair<int, Queue<int>>(1, deck[1]);
                int p1 = deck[1].Dequeue();
                int p2 = deck[2].Dequeue();
                if (deck[1].Count >= p1 && deck[2].Count >= p2)
                {
                    Dictionary<int, Queue<int>> subDeck = new Dictionary<int, Queue<int>>();
                    subDeck[1] = new Queue<int>(deck[1].Take(p1));
                    subDeck[2] = new Queue<int>(deck[2].Take(p2));
                    KeyValuePair<int, Queue<int>> res = Game(subDeck);
                    if (res.Key == 1)
                    {
                        deck[1].Enqueue(p1);
                        deck[1].Enqueue(p2);
                    }
                    else
                    {
                        deck[2].Enqueue(p2);
                        deck[2].Enqueue(p1);
                    }
                }
                else if (p1 > p2)
                {
                    deck[1].Enqueue(p1);
                    deck[1].Enqueue(p2);
                }
                else
                {
                    deck[2].Enqueue(p2);
                    deck[2].Enqueue(p1);
                }
                round++;
            }
            return deck.First(x => x.Value.Count != 0);
        }

        static void Parse(string input)
        {
            cards = new Dictionary<int, Queue<int>>();
            cards = (
                from tile in input.Split(Environment.NewLine + Environment.NewLine)
                let lines = tile.Split(Environment.NewLine)
                let id = Regex.Match(lines[0], "\\d+").Value
                let content = new Queue<int>(lines.Skip(1).Where(x => x != "").Select(x => int.Parse(x)))
                select (int.Parse(id), content)
            ).ToDictionary(p => p.Item1, p => p.content);
        }
    }
}
