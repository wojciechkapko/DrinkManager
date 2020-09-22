using BLL;
using DrinkManagerWeb.Data;
using DrinkManagerWeb.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormCollection data)
        {

            var ingredients = new List<Ingredient>();

            foreach (var key in data.Keys)
            {
                if (key.Contains("Ingredient"))
                {
                    var ingredientData = data[key].ToString().Split(' ', 2);

                    ingredients.Add(new Ingredient
                    {
                        Name = ingredientData[0],
                        Amount = ingredientData[1]
                    });
                }
            }
            // image placeholder
            var imageUrl = "https://medifactia.com/wp-content/uploads/2018/01/placeholder.png";

            // if image data exists replace placeholder
            if (data.ContainsKey("ImageUrl"))
            {
                imageUrl = data["ImageUrl"];
            }


            var newDrink = new Drink
            {
                Id = Guid.NewGuid().ToString(),
                Ingredients = ingredients,
                GlassType = data["GlassType"],
                ImageUrl = imageUrl,
                DrinkReview = null,
                Category = data["Category"],
                AlcoholicInfo = data["AlcoholicInfo"],
                Instructions = data["Instructions"],
                Name = data["Name"]
            };

            _db.Drinks.Add(newDrink);

            return RedirectToAction(nameof(DrinkDetails), new { id = newDrink.Id });
        }

        public IActionResult Remove(string id)
        {
            var drink = _db.Drinks.Find(d => d.Id.Equals(id));
            if (drink == null)
            {
                return NotFound();
            }
            _db.Drinks.Remove(drink);

            TempData["Alert"] = $"Drink {drink.Name} removed";

            return RedirectToAction(nameof(Index));
        }
    }
}