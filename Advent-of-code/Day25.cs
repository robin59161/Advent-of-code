using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code
{
    public static class Day25
    {
        private const long BIG_PRIME = 20201227;

        public static long Part1(string input)
        {
            var inputs = input.Split(Environment.NewLine);
            var cardLoopSize = FindLoopSize(Convert.ToInt64(inputs[1]), 7);
            var doorEncryptionKey = Loop(Convert.ToInt64(inputs[0]), cardLoopSize);
            return doorEncryptionKey;
        }

		public static long FindLoopSize(long publicKey, long subjectNumber)
        {
            long value = 1;
            for(long i = 1; i < long.MaxValue; i++)
            {
                value = (value * subjectNumber) % BIG_PRIME;
                if (value == publicKey)
                    return i;
            }
            return -1;
        }

        public static long Loop(long subjectNUmber,long loopSize)
        {
            long value = 1;
            for(long i = 0; i < loopSize; i++)
            {
                value = (value * subjectNUmber) % BIG_PRIME;
            }
            return value;
        }
	}
}
