using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day16
    {
        public static int Part1(string input)
        {

            return 0;
        }


        public class Packet
        {
            public uint version { get; set; }
            public uint TypeID { get; set; }

            public uint LengthTypeID { get; set; }
            public List<Packet> SubPackets { get; set; }
            public List<uint> SubPacketValues { get; set; }


        }

    }
}
