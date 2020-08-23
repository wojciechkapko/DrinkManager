using BLL;
using DrinkManagerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DrinkManagerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DrinkLoader _loader;

        public HomeController(ILogger<HomeController> logger, DrinkLoader loader)
        {
            _logger = logger;
            _loader = loader;
        }

        public IActionResult Index()
        {
            var drinks = _loader.InitializeDrinksFromFile();
            return View();
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