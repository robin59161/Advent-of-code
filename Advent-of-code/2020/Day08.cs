using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code._2020
{
    public static class Day08
    {
        public static int Part1(string input)
        {
            var lines = input.Split(Environment.NewLine).Select(l => l.Split(' '));
            var instructions = lines.Select(x => (inst: x[0], value: int.Parse(x[1][1..]) * (x[1][0] == '-' ? -1 : 1))).ToList();
            return RunBoot(instructions).acc;
        }

        public static int Part2(string input)
        {
            var lines = input.Split(Environment.NewLine).Select(l => l.Split(' '));
            var instructions = lines.Select(x => (inst: x[0], value: int.Parse(x[1][1..]) * (x[1][0] == '-' ? -1 : 1))).ToList();
            return Flip(instructions);
        }

        static (int acc, int index) RunBoot(List<(string inst, int value)> i)
        {
            var instructionsExecuted = new List<int>();
            (int acc, int index) reg = new(0, 0);

            (int acc, int index) Process((string instruction, int value) i) => i.instruction
             switch
            {
                "acc" => (reg.acc + i.value, reg.index + 1),
                "jmp" => (reg.acc, reg.index + i.value),
                "nop" => (reg.acc, reg.index + 1)
            };

            while (reg.index < i.Count && !instructionsExecuted.Contains(reg.index))
            {
                instructionsExecuted.Add(reg.index);
                reg = Process(i[reg.index]);
            }
            return reg;
        }

        static int Flip(List<(string inst, int value)> instructions)
        {
            string flip(string value) => value switch { "jmp" => "nop", "nop" => "jmp", "acc" => "acc" };
            for (int i = 0; i < instructions.Count; i++)
            {
                instructions[i] = (flip(instructions[i].inst), instructions[i].value);
                var comp = RunBoot(instructions);
                if (comp.index == instructions.Count) return comp.acc;
                instructions[i] = (flip(instructions[i].inst), instructions[i].value);
            }
            return 0;
        }

    }
}
