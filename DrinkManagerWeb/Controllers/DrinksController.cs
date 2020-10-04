#nullable enable
using BLL;
using BLL.Data.Repositories;
using DrinkManagerWeb.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet("drink/edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);

            var model = new DrinkCreateViewModel
            {
                Id = drink?.DrinkId,
                GlassType = drink?.GlassType,
                Category = drink?.Category,
                Instructions = drink?.Instructions,
                AlcoholicInfo = drink?.AlcoholicInfo,
                Name = drink?.Name,
                Ingredients = drink?.Ingredients,
                ImageUrl = drink?.ImageUrl
            };

            return View("Create", model);
        }



        [HttpGet("drink/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("drink/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection data, string? id)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var ingredients = new List<Ingredient>();

            // create ingredient objects from the from data
            foreach (var key in data.Keys)
            {
                if (key.Contains("Ingredient"))
                {
                    ingredients.Add(new Ingredient
                    {
                        IngredientId = Guid.NewGuid().ToString(),
                        Name = data[key],
                        Amount = data["Amount" + key.Split("Ingredient")[1]]
                    });
                }
            }
            // image placeholder
            var imageUrl = "https://medifactia.com/wp-content/uploads/2018/01/placeholder.png";

            // if image data exists replace placeholder
            if (data.ContainsKey("ImageUrl") && string.IsNullOrWhiteSpace(data["ImageUrl"]) == false)
            {
                imageUrl = data["ImageUrl"];
            }

            // id that we will use for the redirect
            string redirectId;

            if (id != null)
            {
                // ID is not null, we edit
                var drinkToUpdate = await _drinkRepository.GetDrinkById(id);


                if (drinkToUpdate == null)
                {
                    // something went wrong redirect to drinks index
                    TempData["Alert"] = $"Drink not found";
                    return RedirectToAction(nameof(Index));
                }

                drinkToUpdate.Ingredients = ingredients;
                drinkToUpdate.GlassType = data["GlassType"];
                drinkToUpdate.Category = data["Category"];
                drinkToUpdate.AlcoholicInfo = data["AlcoholicInfo"];
                drinkToUpdate.Instructions = data["Instructions"];
                drinkToUpdate.Name = data["Name"];
                drinkToUpdate.ImageUrl = imageUrl;
                _drinkRepository.UpdateDrink(drinkToUpdate);
                redirectId = id;
            }
            else
            {
                // id was null, we create a new drink
                var newDrink = new Drink
                {
                    DrinkId = Guid.NewGuid().ToString(),
                    Ingredients = ingredients,
                    GlassType = data["GlassType"],
                    ImageUrl = imageUrl,
                    DrinkReview = null,
                    Category = data["Category"],
                    AlcoholicInfo = data["AlcoholicInfo"],
                    Instructions = data["Instructions"],
                    Name = data["Name"]
                };


                await _drinkRepository.AddDrink(newDrink);
                redirectId = newDrink.DrinkId;
            }

            await _drinkRepository.SaveChanges();

            return RedirectToAction(nameof(DrinkDetails), new { id = redirectId });
        }

        public async Task<IActionResult> Remove(string id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);

            if (drink == null)
            {
                return NotFound();
            }

            _drinkRepository.DeleteDrink(drink);
            await _drinkRepository.SaveChanges();

            TempData["Alert"] = $"Drink {drink.Name} removed";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddToFavourite(string id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);
            if (drink == null)
            {
                // add error View
            }
            drink.IsFavourite = true;

            _drinkRepository.UpdateDrink(drink);
            await _drinkRepository.SaveChanges();

            return RedirectToAction("DrinkDetails", new { id });
        }

        public async Task<IActionResult> RemoveFromFavourite(string id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);
            if (drink == null)
            {
                // add error View
            }
            drink.IsFavourite = false;

            _drinkRepository.UpdateDrink(drink);
            await _drinkRepository.SaveChanges();

            return RedirectToAction("DrinkDetails", new { id });
        }
    }
}