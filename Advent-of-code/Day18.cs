using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code
{
    public static class Day18
    {
        public static long Part1(string input)
        {
            long sum = 0;
            
            foreach(string line in input.Split(Environment.NewLine))
            {
                Dictionary<int, Operation> ops = new Dictionary<int,Operation>();
                int Depth = 0;
                foreach (char c in line.Replace(" ", ""))
                {
                    KeyValuePair<int,Operation> last = ops.LastOrDefault(x=>x.Key == Depth);
                    switch (c)
                    {
                        case '+':
                            if(last.Value == null)
                            {
                                Operation top = ops.LastOrDefault(x => x.Key == Depth+1).Value;
                                ops[Depth] = new Addition(top,Depth);
                                ops.Remove(top.Priority);
                            }
                            else
                                ops[Depth] = new Addition(last.Value,Depth);
                            break;
                        case '*':
                            if (last.Value == null)
                            {
                                Operation top = ops.LastOrDefault(x => x.Key == Depth + 1).Value;
                                ops[Depth] = new Multiplication(top,Depth);
                                ops.Remove(top.Priority);
                            }
                            else
                                ops[Depth] = new Multiplication(last.Value,Depth);
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
                long total = ops[0].Execute();
                string str = ops[0].ToString();
                if (str != line)
                {
                    Console.WriteLine(line + " = " + total);
                }
                sum += total;
            }
            return sum;
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
                string str = "";
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
