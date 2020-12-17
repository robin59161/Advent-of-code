using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Advent_of_code
{
    class Program
    {
        static readonly int Day = DateTime.Now.Day;
        static void Main(string[] args)
        {
            for (int d = 1; d <= Day; d++)
            {
                string className = (d < 10) ? "0"+d.ToString() : d.ToString();
                Type t = Type.GetType("Advent_of_code.Day" + className);

                if (t != null)
                {
                    string input = Resources.Resource1.ResourceManager.GetString("Input" + d);
                    Console.WriteLine("Jour " + d);
                    Console.WriteLine("╔════════╦═════════════════╦═══════════════════════════════╗");
                    for (int i = 1; i <= 2; i++)
                    {
                        MethodInfo method = t.GetMethod("Part" + i);
                        if (method != null)
                        {
                            Stopwatch st = new Stopwatch();
                            st.Start();
                            var res = string.Empty;
                            try
                            {
                                res = method.Invoke(t, new object[] { input }).ToString();
                            }catch(Exception e)
                            {
                                res = "Error";
                            }
                            st.Stop();
                            Console.WriteLine("║ Part {0} ║ {1,-15} ║ Temps d'éxécution: {2,-10} ║", i, res, Math.Round(st.Elapsed.TotalSeconds, 3) + "s");
                        }
                        if (i == 1)
                            Console.WriteLine("╠════════╬═════════════════╬═══════════════════════════════╣");
                    }
                    Console.WriteLine("╚════════╩═════════════════╩═══════════════════════════════╝");
                }
            }
            Console.ReadKey();
        }
        
    }
    
}
