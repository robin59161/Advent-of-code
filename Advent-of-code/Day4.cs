using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code
{
    public static class Day4
    {
        public static int Part1(string input)
        {
            string regex = @"(?=.*(ecl:[^\s]+))(?=.*(hcl:[^\s]+))(?=.*(iyr:[^\s]+))(?=.*(pid:[^\s]+))(?=.*(hgt:[^\s]+))(?=.*(eyr:[^\s]+))(?=.*(byr:[^\s]+))";
            List<string> migrants = new List<string>(input.Split(new string[] { "\r\n\r\n" },
                               StringSplitOptions.RemoveEmptyEntries));
            return migrants.Where(x => Regex.IsMatch(x.Replace(Environment.NewLine, " "), regex)).Count();
        }

        public static int Part2(string input)
        {
            string regex = @"(?=.*(ecl:(amb|blu|brn|gry|grn|hzl|oth)))(?=.*(hcl:\#([0-9]|[a-f]){6}))(?=.*(iyr:(201[0-9]|2020)))(?=.*(pid:[0-9]{9}(?![0-9])))(?=.*(hgt:1[5-8][0-9]cm|19[0-3]cm|59in|6[0-9]in|7[0-6]in))(?=.*(eyr:202[0-9]|2030))(?=.*(byr:19[2-9][0-9]|200[0-2]))";

            List<string> migrants = new List<string>(input.Split(new string[] { "\r\n\r\n" },
                               StringSplitOptions.RemoveEmptyEntries));
            return migrants.Where(x => Regex.IsMatch(x.Replace(Environment.NewLine, " "), regex)).Count();
        }
    }
}
