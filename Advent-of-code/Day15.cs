using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code
{
    public static class Day15
    {
        public static long Part1(string Input,int stop)
        {
            List<long> spoken =  Input.Split(',').Select(x => Convert.ToInt64(x)).ToList();
            for(int turn=spoken.Count+1; turn <= stop; turn++)
            {
                long last = spoken.Last();
                bool new_entry = spoken.IndexOf(last) == spoken.Count()-1;
                if(new_entry)
                {
                    spoken.Add(0);
                }
                else
                {
                    long t = (turn-1) - (spoken.WithIndex().Where(x => x.item == last && x.index != spoken.Count()-1).Last().index+1);
                    spoken.Add(t);
                }
            }
            return spoken.Last();
        }

        public static long Part2(string Input, int stop)
        {
            Dictionary<long,(long,long)> spoken = new Dictionary<long, (long,long)>();
            long last = 0;
            foreach (var(line,Index) in Input.Split(',').WithIndex())
            {
                last = Convert.ToInt64(line);
                spoken.Add(last, (Index+1,Index+1));
            }
            for (int turn = spoken.Count + 1; turn <= stop; turn++)
            {
                if (!spoken.ContainsKey(last))
                {
                    spoken[0] = (turn, spoken[0].Item2);
                    last = 0;
                }
                else
                {
                    long num = turn - 1 - spoken[last].Item2;
                    if(spoken.ContainsKey(num))
                        spoken[num] = (turn, spoken[num].Item1);
                    else
                        spoken[num] = (turn,turn);
                    last = num;
                }
            }
            return last;
        }
    }
}
