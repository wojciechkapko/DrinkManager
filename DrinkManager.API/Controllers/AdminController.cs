using BLL.Services;
using Domain;
using DrinkManager.API.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Persistence.Repositories;
using ReportingModule.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DrinkManager.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly ISettingRepository _settingRepository;
        private readonly BackgroundJobScheduler _backgroundJobScheduler;
        private readonly IReportingModuleService _apiService;

        public AdminController(
            RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager,
            IPasswordHasher<AppUser> passwordHasher,
            BackgroundJobScheduler backgroundJobScheduler,
            ISettingRepository settingRepository,
            IReportingModuleService apiService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _backgroundJobScheduler = backgroundJobScheduler;
            _settingRepository = settingRepository;
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Users");
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
                ApplicationRoles = _roleManager.Roles
                    .Select(r => new SelectListItem { Text = r.Name, Value = r.Id })
                    .ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                if (role != null)
                {
                    AppUser user = new AppUser
                    {
                        Email = model.Email,
                        UserName = model.Email
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                        return RedirectToAction("Users");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            model.ApplicationRoles = _roleManager
                .Roles
                .Select(r => new SelectListItem { Text = r.Name, Value = r.Id })
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> UpdateUserRoleAndEmail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Users");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                RedirectToAction("Users");
            }

            var model = new UserEditRoleAndEmailViewModel
            {
                Id = id,
                UserName = user.UserName,
                Email = user?.Email,
                ApplicationRoleId = _roleManager.Roles
                    .SingleOrDefault(r => r.Name == _userManager.GetRolesAsync(user).Result.SingleOrDefault())?.Id,
                ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem { Text = r.Name, Value = r.Id })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRoleAndEmail(UserEditRoleAndEmailViewModel model)
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

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            model.ApplicationRoles = _roleManager
                .Roles
                .Select(r => new SelectListItem { Text = r.Name, Value = r.Id })
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> UpdateUserPassword(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Users");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("Users");
            }

            var model = new UserEditPasswordViewModel
            {
                Email = user.Email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserPassword(UserEditPasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (ModelState.IsValid)
            {
                var passwordValidator = new PasswordValidator<AppUser>();
                var validationResult = await passwordValidator.ValidateAsync(_userManager, user, model.Password);
                if (!validationResult.Succeeded)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Users");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            AppUser userToDelete = await _userManager.FindByIdAsync(id);

            if (userToDelete.Email == Environment.GetEnvironmentVariable("AdminUserEmail"))
            {
                return RedirectToAction("Users");
            }
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
            return View(new CreateRoleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel roleModel)
        {
            var role = new IdentityRole { Name = roleModel.Name };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Roles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(roleModel);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole roleToDelete = await _roleManager.FindByIdAsync(id);

            if (roleToDelete.Name == Environment.GetEnvironmentVariable("RestrictedRoleNamePrimary") || roleToDelete.Name == Environment.GetEnvironmentVariable("RestrictedRoleNameSecondary"))
            {
                return RedirectToAction("Roles");
            }
            return View(roleToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id, IdentityRole role)
        {
            IdentityRole roleToDelete = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(roleToDelete);
            return RedirectToAction("Roles");
        }

        [HttpGet("settings")]
        public IActionResult Settings()
        {
            var model = new SettingsViewModel
            {
                Settings = _settingRepository.GetAllSettings().ToList()
            };

            return View(model);
        }

        [HttpPost("settings")]
        public IActionResult UpdateSettings(IFormCollection data)
        {
            var originalSettings = _settingRepository.GetAllSettings().Where(s => s.DisallowManualChange == false).ToList();
            var shouldRestartBackgroundJob = false;

            for (var i = 0; i < originalSettings.Count(); i++)
            {
                if (originalSettings[i].Value != data[originalSettings[i].Name])
                {
                    originalSettings[i].Value = data[originalSettings[i].Name];
                    _settingRepository.Update(originalSettings[i]);
                    shouldRestartBackgroundJob = true;
                }
            }

            if (shouldRestartBackgroundJob)
            {
                _backgroundJobScheduler.StopAsync(new CancellationToken());
                _backgroundJobScheduler.StartAsync(new CancellationToken());
            }

            return RedirectToAction(nameof(Settings));
        }

        [HttpGet]
        public async Task<IActionResult> UserActivitiesReport(string username)
        {
            var model = await _apiService.GetUserReportData(username);
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

        [HttpPost]
        public async Task<IActionResult> GeneralReport(IFormCollection data)
        {
            if (data["start.date"].ToString().Length == 0 || data["end.date"].ToString().Length == 0)
            {
                TempData["Alert"] = "Both dates must be chosen";
                TempData["AlertClass"] = "alert-danger";
                return RedirectToAction(nameof(Settings));
            }
            var model = await _apiService.GetReportData(DateTime.Parse(data["start.date"]), DateTime.Parse(data["end.date"]));
            return View(model);
        }
    }
}
