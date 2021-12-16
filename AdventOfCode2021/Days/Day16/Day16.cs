namespace AdventOfCode2021.Days.Day16
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class Day16 : BaseDay
    {
        private Dictionary<char, string> hexValueDictionary = new Dictionary<char, string>
        {
            { '0', "0000" },
            { '1', "0001" },
            { '2', "0010" },
            { '3', "0011" },
            { '4', "0100" },
            { '5', "0101" },
            { '6', "0110" },
            { '7', "0111" },
            { '8', "1000" },
            { '9', "1001" },
            { 'A', "1010" },
            { 'B', "1011" },
            { 'C', "1100" },
            { 'D', "1101" },
            { 'E', "1110" },
            { 'F', "1111" }
        };

        public override string Part1()
        {
            string binaryInput = string.Empty;
            foreach (char c in Input)
            {
                binaryInput += hexValueDictionary[c];

                //int i = Convert.ToInt32(c.ToString(), 16);
                //binaryInput += Convert.ToString(i, 2);
            }

            int index = 0;
            List<Packet> packets = ReadPacket(binaryInput, ref index);

            return packets.Sum(packet => packet.Version).ToString();
        }


        public override string Part2()
        {
            string binaryInput = string.Empty;
            foreach (char c in Input)
            {
                binaryInput += hexValueDictionary[c];

                //int i = Convert.ToInt32(c.ToString(), 16);
                //binaryInput += Convert.ToString(i, 2);
            }

            int index = 0;
            List<Packet> packets = ReadPacket(binaryInput, ref index);

            long value =  CalcPacketValue(packets[0]);

            return value.ToString();
        }

        private long CalcPacketValue(Packet packet)
        {
            if (packet.PacketType == 4)
            {
                return packet.Data;
            }

            List<long> values = new List<long>();
            foreach (Packet packetSubPacket in packet.SubPackets)
            {
                values.Add(CalcPacketValue(packetSubPacket));
            }

            switch (packet.PacketType)
            {
                case 0: // Sum
                    if (packet.SubPackets.Count == 1)
                    {
                        return values[0];
                    }
                    else
                    {
                        long total = values.Aggregate((acc, cur) => acc + cur);
                        return total;
                    }

                case 1: // product
                    if (packet.SubPackets.Count == 1)
                    {
                        return values[0];
                    }
                    else
                    {
                        long total = values.Aggregate((acc, cur) => acc * cur);
                        return total;
                    }

                case 2: // Minimum
                    return values.Min();
                case 3: // Maximum
                    return values.Max();
                case 5: // Greater than
                    return values[0] > values[1] ? 1 : 0;
                case 6: // Less than
                    return values[0] < values[1] ? 1 : 0;
                case 7: // Equal
                    return values[0] == values[1] ? 1 : 0;
                default:
                    throw new Exception("Unknown packet type " + packet.PacketType);
            }
        }

        private List<Packet> ReadPacket(string binaryInput, ref int index)
        {
            List<Packet> packets = new List<Packet>();
            var currentPacket = new Packet();
            packets.Add(currentPacket);

            // read header
            int version = Convert.ToInt32(binaryInput.Substring(index, 3), 2);
            index += 3;
            currentPacket.Version = version;

            int packetType = Convert.ToInt32(binaryInput.Substring(index, 3), 2);
            index += 3;
            currentPacket.PacketType = packetType;

            // parse data
            switch (packetType)
            {
                case 4: // literal value
                    long value = ReadLiteralValue(binaryInput, ref index);
                    currentPacket.Data = value;
                    break;
                default: // operator
                    int lengthTypeId = Convert.ToInt32(binaryInput.Substring(index, 1), 2);
                    index++;

                    if (lengthTypeId == 0)
                    {
                        // next 15 bits are length of subpacket
                        int subPacketBitLength = Convert.ToInt32(binaryInput.Substring(index, 15), 2);
                        index += 15;

                        string subPacket = binaryInput.Substring(index, subPacketBitLength);
                        index += subPacketBitLength;

                        int subPacketIndex = 0;
                        while (subPacketIndex < subPacketBitLength)
                        {
                            var readPackets = ReadPacket(subPacket, ref subPacketIndex);
                            packets.AddRange(readPackets);
                            currentPacket.SubPackets.Add(readPackets[0]);
                        }
                    }
                    else
                    {
                        // next 11 bits are number of subpackets
                        int amountSubPackets = Convert.ToInt32(binaryInput.Substring(index, 11), 2);
                        index += 11;

                        for (int i = 0; i < amountSubPackets; i++)
                        {
                            List<Packet> readPackets = ReadPacket(binaryInput, ref index);
                            packets.AddRange(readPackets);
                            currentPacket.SubPackets.Add(readPackets[0]);
                        }
                    }

                    break;
            }

            return packets;
        }

        private long ReadLiteralValue(string binaryInput, ref int index)
        {
            string valueBits = "";
            while (binaryInput[index] == '1')
            {
                valueBits += binaryInput.Substring(index + 1, 4);
                index += 5;
            }

            valueBits += binaryInput.Substring(index + 1, 4);
            index += 5;

            long number = Convert.ToInt64(valueBits, 2);
            return number;
        }

        public class Packet
        {
            public int Version { get; set; }

            public int PacketType { get; set; }

            public long Data { get; set; }

            public List<Packet> SubPackets { get; } = new List<Packet>();
        }
    }
}