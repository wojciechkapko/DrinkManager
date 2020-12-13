using BLL;
using DrinkManagerWeb.Models;
using DrinkManagerWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.Services;
using Newtonsoft.Json;

namespace DrinkManagerWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IReportingModuleService _reportingModuleService;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IPasswordHasher<AppUser> passwordHasher, IReportingModuleService reportingModuleService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _passwordHasher = passwordHasher;
            _reportingModuleService = reportingModuleService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            return View(new AdminIndexViewModel
            {
                DateToday = DateTime.Today.ToShortDateString(),
                DayOfWeekToday = DateTime.Today.DayOfWeek.ToString(),
                UserName = user.Email
            });
        }

        public IActionResult Users()
        {
            var users = _userManager.Users.OrderBy(user => user.UserName).ToList();
            var usersAndRoles = new Dictionary<string, List<string>>();

            foreach (var user in users)
            {
                var roles = _userManager.GetRolesAsync(user).Result.ToList();
                usersAndRoles.Add(user.UserName, roles);
            }

            var model = new UserListViewModel { RolesPerUser = usersAndRoles, Users = users };

            return View(model);
        }

        public ViewResult CreateUser()
        {
            var model = new UserViewModel
            {
                ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem { Text = r.Name, Value = r.Id })
                    .ToList()
            };
            return View("CreateUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    IdentityRole applicationRole = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Users");
                        }
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> UpdateUser(string id)
        {
            UserViewModel model = new UserViewModel
            {
                ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem { Text = r.Name, Value = r.Id })
                    .ToList()
            };

            if (!string.IsNullOrEmpty(id))
            {
                AppUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    model.Email = user.Email;
                    model.ApplicationRoleId = _roleManager.Roles.SingleOrDefault(r => r.Name == _userManager.GetRolesAsync(user).Result.SingleOrDefault())?.Id;
                }
            }
            return View("UpdateUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(model.Email))
                    {
                        user.Email = model.Email;
                        user.UserName = model.Email;
                    }
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                    }
                    if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password))
                    {
                        IdentityResult result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            string existingRole = _userManager.GetRolesAsync(user).Result.SingleOrDefault();
                            string existingRoleId = _roleManager.Roles.SingleOrDefault(r => r.Name == existingRole)?.Id;
                            if (existingRoleId == null)
                            {
                                IdentityRole applicationRole = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                                if (applicationRole != null)
                                {
                                    IdentityResult newRoleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                                    if (newRoleResult.Succeeded)
                                    {
                                        return RedirectToAction("Users");
                                    }
                                }
                            }
                            else
                            {
                                if (existingRoleId != model.ApplicationRoleId)
                                {
                                    IdentityResult roleResult = await _userManager.RemoveFromRoleAsync(user, existingRole);
                                    if (roleResult.Succeeded)
                                    {
                                        IdentityRole applicationRole = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                                        if (applicationRole != null)
                                        {
                                            IdentityResult newRoleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                                            if (newRoleResult.Succeeded)
                                            {
                                                return RedirectToAction("Users");
                                            }
                                        }
                                    }
                                }
                            }
                            return RedirectToAction("Users");
                        }
                    }
                }
            }
            return RedirectToAction("Users");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            AppUser userToDelete = await _userManager.FindByIdAsync(id);
            return View(userToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id, AppUser user)
        {
            AppUser userToDelete = await _userManager.FindByIdAsync(id);
            if (userToDelete != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(userToDelete);
                if (result.Succeeded)
                    return RedirectToAction("Users");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Users");
        }

        public async Task<IActionResult> Roles()
        {
            var roles = _roleManager.Roles.OrderBy(role => role.Name).ToList();
            var roleUsers = new Dictionary<string, List<string>>();

            foreach (var identityRole in roles)
            {
                var users = await _userManager.GetUsersInRoleAsync(identityRole.Name);
                var usersToReturn = users.Select(u => u.UserName).ToList();
                roleUsers.Add(identityRole.Name, usersToReturn);
            }

            var model = new RoleViewModel
            {
                Roles = roles,
                UsersPerRole = roleUsers
            };

            return View(model);
        }

        public IActionResult CreateRole()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Roles");
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole roleToDelete = await _roleManager.FindByIdAsync(id);
            return View(roleToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id, IdentityRole role)
        {
            IdentityRole roleToDelete = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(roleToDelete);
            return RedirectToAction("Roles");
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
                return RedirectToAction(nameof(Roles));
            else
                return await UpdateRole(model.RoleId);
        }

        [HttpGet]
        public async Task<IActionResult> UserActivitiesReport(string username)
        {
            var model = await _reportingModuleService.GetUserReportData(username);
            return View(model);
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        public IActionResult Errors()
        {
            return View();
        }
    }
}
