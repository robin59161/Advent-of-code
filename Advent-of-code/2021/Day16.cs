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
            string binarystring = String.Join(String.Empty,
              input.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            );
            Packet Mainpacket = ParsePacket(binarystring, out string restOfBinary);
            int version = Mainpacket.version + CountVersion(Mainpacket.SubPackets);
            return version;
        }

        public static long Part2(string input)
        {
            string binarystring = String.Join(String.Empty,
              input.Select(
                c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
              )
            );
            Packet Mainpacket = ParsePacket(binarystring, out string restOfBinary);
            return GetValue(Mainpacket);
        }

        public static long GetValue(Packet packet)
        {
            long value = 0;
            switch (packet.TypeID)
            {
                case (0):
                    value += packet.SubPackets.Sum(pack => GetValue(pack));
                    break;
                case (1):
                    long mult = 1;
                    foreach(Packet pack in packet.SubPackets)
                    {
                        mult *= GetValue(pack);
                    }
                    value += mult;
                    break;
                case (2):
                    value += packet.SubPackets.Min(pack => GetValue(pack));
                    break;
                case (3):
                    value += packet.SubPackets.Max(pack => GetValue(pack));
                    break;
                case (4):
                    value += Convert.ToInt64(string.Join("", packet.SubPacketValues), 2);
                    break;
                case (5):
                    Packet first5 = packet.SubPackets.ElementAt(0);
                    Packet second5 = packet.SubPackets.ElementAt(1);
                    value += GetValue(first5) > GetValue(second5) ? 1 : 0;
                    break;
                case (6):
                    Packet first6 = packet.SubPackets.ElementAt(0);
                    Packet second6 = packet.SubPackets.ElementAt(1);
                    value += GetValue(first6) < GetValue(second6) ? 1 : 0;
                    break;
                case (7):
                    Packet first7 = packet.SubPackets.ElementAt(0);
                    Packet second7 = packet.SubPackets.ElementAt(1);
                    value += GetValue(first7) == GetValue(second7) ? 1 : 0;
                    break;
            }
            return value;
        }

        public static int CountVersion(List<Packet> packets)
        {
            if(packets.Count() == 0)
            {
                return 0;
            }
            else
            {
                int version = 0;
                foreach(Packet packet in packets)
                {
                    version += packet.version + CountVersion(packet.SubPackets);
                }
                return version;
            }
        }

        public static Packet ParsePacket(string binarystring, out string restOfBinary)
        {
            Packet packet = new Packet();
            packet.version = Convert.ToInt32(binarystring.Substring(0, 3), 2);
            packet.TypeID = Convert.ToInt32(binarystring.Substring(3, 3), 2);
            if (packet.TypeID == 4)
            {
                GetLiterralValues(binarystring.Substring(6, binarystring.Length - 6), packet.SubPacketValues, out int index);
                restOfBinary = binarystring.Substring(6 + index, binarystring.Length - (6 + index));
            }
            else
            {
                if(binarystring[6] == '0')
                {
                    packet.LengthTypeID = Convert.ToInt32(binarystring.Substring(7, 15), 2);
                    int i = 0;
                    int length = packet.LengthTypeID;
                    restOfBinary = binarystring.Substring(22 + packet.LengthTypeID, binarystring.Length - (22 + packet.LengthTypeID));
                    while (binarystring.Length > 0)
                    {
                        int start = 0;
                        if (i == 0)
                        {
                            start = 22;
                        }
                        packet.SubPackets.Add(ParsePacket(binarystring.Substring(start, length), out string restOfBin));
                        length = restOfBin.Length;
                        binarystring = restOfBin;
                        i++;
                    }
                }
                else
                {
                    packet.NumberOfSubPackets = Convert.ToInt32(binarystring.Substring(7, 11), 2);
                    for(int i = 0; i < packet.NumberOfSubPackets; i++)
                    {
                        int start = 0;
                        if(i == 0)
                        {
                            start = 18;
                        }
                        packet.SubPackets.Add(ParsePacket(binarystring.Substring(start, binarystring.Length - start), out string restOfBin));
                        binarystring = restOfBin;
                    }
                    restOfBinary = binarystring;
                }
            }
            return packet;
        }

        public static void GetLiterralValues(string binaryString, List<string> bits, out int index)
        {
            if(binaryString[0] == '0')
            {
                index = 5;
                bits.Add(binaryString.Substring(1, 4));
            }
            else
            {
                bits.Add(binaryString.Substring(1, 4));
                GetLiterralValues(binaryString.Substring(5, binaryString.Length - 5), bits, out int ind);
                index = ind + 5;
            }
        }

        public class Packet
        {
            public Packet()
            {
                SubPackets = new List<Packet>();
                SubPacketValues = new List<string>();
            }
            public int version { get; set; }
            public int TypeID { get; set; }

            public int LengthTypeID { get; set; }

            public int NumberOfSubPackets { get; set; }
            public List<Packet> SubPackets { get; set; }
            public List<string> SubPacketValues { get; set; }


        }

    }
}
