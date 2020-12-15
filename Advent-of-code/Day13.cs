using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code
{
    public static class Day13
    {
        public static double Part1(string input)
        {
            string[] inputs = input.Split(Environment.NewLine);
            int time = Convert.ToInt32(inputs[0]);
            int[] buses = inputs[1].Split(',').Distinct().Where(x => x != "x").Select(x => Convert.ToInt32(x)).ToArray();
            double minimum = -1;
            int BusId = 0;
            foreach(int bus in buses)
            {
                double closest = Math.Ceiling((double)time / bus);
                var res = closest * bus - time;
                if (minimum == -1 || res < minimum)
                {
                    minimum = res;
                    BusId = bus;
                }
            }
            return minimum * BusId;
        }

        public static long Part2(string input)
        {
            string[] inputs = input.Split(Environment.NewLine);
            int times = Convert.ToInt32(inputs[0]);
            List<(int busNum, int requiredDepartureOffset)> busDepartureInfo = inputs[1].Split(',').Select((x,index) =>(x,index)).ToList().Where(x=> x.x != "x").Select(x => (Convert.ToInt32(x.x),x.index)).ToList();

            long time = busDepartureInfo[0].busNum;
            long increment = busDepartureInfo[0].busNum;
            for (int i = 1; i < busDepartureInfo.Count(); i++)
            {
                (int busNum,int offset) indexed = busDepartureInfo[i];
                while ((time + indexed.offset) % indexed.busNum != 0)
                {
                    time += increment;
                }
                increment *= indexed.busNum;
            }
            return time;
        }
    }
}
