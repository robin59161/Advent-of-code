using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2020
{
    public static class Day18
    {
        public static long Part1(string input)
        {
            return Solve(input.Split(Environment.NewLine)).Sum(x=>x.Execute());
        }

        private static List<Operation> Solve(string[] lines)
        {
            List<Operation> res = new List<Operation>();
            foreach (string line in lines)
            {
                Dictionary<int, Operation> ops = new Dictionary<int, Operation>();
                int Depth = 0;
                foreach (char c in line.Replace(" ", ""))
                {
                    KeyValuePair<int, Operation> last = ops.LastOrDefault(x => x.Key == Depth);
                    switch (c)
                    {
                        case '+':
                            if (last.Value == null)
                            {
                                Operation top = ops.LastOrDefault(x => x.Key == Depth + 1).Value;
                                ops[Depth] = new Addition(top, Depth);
                                ops.Remove(top.Priority);
                            }
                            else
                                ops[Depth] = new Addition(last.Value, Depth);
                            break;
                        case '*':
                            if (last.Value == null)
                            {
                                Operation top = ops.LastOrDefault(x => x.Key == Depth + 1).Value;
                                ops[Depth] = new Multiplication(top, Depth);
                                ops.Remove(top.Priority);
                            }
                            else
                                ops[Depth] = new Multiplication(last.Value, Depth);
                            break;
                        case '(':
                            Depth++;
                            break;
                        case ')':
                            Depth--;
                            Operation prev = ops.LastOrDefault(x => x.Key == Depth).Value;
                            if (prev != null)
                            {
                                prev.Right = last.Value;
                                ops.Remove(last.Key);
                            }
                            break;
                        default:
                            int num = Convert.ToInt32(c.ToString());
                            if (last.Value == null)
                                ops.Add(Depth, new Number(num, Depth));
                            else if (last.Key == Depth)
                                last.Value.Right = new Number(num, Depth);
                            else
                                ops.Add(Depth, new Number(num, Depth));
                            break;
                    }
                }
                res.Add(ops.First().Value);
            }
            return res;
        }

        private static List<Operation> Solve2(string[] lines)
        {
            List<Operation> res = new List<Operation>();
            foreach (string line in lines)
            {
                Dictionary<int, List<Operation>> ops = new Dictionary<int, List<Operation>>();
                int Depth = 0;
                foreach (char c in line.Replace(" ", ""))
                {
                    KeyValuePair<int, List<Operation>> last = ops.LastOrDefault(x => x.Key == Depth);
                    switch (c)
                    {
                        case '+':
                            if (last.Value == null)
                            {
                                Operation top = ops.LastOrDefault(x => x.Key == Depth + 1).Value.Last();
                                ops[Depth] = new List<Operation>() { new Addition(top, Depth) };
                                ops.Remove(top.Priority);
                            }
                            else
                            {
                                if (last.Value.Last() is Multiplication && Depth == last.Value.Last().Priority)
                                {
                                    Operation op = new Addition(last.Value.Last().Right, Depth);
                                    ops[Depth].Add(op);
                                    Operation second = last.Value.Reverse<Operation>().Skip(1).First();
                                    second.Right = op;
                                }
                                else
                                {
                                    ops[Depth][ops[Depth].Count() - 1] = new Addition(last.Value.Last(), Depth);
                                }
                            }
                            break;
                        case '*':
                            if (last.Value == null)
                            {
                                Operation top = ops.LastOrDefault(x => x.Key == Depth + 1).Value.First();
                                ops[Depth] = new List<Operation>() { new Multiplication(top, Depth) };
                                ops.Remove(top.Priority);
                            }
                            else
                            {
                                ops[Depth][ops[Depth].Count() - 1] = new Multiplication(last.Value.Last(), Depth);
                            }
                            break;
                        case '(':
                            Depth++;
                            break;
                        case ')':
                            Depth--;
                            List<Operation> prev = ops.LastOrDefault(x => x.Key == Depth).Value;
                            if (prev != null && prev.Count > 0)
                            {
                                prev.Last().Right = last.Value.First();
                                if (prev.Count > 1)
                                {
                                    prev.RemoveAt(1);
                                }
                                ops.Remove(last.Key);
                            }
                            break;
                        default:
                            int num = Convert.ToInt32(c.ToString());
                            if (last.Value == null)
                                ops.Add(Depth, new List<Operation>() { new Number(num, Depth) });
                            else if (last.Key == Depth)
                            {
                                if(last.Value.Count > 1)
                                {
                                    last.Value.Last(x=>x.Right == null).Right = new Number(num, Depth);
                                    last.Value.RemoveAt(1);
                                }
                                else
                                {
                                    last.Value.First().Right = new Number(num, Depth);
                                }
                            }
                            else
                                ops.Add(Depth, new List<Operation>() { new Number(num, Depth) });
                            break;
                    }
                }
                res.Add(ops.First().Value.First());
            }
            return res;
        }
        public static long Part2(string input)
        {
            List<Operation> first = Solve2(input.Split(Environment.NewLine));
            return first.Sum(x=>x.Execute());
        }

        public abstract class Operation
        {
            public Operation Left { get; set; }
            public Operation Right { get; set; }
            public int Priority { get; set; }

            public abstract long Execute();

            public Operation(int priority)
            {
                this.Priority = priority;
            }

        }

        public class Number : Operation
        {

            public long Value { get; set; }
            public override long Execute()
            {
                return Value;
            }
            public Number(long value, int priority): base(priority)
            {
                Value = value;
            }

            public override string ToString() { return Value.ToString(); }
        }

        public class Addition : Operation
        {
            public override long Execute()
            {
                return Left.Execute() + Right.Execute(); 
            }
            public Addition(Operation Last,int priority): base(priority)
            {
                this.Left = Last;
            }

            public override string ToString()
            {
                string str = "";
                if (Priority != Right.Priority && Priority == Left.Priority)
                    str = string.Format("{0} + ({1})", Left.ToString(), Right.ToString()); 
                else if(Priority != Left.Priority && Priority == Right.Priority)
                    str = string.Format("({0}) + {1}", Left.ToString(), Right.ToString());
                else if(Priority != Right.Priority && Priority != Left.Priority)
                    str = string.Format("({0}) + ({1})", Left.ToString(), Right.ToString());
                else
                    str = string.Format("{0} + {1}", Left.ToString(), Right.ToString());
                return str;
            }
        }

        public class Multiplication : Operation
        {
            public override long Execute()
            {
                return Left.Execute() * Right.Execute();
            }
            public Multiplication(Operation Last,int priority) : base(priority)
            {
                this.Left = Last;
            }

            public override string ToString()
            {
                string str;
                if (Priority != Right.Priority && Priority == Left.Priority)
                    str = string.Format("{0} * ({1})", Left.ToString(), Right.ToString());
                else if (Priority != Left.Priority && Priority == Right.Priority)
                    str = string.Format("({0}) * {1}", Left.ToString(), Right.ToString());
                else if (Priority != Right.Priority && Priority != Left.Priority)
                    str = string.Format("({0}) * ({1})", Left.ToString(), Right.ToString());
                else
                    str = string.Format("{0} * {1}", Left.ToString(), Right.ToString());
                return str;
            }
        }
    }
}
