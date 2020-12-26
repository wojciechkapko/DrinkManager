using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DrinkManager.API.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        [HttpGet]
        public IActionResult Index([FromQuery] int? page)
        {

            var orders = new List<string> { "a", "b", "c" };

            return Ok(orders);
        }
    }
}
