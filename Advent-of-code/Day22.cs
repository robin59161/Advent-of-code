using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code
{
    public static class Day22
    {
        static Dictionary<int, int[]> cards;
        public static long Part1(string input)
        {
            Parse(input);
            while (cards.Where(x=>x.Value.Length == 0).Count() == 0)
            {
                int p1 = cards[1].Take(1).First();
                int p2 = cards[2].Take(1).First();
                if(p1 > p2)
                {
                    cards[1] = cards[1].Skip(1).Append(p1).Append(p2).ToArray();
                    cards[2] = cards[2].Skip(1).ToArray();
                }
                else
                {
                    cards[2] = cards[2].Skip(1).Append(p2).Append(p1).ToArray();
                    cards[1] = cards[1].Skip(1).ToArray();
                }
            }
            int[] winner = cards.First(x => x.Value.Length != 0).Value;
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
            KeyValuePair<int, int[]> winner = Game(cards);
            int res = 0;
            for (int i = winner.Value.Length; i > 0; i--)
            {
                res += i * winner.Value[winner.Value.Length - i];
            }
            return res;
        }

        static KeyValuePair<int,int[]> Game(Dictionary<int,int[]> deck)
        {
            int round = 1;
            HashSet<string> config = new HashSet<string>();
            while (deck.Where(x => x.Value.Length == 0).Count() == 0)
            {
                string conf = String.Join(',', deck[1]) + "|" + String.Join(',', deck[2]);
                if (!config.Add(conf))
                    return new KeyValuePair<int, int[]>(1, deck[1]);
                int p1 = deck[1].First();
                int p2 = deck[2].First();
                if (deck[1].Length > p1 && deck[2].Length > p2)
                {
                    deck[1] = deck[1].Skip(1).ToArray();
                    deck[2] = deck[2].Skip(1).ToArray();
                    Dictionary<int, int[]> subDeck = new Dictionary<int, int[]>();
                    subDeck[1] = deck[1].Take(p1).ToArray();
                    subDeck[2] = deck[2].Take(p2).ToArray();
                    KeyValuePair<int, int[]> res = Game(subDeck);
                    if(res.Key == 1)
                        deck[1] = deck[1].Append(p1).Append(p2).ToArray();
                    else
                        deck[2] = deck[2].Append(p2).Append(p1).ToArray();
                }
                else if (p1 > p2)
                {
                    deck[1] = deck[1].Skip(1).Append(p1).Append(p2).ToArray();
                    deck[2] = deck[2].Skip(1).ToArray();
                }
                else
                {
                    deck[2] = deck[2].Skip(1).Append(p2).Append(p1).ToArray();
                    deck[1] = deck[1].Skip(1).ToArray();
                }
                round++;
            }
            return deck.First(x => x.Value.Length != 0);
        }

        static void Parse(string input)
        {
            cards = new Dictionary<int, int[]>();
            cards = (
                from tile in input.Split(Environment.NewLine + Environment.NewLine)
                let lines = tile.Split(Environment.NewLine)
                let id = Regex.Match(lines[0], "\\d+").Value
                let content = lines.Skip(1).Where(x => x != "").Select(x => int.Parse(x)).ToArray()
                select (int.Parse(id), content)
            ).ToDictionary(p => p.Item1, p => p.content);
        }
    }
}
