using BLL;
using DrinkManagerWeb.Data;
using DrinkManagerWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

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
    }
}