using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code
{
    public static class Day19
    {
        static Dictionary<int, string> rules;
        public static long Part1(string input)
        {
            int current = 0;
            string[] lines = input.Split(Environment.NewLine);
            string line = lines[current];
            rules = new Dictionary<int, string>();
            do
            {
                rules.Add(Convert.ToInt32(line.Substring(0, line.IndexOf(':'))), line.Substring(line.IndexOf(':')+2).Replace('"',' ').Trim());
                current++;
                line = lines[current];
            } while (!string.IsNullOrEmpty(line));

            Regex regex = new Regex(@"^"+BuildRegex(0)+"$");
            current++;
            long count = 0;
            do
            {
                line = lines[current];
                if (regex.IsMatch(line))
                    count++;
                current++;
            } while (!string.IsNullOrEmpty(line) && current < lines.Length);
            return count;
        }

        public static String BuildRegex(int ruleIndex)
        {
            string rule = rules[ruleIndex];
            if (rule == "a") return "a";
            else if (rule == "b") return "b";
            else if (rule.Contains("|"))
            {
                string[] rulesPart = rule.Split('|');
                string str = "(";
                int first = 0;
                foreach(string part in rulesPart)
                {
                    str += "(";
                    foreach(string subpart in part.Trim().Split(' '))
                    {
                        str += BuildRegex(Convert.ToInt32(subpart));
                    }
                    str += ")";
                    if (first == 0)
                        str += "|";
                    first++;
                }
                str += ")";
                return str;
            }
            else
            {
                string str = string.Empty;
                foreach (string subpart in rule.Split(' '))
                {
                    str += BuildRegex(Convert.ToInt32(subpart));
                }
                return str;
            }
        }

        public static long Part2(string input)
        {
            int current = 0;
            input = input.Replace("8: 42", "8: 42 | 42 8").Replace("11: 42 31", "11: 42 31 | 42 11 31");
            string[] lines = input.Split(Environment.NewLine);
            string line = lines[current];
            rules = new Dictionary<int, string>();
            do
            {
                rules.Add(Convert.ToInt32(line.Substring(0, line.IndexOf(':'))), line.Substring(line.IndexOf(':') + 2).Replace('"', ' ').Trim());
                current++;
                line = lines[current];
            } while (!string.IsNullOrEmpty(line));

            Regex regex = new Regex(@"^" + BuildRegex2(0) + "$");
            current++;
            long count = 0;
            do
            {
                line = lines[current];
                Match m = regex.Match(line);
                if (m.Success)
                {
                    string res = m.Groups["cycle"].Value;
                    Regex group1 = new Regex(BuildRegex2(42));
                    Regex group2 = new Regex(BuildRegex2(8));
                    var r = group1.Matches(res);
                    var r2 = group2.Matches(res);
                    if(r.Count == r2.Count)
                        count++;
                }
                current++;
            } while (!string.IsNullOrEmpty(line) && current < lines.Length);
            return count;
        }

        public static String BuildRegex2(int ruleIndex)
        {
            string rule = rules[ruleIndex];
            if (rule == "a") return "a";
            else if (rule == "b") return "b";
            else if (rule.Contains("|"))
            {
                string[] rulesPart = rule.Split('|');
                string str = "(";
                int first = 0;
                bool cycle = rule.Split('|')[1].Split(' ').ToList().Exists(x => x == ruleIndex.ToString());
                foreach (string part in rulesPart)
                {
                    str += "(";
                    if (cycle)
                        str += "?<cycle>";
                   
                    foreach (string subpart in part.Trim().Split(' '))
                    {
                        if(!cycle)
                            str += BuildRegex2(Convert.ToInt32(subpart));
                        else
                            str+= BuildRegex2(Convert.ToInt32(subpart))+"+";
                    }
                    str += ")";
                    if (first == 0 && !cycle)
                        str += "|";
                    else if (cycle)
                        break;
                    first++;
                }
                str += ")";
                return str;
            }
            else
            {
                string str = string.Empty;
                foreach (string subpart in rule.Split(' '))
                {
                    str += BuildRegex2(Convert.ToInt32(subpart));
                }
                return str;
            }
        }
    }
}
