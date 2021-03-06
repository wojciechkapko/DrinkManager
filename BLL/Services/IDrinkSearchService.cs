﻿using Domain;
using Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public interface IDrinkSearchService
    {
        IQueryable<Drink> SearchByName(string textToSearch);

        IQueryable<Drink> SearchByIngredients(SortedSet<string> ingredientsToSearch, SearchDrinkOption searchOption);

        IQueryable<Drink> SearchByAlcoholContent(bool alcoholics, bool nonAlcoholics, bool optionalAlcoholics);
    }
}
