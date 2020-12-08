using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code
{
    public static class Day8
    {
        public static int Part1(string input)
        {
            List<int> seen = new List<int>();
            string[] lines = input.Split(Environment.NewLine);
            int current = 0;
            int acc = 0;
            Regex r = new Regex(@"(acc|jmp|nop)\s([+-]\d*)");
            while (!seen.Contains(current) && current < lines.Length)
            {
                seen.Add(current);
                string line = lines[current];
                Match m = r.Match(line);
                switch(m.Groups[1].Value)
                {
                    case "acc":
                        acc += Int32.Parse(m.Groups[2].Value);
                        current++;
                        break;
                    case "jmp":
                        current += Int32.Parse(m.Groups[2].Value);
                        break;
                    default:
                        current++;
                        break;
                }
                
            }
            return acc;
        }

        public static int Part2(string input)
        {
            string[] original = input.Split(Environment.NewLine);
            for(int i = 0;i < original.Length; i++)
            {
                Regex r = new Regex(@"(acc|jmp|nop)\s([+-]\d*)");
                if (r.Match(original[i]).Groups[1].Value == "acc")
                    continue;
                string[] modified = new string[original.Length];
                Array.Copy(original,modified,original.Length);
                if (modified[i].Contains("jmp"))
                    modified[i]=  modified[i].Replace("jmp", "nop");
                else
                    modified[i] = modified[i].Replace("nop", "jmp");

                int[] res = runBoot(modified);
                if (res[0] == original.Length)
                    return res[1];

            }
            throw new Exception("No Solution Found");
        }

        private static int[] runBoot(string[] lines)
        {
            List<int> seen = new List<int>();
            int current = 0;
            int acc = 0;
            Regex r = new Regex(@"(acc|jmp|nop)\s([+-]\d*)");
            while (!seen.Contains(current) && current < lines.Length)
            {
                seen.Add(current);
                string line = lines[current];
                Match m = r.Match(line);
                switch (m.Groups[1].Value)
                {
                    case "acc":
                        acc += Int32.Parse(m.Groups[2].Value);
                        current++;
                        break;
                    case "jmp":
                        current += Int32.Parse(m.Groups[2].Value);
                        break;
                    default:
                        current++;
                        break;
                }

            }
            return new int[] { current, acc };
        }
    }
}
