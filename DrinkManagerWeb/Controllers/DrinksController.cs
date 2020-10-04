using BLL;
using BLL.Data.Repositories;
using DrinkManagerWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

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
            int pageSize = 12;
            var drinks = _drinkRepository.GetAllDrinks();
            drinks = sortOrder switch
            {
                "name_desc" => drinks.OrderByDescending(s => s.Name),
                _ => drinks.OrderBy(s => s.Name),
            };
            var model = new DrinksViewModel
            {
                Drinks = PaginatedList<Drink>.CreatePaginatedList(drinks, pageNumber ?? 1, pageSize)
            };
            return View(model);
        }

        [HttpGet("drink/{id}")]
        public async Task<IActionResult> DrinkDetails(string id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);
            if (drink == null)
            {
                // add error View
            }

            return View(drink);
        }

        [HttpGet("Drinks/favourites")]
        public IActionResult FavouriteDrinks(string sortOrder, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            int pageSize = 10;

            var drinks = this._drinkRepository.GetAllDrinks().Where(x => x.IsFavourite);
            
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

        public async Task<IActionResult> AddToFavourite(string id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);
            if (drink == null)
            {
                // add error View
            }
            drink.IsFavourite = true;

            _drinkRepository.Update(drink);
            await _drinkRepository.SaveChanges();
            
            return RedirectToAction("DrinkDetails", new {id});
        }

        public async Task<IActionResult> RemoveFromFavourite(string id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);
            if (drink == null)
            {
                // add error View
            }
            drink.IsFavourite = false;

            _drinkRepository.Update(drink);
            await _drinkRepository.SaveChanges();
            
            return RedirectToAction("DrinkDetails", new { id });
        }

        public IActionResult SearchByAlcoholContent(string sortOrder, int? pageNumber, bool alcoholics = true, bool nonAlcoholics = true, bool optionalAlcoholics = true)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["Alcoholics"] = alcoholics;
            ViewData["nonAlcoholics"] = nonAlcoholics;
            ViewData["optionalAlcoholics"] = optionalAlcoholics;
            int pageSize = 10;
            var drinks = SearchDrink.SearchByAlcoholContent(alcoholics, nonAlcoholics, optionalAlcoholics, _drinkRepository.GetAllDrinks().ToList());
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
                Drinks = PaginatedList<Drink>.CreatePaginatedList(drinks, pageNumber ?? 1, pageSize),
                Alcoholics = alcoholics,
                NonAlcoholics = nonAlcoholics,
                OptionalAlcoholics = optionalAlcoholics
            };
            return View(model);
        }
    }
}