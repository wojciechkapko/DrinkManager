using Microsoft.AspNetCore.Mvc;
using ReportingModule.API.Data;
using ReportingModule.API.Services;
using System;
using System.Threading.Tasks;

namespace ReportingModule.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportDataService _reportDataService;
        private readonly IUserOrientedDataService _userOrientedDataService;

        public ReportController(IReportDataService reportDataService, IUserOrientedDataService userOrientedDataService)
        {
            _reportDataService = reportDataService;
            _userOrientedDataService = userOrientedDataService;
        }

        [HttpGet("generateReport/{datesInfo}")]
        public async Task<IActionResult> GenerateNewReport(string datesInfo)
        {
            var bothDates = datesInfo.Split(",");
            var start = DateTime.Parse(bothDates[0]).AddHours(-1);
            var end = DateTime.Parse(bothDates[1]).AddHours(-1);
            var report = new Report()
            {
                StartDate = start,
                EndDate = end,
                NewRegisters = await _reportDataService.GetNewRegisters(start, end),
                SuccessfulLoginsAmount = await _reportDataService.GetSuccessfulLoginsData(start, end),
                MostFavouriteDrink = await _reportDataService.GetMostFavouriteDrinkData(start, end),
                HighestScoreDrink = await _reportDataService.GetHighestScoreDrinkData(start, end),
                LowestScoreDrink = await _reportDataService.GetLowestScoreDrinkData(start, end),
                MostVisitedDrink = await _reportDataService.GetMostVisitedDrinkData(start, end),
                MostActiveUser = await _reportDataService.GetMostActiveUser(start, end),
                MostReviewedDrink = await _reportDataService.GetMostReviewedDrinkData(start, end)
            };

            return Ok(report);
        }

        [HttpGet("checkUser/{username}")]
        public async Task<IActionResult> GenerateUserReport(string username)
        {
            var userReport = new UserReport()
            {
                Username = username,
                RegisteredAt = await _userOrientedDataService.GetUserCreationDate(username),
                LoginsCount = await _userOrientedDataService.GetUserLoginsCount(username),
                LastSeen = await _userOrientedDataService.GetTimeSinceLastUserActivity(username),
                MostVisitedDrink = await _userOrientedDataService.GetMostVisitedDrinkData(username),
                RecentlyFavouriteDrink = await _userOrientedDataService.GetLastAddedFavouriteDrink(username),
                RecentlyReviewedDrink = await _userOrientedDataService.GetLastReviewedDrink(username)
            };

            return Ok(userReport);
        }
    }
}
