using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code._2020
{
    public static class Day07
    {
        public static int Part1(string input)
        {
            foreach(string line in input.Split(Environment.NewLine))
            {
                BagFactory.AddBag(line);
            }
            return BagFactory.GetBag("shiny gold").CanBeHoldBy(null);
        }
        public static int Part2(string input)
        {
            if (BagFactory.Bags.Count == 0)
            {
                foreach (string line in input.Split(Environment.NewLine))
                {
                    BagFactory.AddBag(line);
                }
            }
            return BagFactory.GetBag("shiny gold").CanHold(1);
        }
    }

    

    public class Bag
    {
        public string Label { get; set; }
        public Dictionary<Bag,int> Childs { get; set; }
        public List<Bag> Parents { get; set; }
        public Bag(string label)
        {
            this.Label = label;
            Childs = new Dictionary<Bag, int>();
            Parents = new List<Bag>();
        }

        public void AddChild(Bag b,int nb)
        {
            this.Childs.Add(b, nb);
        }

        public void AddParent(Bag b)
        {
            this.Parents.Add(b);
        }

        public int CanHold(int nb)
        {
            int count = nb*this.Childs.Sum(x=>x.Value);
            foreach(KeyValuePair<Bag,int> bag in Childs)
            {
                count += bag.Key.CanHold(nb * bag.Value); ;
            }
            return count;
        }

        public int CanBeHoldBy(List<Bag> visited)
        {
            if (visited == null)
                visited = new List<Bag>();
            List<Bag> notVisited = this.Parents.Where(x => !visited.Contains(x)).ToList();
            int nb = notVisited.Count();
            visited.AddRange(notVisited);
            foreach (Bag bag in notVisited)
            {
                nb += bag.CanBeHoldBy(visited);
            }
            
            return nb;
        }

    }

    public static class BagFactory
    {
        public static List<Bag> Bags = new List<Bag>();

        public static void AddBag(string bag)
        {
            Regex r = new Regex(@"(.*) bags contain (.*)");
            Match m = r.Match(bag);
            string label = m.Groups[1].Value;
            string contains = m.Groups[2].Value;
            Bag b = GetBag(label);
            if (contains != "no other bags.")
            {
                foreach (string contain in contains.Split(','))
                {
                    Match c = Regex.Match(contain, @"(\d+) (.*) bag");
                    int nb = Int32.Parse(c.Groups[1].Value);
                    string l = c.Groups[2].Value;
                    Bag child = GetBag(l);
                    child.AddParent(b);
                    b.AddChild(child, nb);
                }
            }
            
        }

        public static Bag GetBag(string label)
        {
            Bag b = Bags.FirstOrDefault(x => x.Label == label);
            if (b == null)
            {
                b = new Bag(label);
                Bags.Add(b);
            }
            return b;
        }

    }
}
