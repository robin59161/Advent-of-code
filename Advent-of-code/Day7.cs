using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code
{
    public static class Day7
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
            foreach (string line in input.Split(Environment.NewLine))
            {
                BagFactory.AddBag(line);
            }
            return BagFactory.GetBag("shiny gold").CanHold(1);
        }
    }

    

    public class Bag
    {
        public string Label { get; set; }
        public List<KeyValuePair<Bag,int>> Childs { get; set; }
        public List<Bag> Parents { get; set; }
        public Bag(string label)
        {
            this.Label = label;
            Childs = new List<KeyValuePair<Bag, int>>();
            Parents = new List<Bag>();
        }

        public void AddChild(Bag b,int nb)
        {
            this.Childs.Add(new KeyValuePair<Bag, int>(b, nb));
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

        public int CanBeHoldBy(List<Bag> parents)
        {
            int nb = this.Parents.Count();
            if(parents == null)
                parents = new List<Bag>();
            parents.AddRange(this.Parents.Where(x=>!parents.Contains(x)));
            foreach (Bag bag in Parents)
            {
                nb += bag.CanBeHoldBy(parents);
            }
            return parents.Count();
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
