using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code
{
    public static class Day5
    {
        private static Dictionary<char, char> binMap = new Dictionary<char, char>()
        {
            {'F', '0'}, {'B','1'}, {'L', '0'}, {'R', '1'}
        };

        public static int Part1(string input)
        {
            int maxId = 0;
            foreach(string raw in input.Split(Environment.NewLine))
            {
                string binRaw = toBinary(raw[0..7]);
                string binCol = toBinary(raw[7..]);
                int id = (Convert.ToInt32(binRaw, 2) * 8) + Convert.ToInt32(binCol,2);
                if (id > maxId)
                    maxId = id;
            }
            return maxId;
        }

        public static string toBinary(string seat)
        {
            char[] seatBin = new char[seat.Length];
            for (int i = 0; i < seat.Length; i++)
            {
                seatBin[i] = binMap[seat[i]];
            }
            return new string(seatBin);
        }

        public static int Part2(string input)
        {
            string[] inputRows = input.Split(Environment.NewLine);

            int[] seats = inputRows.Select(x =>
            {   
                var binRaw = toBinary(x[0..7]);
                var binCol = toBinary(x[7..]);
                return (Convert.ToInt32(binRaw, 2) * 8) + Convert.ToInt32(binCol, 2);
            }).OrderBy(x => x).ToArray();
            for(int i=0;i < seats.Count(); i++)
            {
                if (seats[i + 1] - 1 == seats[i] + 1)
                    return seats[i] + 1;
            }
            throw new Exception("No result");
        }
    }
}
