using BLL;
using DrinkManagerWeb.Data;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
                ViewData["CurrentSort"] = sortOrder;
                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

                if (searchString != null)
                {
                    pageNumber = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewData["CurrentFilter"] = searchString;

                var drinks = _db.Drinks.AsQueryable();
                if (!String.IsNullOrEmpty(searchString))
                {
                    drinks = drinks.Where(s => s.Name.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        drinks = drinks.OrderByDescending(s => s.Name);
                        break;
                    default:    
                        drinks = drinks.OrderBy(s => s.Name);
                        break;
                }

                int pageSize = 10;
                return View(PaginatedList<Drink>.CreatePaginatedList(drinks, pageNumber ?? 1, pageSize));
        }
    }
}