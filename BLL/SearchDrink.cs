using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class SearchDrink
    {
        public static List<Drink> SearchByName(string textToSearch, List<Drink> drinksListToSearch)
        {
            return drinksListToSearch.Where(drink =>  drink.Name.Contains(textToSearch, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public static List<Drink> SearchByIngredients(List<string> ingredientsToSearch, List<Drink> drinksListToSearch)
        {
            
            
            var drinksFound = new List<Drink>();

            //foreach (var drink in drinksListToSearch)
            //{
            //    drinksFound = drink.Ingredient;
            //}
            //drinksFound = drinksListToSearch.Select(drink => drink.Ingredients.ToString().Contains(ingredientsToSearch.));

            //var ingredientsFoundCount = 0;

            //foreach (var drink in drinksListToSearch)
            //{
            //    if (drink.Ingredients.Contains.All(ingredientsToSearch))
            //    {
                    

            //    }

            //    foreach (var ingredient in drink.Ingredients)
            //    {
            //        if (ingredient.Name == null)
            //        {
            //            continue;
            //        }
                    
            //        foreach (var ingredientToSearch in ingredientsToSearch)
            //        {
            //            if (ingredient.Name.Contains(ingredientToSearch, StringComparison.InvariantCultureIgnoreCase))
            //            {
            //                ingredientsFoundCount++;
            //            }
            //        }
            //    }

            //    if (ingredientsFoundCount >= ingredientsToSearch.Count)
            //    {
            //        drinksFound.Add(drink);
            //    }
            //}

            return drinksFound;
        }
    }
}
