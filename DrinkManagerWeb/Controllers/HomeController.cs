using BLL;
using BLL.Data.Repositories;
using DrinkManagerWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DrinkManagerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDrinkRepository _drinkRepository;

        public HomeController(ILogger<HomeController> logger, IDrinkRepository drinkRepository)
        {
            _logger = logger;
            _drinkRepository = drinkRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                Drinks = await _drinkRepository.GetAllDrinks()
            };

            // temp test drink to be removed
            var newDrink = new Drink
            {
                Name = "test",
                Ingredients = null,
                DrinkReview = null,
                AlcoholicInfo = "alcoholic",
                GlassType = "tall",
                Category = "nice",
                Instructions = "mix",
                Id = Guid.NewGuid().ToString()
            };

            // tests to be removed
            _drinkRepository.DeleteDrink(await _drinkRepository.FindDrink(d => d.Name.Equals("test")));
            await _drinkRepository.SaveChanges();

            var id = newDrink.Id;
            await _drinkRepository.AddDrink(newDrink);
            await _drinkRepository.SaveChanges();
            var testDrink = await _drinkRepository.GetDrinkById(id);
            testDrink.Name = "Test22";
            _drinkRepository.Update(testDrink);
            await _drinkRepository.SaveChanges();
            _drinkRepository.DeleteDrink(testDrink);
            await _drinkRepository.SaveChanges();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}