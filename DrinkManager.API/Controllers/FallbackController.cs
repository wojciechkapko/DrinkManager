using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DrinkManager.API.Controllers
{
    public class FallbackController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/client", "index.html"), "text/HTML");
        }
    }
}
