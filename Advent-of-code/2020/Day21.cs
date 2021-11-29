using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2020
{
    public static class Day21
    {
        record Problem(
        HashSet<string> allergens,
        HashSet<string> ingredients,
        (HashSet<string> ingredients, HashSet<string> allergens)[] mapping);

        public static long Part1(string input)
        {
            var problem = Parse(input);
            var suspiciousIngredients = GetIngredientsByAllergene(problem).SelectMany(kvp => kvp.Value).ToHashSet();
            return problem.mapping
                .Select(entry => entry.ingredients.Count(ingredient => !suspiciousIngredients.Contains(ingredient)))
                .Sum();
        }

        public static string Part2(string input)
        {
            var problem = Parse(input);
            var ingredientsByAllergene = GetIngredientsByAllergene(problem);

            while (ingredientsByAllergene.Values.Any(ingredients => ingredients.Count > 1))
            {
                foreach (var allergen in problem.allergens)
                {
                    var candidates = ingredientsByAllergene[allergen];
                    if (candidates.Count == 1)
                    {
                        foreach (var allergenT in problem.allergens)
                        {
                            if (allergen != allergenT)
                            {
                                ingredientsByAllergene[allergenT].Remove(candidates.Single());
                            }
                        }
                    }
                }
            }

            return string.Join(",", problem.allergens.OrderBy(a => a).Select(a => ingredientsByAllergene[a].Single()));
        }

        private static Problem Parse(string input)
        {
            var mapping = (
                from line in input.Split(Environment.NewLine)
                let parts = line.Trim(')').Split(" (contains ")
                let ingredients = parts[0].Split(" ").ToHashSet()
                let allergens = parts[1].Split(", ").ToHashSet()
                select (ingredients, allergens)
            ).ToArray();

            return new Problem(
                mapping.SelectMany(entry => entry.allergens).ToHashSet(),
                mapping.SelectMany(entry => entry.ingredients).ToHashSet(),
                mapping
            );
        }

        private static Dictionary<string, HashSet<string>> GetIngredientsByAllergene(Problem problem) =>
            problem.allergens.ToDictionary(
                allergene => allergene,
                allergene => problem.mapping
                    .Where(entry => entry.allergens.Contains(allergene))
                    .Aggregate(
                        problem.ingredients as IEnumerable<string>,
                        (res, entry) => res.Intersect(entry.ingredients))
                    .ToHashSet());
    }
}
