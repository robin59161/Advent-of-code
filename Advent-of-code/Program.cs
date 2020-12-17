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
            long res = Day16.Part2(Resources.Resource1.Input16);
            st.Stop();
            Console.WriteLine(res);
            Console.WriteLine("Temps d'éxécution :" + st.Elapsed.Milliseconds+"ms");
            Console.ReadKey();
        }
        
    }
    
}
