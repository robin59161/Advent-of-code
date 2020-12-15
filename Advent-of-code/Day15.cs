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
    }
}
