using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DrinkManagerWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        RoleManager<IdentityRole> _roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult RolesList()
        {
            var roles = _roleManager.Roles.ToList();  
            return View(roles);
        }

        public IActionResult CreateRole()  
        {  
            return View(new IdentityRole());  
        }  
  
        [HttpPost]  
        public async Task<IActionResult> CreateRole(IdentityRole role)  
        {  
            await _roleManager.CreateAsync(role);  
            return RedirectToAction("RolesList");  
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                return RedirectToAction("RolesList");
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("RolesList", _roleManager.Roles);
        }

        public IActionResult UpdateRole()
        {
            throw new System.NotImplementedException();
        }
    }
}
