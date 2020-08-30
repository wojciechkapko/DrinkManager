using DrinkManagerWeb.Data;
using DrinkManagerWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            var model = new DrinksViewModel
            {
                Drinks = _db.Drinks.ToList()
            };

            return View(model);
        }
    }
}