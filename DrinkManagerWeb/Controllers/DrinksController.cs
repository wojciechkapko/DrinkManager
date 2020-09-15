using BLL;
using DrinkManagerWeb.Data;
using DrinkManagerWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Enums;

namespace DrinkManagerWeb.Controllers
{
    public class DrinksController : Controller
    {
        private readonly DrinkAppContext _db;

        public DrinksController(DrinkAppContext db)
        {
            _db = db;
        }

        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
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
                // solution using BLL -- requires significant adjustments in BLL & console app (changes of List type to IQuerable)
                // do we have to care about console app ?
                //drinks = SearchDrink.SearchByName(searchString, drinks);
                drinks = drinks.Where(drink =>
                    drink.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase));
            }

            ViewData["SearchString"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            int pageSize = 10;
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

        public IActionResult SearchByIngredients(string searchString, string sortOrder, int? pageNumber)
        {
            var drinks = _db.Drinks.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                //drinks = SearchDrink.SearchByIngredients(new SortedSet<string>(searchString.Split(' ')), drinks.ToList(), SearchDrinkOption.Any);
            }

            ViewData["SearchString"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            int pageSize = 10;
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

    }
}