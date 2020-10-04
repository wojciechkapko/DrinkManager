using BLL;
using BLL.Data.Repositories;
using DrinkManagerWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DrinkManagerWeb.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IDrinkRepository _drinkRepository;

        public DrinksController(IDrinkRepository drinkRepository)
        {
            _drinkRepository = drinkRepository;
        }

        public IActionResult Index(string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            int pageSize = 10;
            var drinks = _drinkRepository.GetAllDrinks();
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
        public async Task<IActionResult> DrinkDetails(string id)
        {

            var model = new DrinkDetailsViewModel
            {
                Drink = await _drinkRepository.GetDrinkById(id)
                IsFavourite = _db.FavouriteDrinksIds.Contains(int.Parse(id))
            };

            return View(model);
        }
        [HttpGet("Drinks/favourites")]
        public IActionResult FavouriteDrinks(string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            int pageSize = 10;
            var drinks = _db.Drinks.AsQueryable();
            var favouritesIds = _db.FavouriteDrinksIds;
            drinks = drinks.Where(drink => favouritesIds.Contains(int.Parse(drink.Id)));
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

        public IActionResult AddToFavourite(string id)
        {
            _db.FavouriteDrinksIds.Add(int.Parse(id));

            return RedirectToAction("DrinkDetails", new {id});
        }

        public IActionResult RemoveFromFavourite(string id)
        {
            _db.FavouriteDrinksIds.Remove(int.Parse(id));

            return RedirectToAction("DrinkDetails", new { id });
        }
    }
}