using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code
{
    public static class Day16
    {
        public static long Part1(string input)
        {
            List<(int min, int max)> ranges = new List<(int min, int max)>();
            string step = "range";
            List<int> errors = new List<int>();
            foreach(string line in input.Split(Environment.NewLine))
            {
                if (string.IsNullOrEmpty(line)) continue;
                else if (line == "your ticket:")
                {
                    step = "mine";
                }
                else if (line == "nearby tickets:")
                {
                    step = "nearby";
                }
                else if (step == "range" && line != "")
                {
                    Regex range = new Regex(@"(\d+)-(\d+) or (\d+)-(\d+)");
                    Match m = range.Match(line);
                    (int, int) fRange = (Convert.ToInt32(m.Groups[1].Value), Convert.ToInt32(m.Groups[2].Value));
                    (int, int) sRange = (Convert.ToInt32(m.Groups[3].Value), Convert.ToInt32(m.Groups[4].Value));
                    ranges.Add(fRange);
                    ranges.Add(sRange);
                }else if(step == "nearby")
                {
                    foreach(int numero in line.Split(',').Select(x => Convert.ToInt32(x))){
                        bool found = false;
                        foreach((int min, int max) in ranges)
                        {
                            if (numero > min && numero < max) {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                            errors.Add(numero);
                    }
                }
            }
            return errors.Sum();
        }

        public static long Part2(string input)
        {
            List<customRange> ranges = new List<customRange>();
            string step = "range";
            List<int> myTicket = new List<int>();
            List<fact> answers = new List<fact>();
            foreach (string line in input.Split(Environment.NewLine))
            {
                if (string.IsNullOrEmpty(line)) continue;
                else if (line == "your ticket:")
                {
                    step = "mine";
                }
                else if (line == "nearby tickets:")
                {
                    step = "nearby";
                }
                else if (step == "mine")
                    myTicket = line.Split(',').Select(x=>Convert.ToInt32(x)).ToList();
                else if (step == "range" && line != "")
                {
                    Regex range = new Regex(@"(.*): (\d+)-(\d+) or (\d+)-(\d+)");
                    Match m = range.Match(line);
                    (int, int) fRange = (Convert.ToInt32(m.Groups[2].Value), Convert.ToInt32(m.Groups[3].Value));
                    (int, int) sRange = (Convert.ToInt32(m.Groups[4].Value), Convert.ToInt32(m.Groups[5].Value));
                    customRange r = new customRange(fRange, sRange, m.Groups[1].Value);
                    ranges.Add(r);
                }
                else if (step == "nearby")
                {
                    int[] values = line.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                    bool isValid = true;
                    for (int i = 0; i < values.Length; i++)
                    {
                        fact answer = answers.FirstOrDefault(x => x.index == i);

                        if (answer == null || answer.possibles.Count != 1)
                        {
                            List<string> temp = new List<string>();
                            foreach (customRange range in ranges)
                            {
                                if (range.Include(values[i]))
                                {
                                    temp.Add(range.name);
                                }
                            }
                            if (answer == null)
                            {
                                answers.Add(new fact(i, temp));
                            }
                            else if (temp.Count > 0)
                            {
                                answer.Intersect(temp);
                            }
                            else
                            {
                                isValid = false;
                                break;
                            }
                        }
                    }
                    if (isValid)
                    {
                        answers.ForEach(x => x.Perform());
                    }
                }
            }
            while (answers.Exists(x=>x.possibles.Count > 1))
            {
                List<fact> solved = answers.Where(x => x.possibles.Count == 1).ToList();
                answers.ForEach(x => x.Remove(solved.SelectMany(y => y.possibles).ToList()));
            }
            long res = 1;
            foreach(int index in answers.Where(x => x.Solution.StartsWith("departure")).Select(x => x.index))
            {
                res*= myTicket[index];
            }
            return res;
        }
        public static bool Include(this (int min, int max) range, int number)
        {
            return number >= range.min && number <= range.max;
        }
    }

    public class fact
    {
        public int index;
        public List<string> possibles { get; set; }
        public List<string> temp { get; set; }

        public string Solution
        {
            get
            {
                return possibles.First();
            }
        }

        public fact(int index,List<string> possibles)
        {
            this.index = index;
            this.possibles = possibles;
            this.temp = possibles;
        }

        public void Intersect(List<string> names)
        {
            temp = names;
        }

        public void Perform()
        {
            possibles = possibles.Intersect(temp).ToList();
        }

        public void Remove(List<string> solved)
        {
            if(possibles.Count != 1)
                this.possibles= this.possibles.Except(solved).ToList();
        }
    }

    public class customRange
    {
        public string name;
        public (int min,int max) LowerRange { get; set; }
        public (int min, int max) HigherRange { get; set; }
        public customRange((int min, int max) upper, (int min, int max) lower,string name)
        {
            this.name = name;
            this.LowerRange = lower;
            this.HigherRange = upper;
        }
        public bool Include(int number)
        {
            return LowerRange.Include(number) || HigherRange.Include(number);
        }

        
    }

}
