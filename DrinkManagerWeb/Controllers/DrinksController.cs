using BLL;
using BLL.Enums;
using DrinkManagerWeb.Data;
using DrinkManagerWeb.Models.ViewModels;
using DrinkManagerWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrinkManagerWeb.Controllers
{
    public class DrinksController : Controller
    {
        private readonly DrinkAppContext _db;
        private readonly IDrinkSearchService _drinkSearchService;

        public DrinksController(DrinkAppContext db, IDrinkSearchService drinkSearchService)
        {
            _db = db;
            _drinkSearchService = drinkSearchService;
        }

        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            int pageSize = 10;
            var drinks = _db.Drinks.AsQueryable();
            switch (sortOrder)
            {
                case "name_desc":
                    drinks = drinks.OrderByDescending(s => s.Name);
                    break;
                default:    
                    drinks = drinks.OrderBy(s => s.Name);
                    break;
            }
            var model = new DrinksViewModel
            {
                Drinks = PaginatedList<Drink>.CreatePaginatedList(drinks, pageNumber ?? 1, pageSize)
            };
            return View(model);
        }

        [HttpGet("drink/{id}")]
        public IActionResult DrinkDetails(string id)
        {

            var model = new DrinkDetailsViewModel
            {
                Drink = _db.Drinks.FirstOrDefault(d => d.Id.Equals(id))
            };

            return View(model);
        }

        public IActionResult SearchByName(string searchString, string sortOrder, int? pageNumber)
        {
            var drinks = _db.Drinks.AsQueryable();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                drinks = _drinkSearchService.SearchByName(searchString, drinks);
            }

            ViewData["SearchString"] = searchString;
            ViewData["SearchType"] = "SearchByName";
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            int pageSize = 10;

            var model = new DrinksViewModel
            {
                Drinks = PaginatedList<Drink>.CreatePaginatedList(_drinkSearchService.SortDrinks(sortOrder, drinks), pageNumber ?? 1, pageSize)
            };
            return View(model);
        }

        public IActionResult SearchByIngredients(string searchString, string searchCondition, string sortOrder, int? pageNumber)
        {
            var drinks = _db.Drinks.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                var searchDrinkIngredientsCondition =
                    searchCondition.Equals("all") ? SearchDrinkOption.All : SearchDrinkOption.Any;
                drinks = _drinkSearchService.SearchByIngredients(new SortedSet<string>(searchString.Split(' ')), drinks, searchDrinkIngredientsCondition);
            }

            ViewData["SearchString"] = searchString;
            ViewData["SearchCondition"] = searchCondition;
            ViewData["SearchType"] = "SearchByIngredients";
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            int pageSize = 10;

            var model = new DrinksViewModel
            {
                Drinks = PaginatedList<Drink>.CreatePaginatedList(_drinkSearchService.SortDrinks(sortOrder, drinks), pageNumber ?? 1, pageSize)
            };
            return View(model);
        }
    }
}