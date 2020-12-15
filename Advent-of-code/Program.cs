using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_code
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch st = new Stopwatch();
            st.Start();
            long res = Day15.Part2("6,3,15,13,1,0", 30000000);
            st.Stop();
            Console.WriteLine(res);
            Console.WriteLine("Temps d'éxécution :" + Math.Round(st.Elapsed.TotalSeconds,3)+"s");
            Console.ReadKey();
        }
        
    }
    
}
