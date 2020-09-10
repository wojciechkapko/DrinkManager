using BLL;
using DrinkManagerWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.Data;
using BLL.Data.Repositories;

namespace DrinkManagerWeb.Controllers
{
    public class DrinksController : Controller
    {
        private readonly IDrinkRepository _drinkRepository;

        public DrinksController(IDrinkRepository drinkRepository)
        {
            _drinkRepository = drinkRepository;
        }

        public async Task<IActionResult> Index(string sortOrder, int? pageNumber)
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
                Drinks = await PaginatedList<Drink>.CreatePaginatedList(drinks, pageNumber ?? 1, pageSize)
            };
            return View(model);
        }
    }
}