﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;

namespace Advent_of_code
{
    class Program
    {
        static readonly int Day = 25;
        static readonly int Start = 1;
        static void Main(string[] args)
        {
            (string, ResourceManager) choice = PrintChoice();
            string className = (1 < 10) ? "0"+1.ToString() : 1.ToString();
            Type t = Type.GetType(String.Format("Advent_of_code._{0}.Day{1}",choice.Item1,className));

            if (t != null)
            {
                string input = choice.Item2.GetString("Input1");
                Console.WriteLine("Jour 1");
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
                        }
                        catch (Exception)
                        {
                            res = "Error";
                        }
                        st.Stop();
                        Console.WriteLine("║ Part {0} ║ {1,-15} ║ Temps d'éxécution: {2,-10} ║", i, res, Math.Round(st.Elapsed.TotalMilliseconds, 3) + "ms");
                    }
                    if (i == 1)
                        Console.WriteLine("╠════════╬═════════════════╬═══════════════════════════════╣");
                }
                Console.WriteLine("╚════════╩═════════════════╩═══════════════════════════════╝");
            }
            Console.ReadKey();
        }

        public static (string,ResourceManager) PrintChoice()
        {
            while (true) {
                Console.WriteLine("Quelle année souhaité vous exécuter :");
                Console.WriteLine("1. 2021");
                Console.WriteLine("2. 2023");
                var key = Console.ReadKey();
                Console.WriteLine("");
                switch (key.Key)
                {
                    case ConsoleKey.NumPad2:
                        return ("2023", Resources.Input2023.ResourceManager);
                    case ConsoleKey.D2:
                        return ("2023", Resources.Input2023.ResourceManager);
                    case ConsoleKey.NumPad1:
                        return ("2021", Resources.Input2023.ResourceManager);
                    case ConsoleKey.D1:
                        return ("2021", Resources.Input2023.ResourceManager);
                }
                Console.WriteLine("Mauvaise entrée");
            }
        }
        
    }
    
}
