using BLL;
using DrinkManagerWeb.Models;
using DrinkManagerWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkManagerWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UsersList()
        {
            return View(_userManager.Users);
        }

        public ViewResult CreateUser() => View();
 
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.Name,
                    Email = user.Email
                };
 
                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                    return RedirectToAction("UsersList");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
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

        public async Task<IActionResult> UpdateRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleModification model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    if (user == null) continue;
                    result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                        Errors();
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    if (user == null) continue;
                    result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                        Errors();
                }
            }
            if (ModelState.IsValid)
                return RedirectToAction(nameof(RolesList));
            else
                return await UpdateRole(model.RoleId);
        }

        public IActionResult Errors()
        {
            return View();
        }

    }
}
