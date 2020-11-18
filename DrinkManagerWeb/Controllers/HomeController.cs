using BLL.Data.Repositories;
using BLL.Services;
using DrinkManagerWeb.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace DrinkManagerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISettingRepository _settingRepository;
        private readonly BackgroundJobScheduler _backgroundJobScheduler;

        public HomeController(ILogger<HomeController> logger, ISettingRepository settingRepository, BackgroundJobScheduler backgroundJobScheduler)
        {
            _logger = logger;
            _settingRepository = settingRepository;
            _backgroundJobScheduler = backgroundJobScheduler;
        }

        public IActionResult Index()
        {
            return View();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}